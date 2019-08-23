using System.Collections.Generic;

namespace LownSlow.Models.RecipeViewModels
{
    public class RecipeIngredientsViewModel
    {
        public Ingredient Ingredient { get; set; }
        public Recipe Recipe { get; set; }
        public IEnumerable<IngredientList> IngredientList { get; set; }
    }
}
