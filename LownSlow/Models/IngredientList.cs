﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LownSlow.Models
{
    public class IngredientList
    {
        [Key]
        public int IngredientListId { get; set; }

        public Ingredient Ingredient { get; set; }

        public Recipe Recipe { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Measurement { get; set; }

        [Required(ErrorMessage = "You have to select an ingredient, you dingus.")]
        public int IngredientId { get; set; }

        [Required]
        public int RecipeId { get; set; }

    }
}
