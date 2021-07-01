using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AWWCORTestSolution.Models
{
    public class Ad
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Укажите название объявления")]
        [MaxLength(200, ErrorMessage = "Название не должно превышать 200 символов")]
        public string name { get; set; }
        public decimal price { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{dd.MM.yyyy HH:mm:ss}")]
        public DateTime dateofcreate { get; set; }

        [MaxLength(1000, ErrorMessage = "Название не должно превышать 1000 символов")]
        public string description { get; set; }
        public string mainphoto { get; set; }

       // public string[] Photos { get; set; }
    }
}
