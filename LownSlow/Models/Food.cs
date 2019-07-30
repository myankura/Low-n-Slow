using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LownSlow.Models
{
    public class Food
    {
        [Key]
        [Display(Name = "Food")]
        public int FoodId { get; set; }

        [Required]
        public string Name { get; set; }


        public string Description { get; set; }

        [Display(Name ="Food Type")]
        public string Type { get; set; }

        [Display(Name = "Cooking Time")]
        public DateTime CookTime { get; set; }

        [Display(Name = "Cooking Temperature")]
        public int CookTemp { get; set; }

        [Display(Name = "Finished Temperature")]
        public string FinishedTemp { get; set; }


    }
}
