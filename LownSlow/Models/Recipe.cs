using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LownSlow.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Please name the recipe.")]
        [Display(Name = "Recipe Name")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Please give the recipe a description.")]
        public string Description { get; set; }

        [Required(ErrorMessage ="The recipe must have some directions")]
        public string Directions { get; set; }

        public string Comment { get; set; }

        [Required]
        [Display(Name = "Favorite Recipe")]
        public bool Favorite { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string  UserId { get; set; }

        [Required]
        public int TechniqueId { get; set; }

        public virtual ICollection<IngredientList> IngredientLists { get; set; }

    }
}
