using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LownSlow.Models
{
    public class Ingredient
    {

        [Key]
        [Required]
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "Please name the ingredient")]
        [Display(Name ="Ingredient")]
        public string Name { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<IngredientList> IngredientLists { get; set; }

    }
}
