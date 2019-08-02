using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LownSlow.Models.RecipeViewModels
{
    public class RecipeCreateViewModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        

        public Recipe Recipe { get; set; }
        public Technique Technique { get; set; }
        public IngredientList IngredientList { get; set; }
        public List<Ingredient> AvailableIngredients { get; set; }

        public List<SelectListItem> IngredientOptions
        {
            get
            {
                
                if (AvailableIngredients == null)
                {
                    return null;
                }
                    var il = AvailableIngredients?.Select(i => new SelectListItem(i.Name, i.IngredientId.ToString())).ToList();
                    il.Insert(0, new SelectListItem("Select an ingredient", null));
                    return il;
            }
        }
    }
}
