using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWWCORTestSolution.Models;


namespace AWWCORTestSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : Controller
    {
        AdContext db;

        public AdsController(AdContext context)
        {
            db = context;            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ad>>> Get()
        { 
            var bb = await db.Ads.ToListAsync();
            foreach (var ad in bb)
            {
                ad.dateofcreate.ToString("dd.MM.yyyy HH:mm:ss");
            }
            return bb;
        }

        // GET api/ads/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Ad>> Get(int id)
        {
            Ad ad = await db.Ads.FirstOrDefaultAsync(x => x.id == id);
            ad.dateofcreate.ToString("dd.MM.yyyy HH:mm:ss");
            if (ad == null)
                return NotFound();
            return new ObjectResult(ad);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Ad>> Post(Ad ad)
        {
            if (ad.name.Length == 0)
                ModelState.AddModelError("name", "Название не должно быть пустым");

            if (ad.name.Length >= 200)
                ModelState.AddModelError("name", "Название не должно превышать 200 символов_");

            if (ad.description.Length >= 1000)
                ModelState.AddModelError("description", "Название не должно превышать 1000 символов");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (ad == null)
            {
                return BadRequest();
            }

            ad.dateofcreate = DateTime.Now;
            db.Ads.Add(ad);
            await db.SaveChangesAsync();
            return Ok(ad);
        }

        // PUT api/ads/
        [HttpPut]
        public async Task<ActionResult<Ad>> Put(Ad ad)
        {
            ad.dateofcreate = DateTime.Now;
            if (ad == null)
            {
                return BadRequest();
            }
            if (!db.Ads.Any(x => x.id == ad.id))
            {
                return NotFound();
            }

            db.Update(ad);
            await db.SaveChangesAsync();
            return Ok(ad);
        }

        // DELETE api/ads/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ad>> Delete(int id)
        {
            Ad ad = db.Ads.FirstOrDefault(x => x.id == id);
            if (ad == null)
            {
                return NotFound();
            }
            db.Ads.Remove(ad);
            await db.SaveChangesAsync();
            return Ok(ad);
        }

        public async Task<IActionResult> Index(SortState sortOrder = SortState.PriceAsc)
        {
            IQueryable<Ad> ads = db.Ads.Include(x => x.price);

            ViewData["PriceSort"] = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            ViewData["DateSort"] = sortOrder == SortState.DateOfCreateAsc ? SortState.DateOfCreateDesc : SortState.DateOfCreateAsc;

            ads = sortOrder switch
            {
                SortState.PriceDesc => ads.OrderByDescending(s => s.price),
                SortState.PriceAsc => ads.OrderBy(s => s.price),
                SortState.DateOfCreateDesc => ads.OrderByDescending(s => s.dateofcreate),
                SortState.DateOfCreateAsc => ads.OrderBy(s => s.dateofcreate),
            };
            return View(await ads.AsNoTracking().ToListAsync());
        }
    }
}
