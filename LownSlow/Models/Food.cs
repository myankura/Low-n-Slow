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
        [Display(Name = "Food Id")]
        public int FoodId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name ="Food Type")]
        public string Type { get; set; }

        public int MinCookTime { get; set; }

        public int MaxCookTime { get; set; }

        public int MinCookTemp { get; set; }

        public int MaxCookTemp { get; set; }

        public int MinFinishedTemp { get; set; }

        public int MaxFinishedTemp { get; set; }
    }
}
