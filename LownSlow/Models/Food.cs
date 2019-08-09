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

        [Display(Name = "Food Type")]
        public string Type { get; set; }

        [Display(Name = "Min. Cooking Time (Minutes)")]

        public int MinCookTime { get; set; }

        [Display(Name = "Max. Cooking Time (Minutes)")]
        public int MaxCookTime { get; set; }

        [Display(Name = "Min. Cooking Temperature (F)")]
        public int MinCookTemp { get; set; }

        [Display(Name = "Max. Cooking Temperature (F)")]
        public int MaxCookTemp { get; set; }

        [Display(Name = "Min. Finished Temperature (F)")]
        public int MinFinishedTemp { get; set; }

        [Display(Name = "Max. Finished Temperature (F)")]
        public int MaxFinishedTemp { get; set; }

        public string DisplayMinCookTime
        {
            get
            {
                MinCookTime.ToString($"{MinCookTime / 60} : {MinCookTime % 60}");
                return DisplayMinCookTime;
            }

        }
    }
}
