using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LownSlow.Models
{
    public class Ingredient
    {

        [Key]
        [Required(ErrorMessage = "You have to select an ingredient, you dingus.")]
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "Please name the ingredient")]
        public string Name { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<IngredientList> IngredientLists { get; set; }

        /*public static implicit operator Ingredient(List<Ingredient> v)
        {
            throw new NotImplementedException();
        }*/
    }
}
