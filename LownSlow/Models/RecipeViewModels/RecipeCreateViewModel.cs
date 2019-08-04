using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LownSlow.Models.RecipeViewModels
{
    public class RecipeCreateViewModel
    {

        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }
        public IngredientList IngredientLists { get; set; }

        public List<Technique> AvailableTech { get; set; }

        public List<SelectListItem> TechOptions
        {
            get
            {

                if (AvailableTech == null)
                {
                    return null;
                }
                var technique = AvailableTech?.Select(t => new SelectListItem(t.Name, t.TechniqueId.ToString())).ToList();
                technique.Insert(0, new SelectListItem("Select a technique", null));
                return technique;
            }
        }
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
