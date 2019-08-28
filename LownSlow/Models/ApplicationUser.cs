using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LownSlow.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public virtual ICollection<IngredientList> IngredientLists {get; set;}

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<Technique> Techniques { get; set; }

    }
}
