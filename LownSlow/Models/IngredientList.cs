using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public static IEnumerable<SelectListItem> GetMeasuresList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
            new SelectListItem() {Text="Teaspoon", Value="Teaspoon"},
            new SelectListItem() {Text="Tablespoon", Value="Tablespoon"},
            new SelectListItem() {Text="Cup", Value="Cup"},
            new SelectListItem() {Text="Ounce", Value="Ounce"},
            new SelectListItem() {Text="Pint", Value="Pint"},
            new SelectListItem() {Text="Quart", Value="Quart"},
            new SelectListItem() {Text="Pound", Value="Pound"},
            new SelectListItem() {Text="Gallon", Value="Gallon"},
            };
            return items;
        }
        /*public static List<SelectListItem> Measures = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Teaspoon", Value="Teaspoon"},
            new SelectListItem() {Text="Tablespoon", Value="Tablespoon"},
            new SelectListItem() {Text="Cup", Value="Cup"},
            new SelectListItem() {Text="Ounce", Value="Ounce"},
            new SelectListItem() {Text="Pint", Value="Pint"},
            new SelectListItem() {Text="Quart", Value="Quart"},
            new SelectListItem() {Text="Pound", Value="Pound"},
            new SelectListItem() {Text="Gallon", Value="Gallon"},
        };*/

        [Required]
        public int IngredientId { get; set; }

        [Required]
        public int RecipeId { get; set; }

    }
}
