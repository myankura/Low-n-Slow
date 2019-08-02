using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LownSlow.Models.RecipeViewModels
{
    public class RecipeIngredientsViewModel
    {
        public Ingredient Ingredient { get; set; }
        public Recipe Recipe { get; set; }
        public Technique Technique { get; set; }
        public IEnumerable<IngredientList> IngredientList { get; set; }
    }
}
