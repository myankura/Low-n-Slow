using System.ComponentModel.DataAnnotations;

namespace LownSlow.Models
{
    public class IngredientList
    {
        [Key]
        public int IngredientListId { get; set; }

        public Ingredient Ingredient { get; set; }

        public Recipe Recipe { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public string Measurement { get; set; }

        [Required]
        public int IngredientId { get; set; }

        [Required]
        public int RecipeId { get; set; }

    }
}
