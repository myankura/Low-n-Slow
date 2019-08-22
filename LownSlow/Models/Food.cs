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

        //Cooking times 
        public int MinCookTime { get; set; }
        public int MaxCookTime { get; set; }

        //declare a string variable to hold timeInterval
        public string  timeInterval = "";
        
        //Convert Minimum cooking times for each cut of meat to a more friendly format for the user.
        [Display(Name = "Min. Cooking Time")]
        public string ConvertMinCookTime
        {
            get
            {
                if (MinCookTime % 60 < 10)
                {
                    timeInterval = $"{MinCookTime / 60}:0{MinCookTime % 60}";
                }
                else
                {
                    timeInterval = $"{MinCookTime / 60}:{MinCookTime % 60}";
                }
                return timeInterval;
            }
        }

        //Convert Maximum cooking times for each cut of meat to a more friendly format for the user.
        [Display(Name = "Max. Cooking Time")]
        public string ConvertMaxCookTime
        {
            get
            {
                if (MaxCookTime % 60 < 10)
                {
                    timeInterval = $"{MaxCookTime / 60}:0{MaxCookTime % 60}";
                }
                else
                {
                    timeInterval = $"{MaxCookTime / 60}:{MaxCookTime % 60}";
                }
                return timeInterval;
            }
        }

        //Format all temperatures into an easier format for the user to read.
        public int MinCookTemp { get; set; }
        [Display(Name = "Min. Cooking Temperature")]
        public string FormatMinCookTemp => $"{MinCookTemp}F";

        public int MaxCookTemp { get; set; }
        [Display(Name = "Max. Cooking Temperature")]
        public string FormatMaxCookTemp => $"{MaxCookTemp}F";

        public int MinFinishedTemp { get; set; }
        [Display(Name = "Min. Finished Temperature")]
        public string FormatMinFinTemp => $"{MinFinishedTemp}F";

        public int MaxFinishedTemp { get; set; }
        [Display(Name = "Max. Finished Temperature")]
        public string FormatMaxFinTemp => $"{MaxFinishedTemp}F";

    }
}
