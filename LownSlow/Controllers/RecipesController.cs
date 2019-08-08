using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LownSlow.Data;
using LownSlow.Models;
using Microsoft.AspNetCore.Identity;
using LownSlow.Models.RecipeViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LownSlow.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            //Get current user's UserId.
            var currentUser = await GetCurrentUserAsync();

            //Sort through all recipes recipes and return only those that match the condition where UserId == currentUser.Id
            var usersRecipes = _context.Recipe.Include(r => r.IngredientLists)
                .Include(r => r.Technique)
                .Where(r => r.UserId == currentUser.Id);
            return View(await usersRecipes.ToListAsync());
        }


        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //Get current user
            var currentUser = await GetCurrentUserAsync();

            RecipeIngredientsViewModel viewmodel = new RecipeIngredientsViewModel();

            var recipe = await _context.Recipe.Include(r => r.Technique)
                                              .Include(r => r.User)
                                              .Include(r => r.IngredientLists)
                                              .ThenInclude(il => il.Ingredient)
                                              .FirstOrDefaultAsync(il => il.UserId == currentUser.Id && il.RecipeId == id);

            viewmodel.Recipe = recipe;

            if (id == null)
            {
                return NotFound();
            }

            if (recipe == null)
            {
                return NotFound();
            }

            return View(viewmodel);
        }

        // GET: Recipes/Create
        public async Task<IActionResult> Create()
        {
            //Get current user's UserId.
            var currentUser = await GetCurrentUserAsync();

            var viewModel = new RecipeCreateViewModel
            {
                AvailableTech = await _context.Technique.Where(t => t.User.Id == currentUser.Id).ToListAsync(),
                AvailableIngredients = await _context.Ingredient.Where(i => i.User.Id == currentUser.Id).ToListAsync(),
            };

            return View(viewModel);
        }

        // POST: Recipes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeCreateViewModel viewModel)
        {
            //This block of code will join the ingredient table with the recipe table
            //Check for current user
            var currentUser = await GetCurrentUserAsync();

            //Instantiate instances for recipe, ingredient, and ingredient list
            var recipe = viewModel.Recipe;
            var ingredient = viewModel.Ingredient;
            var ingredList = viewModel.IngredientLists;

            IngredientList newList = new IngredientList();

            ModelState.Remove("Recipe.User");
            ModelState.Remove("Recipe.UserId");
            ModelState.Remove("Ingredient.Name");
            ModelState.Remove("Ingredient.User");
            ModelState.Remove("IngredientLists.IngredientId");

            if (ModelState.IsValid)
            {
                recipe.UserId = currentUser.Id;
                _context.Add(recipe);
                newList.RecipeId = recipe.RecipeId;
                newList.IngredientId = ingredient.IngredientId;
                newList.Quantity = ingredList.Quantity;
                newList.Measurement = ingredList.Measurement;
                _context.Add(newList);
                await _context.SaveChangesAsync();
                return RedirectToAction("Build", new { id = recipe.RecipeId, Recip = viewModel });
            }



            //Adds a technique to the recipe
            viewModel.AvailableTech = await _context.Technique.ToListAsync();
            //Add an ingredient to the ingredient list
            viewModel.AvailableIngredients = await _context.Ingredient.ToListAsync();

            return View(viewModel);
        }

        // GET: Recipes/Build/5
        public async Task<IActionResult> Build(int? id)
        {
            //Get current user
            var currentUser = await GetCurrentUserAsync();

            //instantiate an instance of viewmodel.
            RecipeCreateViewModel viewModel = new RecipeCreateViewModel();

            //Check to see if id is null
            if (id == null)
            {
                return NotFound();
            }

            //Get the context of Recipe
            var recipe = await _context.Recipe
                .Include(r => r.Technique)
                .Include(r => r.User)
                .Include(r => r.IngredientLists)
                .ThenInclude(il => il.Ingredient)
                .FirstOrDefaultAsync(il => il.RecipeId == id);

            viewModel.Recipe = recipe;
            /*recipe = await _context.Recipe.FindAsync(id);*/
            if (recipe == null)
            {
                return NotFound();
            }

            var ingredList = await _context.IngredientList
                .Include(i => i.Ingredient)
                .FirstOrDefaultAsync(il => il.RecipeId == id);

            viewModel.IngredientLists = ingredList;

            //Add an ingredient to the ingredient list
            viewModel.AvailableIngredients = await _context.Ingredient.Where(i => i.User.Id == currentUser.Id).ToListAsync();
            viewModel.AvailableTech = await _context.Technique.Where(t => t.User.Id == currentUser.Id).ToListAsync();
            return View(viewModel);
        }

        //POST: Recipe/Build/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Build(int? id, RecipeCreateViewModel viewModel)
        {
            //Get current user
            var currentUser = await GetCurrentUserAsync();

            //Create instances of view model
            var ingredient = viewModel.Ingredient;
            var ingredientListObj = viewModel.IngredientLists;
            var recipeObj = viewModel.Recipe;

            //Instantiate a new list for storage of recipeId and IngredientId
            IngredientList newList = new IngredientList();

            //Get the context of ingredientlist from the database
            var ingredList = await _context.IngredientList
                .Include(i => i.Ingredient)
                .FirstOrDefaultAsync(il => il.RecipeId == id);

            viewModel.IngredientLists = ingredList;

            //Get the context of the recipe from database
            var recipe = await _context.Recipe
                .Include(r => r.Technique)
                .Include(r => r.User)
                .Include(r => r.IngredientLists)
                .ThenInclude(il => il.Ingredient)
                .FirstOrDefaultAsync(il => il.RecipeId == id);

            viewModel.Recipe = recipe;

            //Get the dropdowns for ingredients and available techniques
            viewModel.AvailableIngredients = await _context.Ingredient.Where(i => i.User.Id == currentUser.Id).ToListAsync();
            viewModel.AvailableTech = await _context.Technique.Where(t => t.User.Id == currentUser.Id).ToListAsync();

            //Check to se if the recipe id is the same as the id passed into the method
            if (id != recipe.RecipeId)
            {
                return NotFound();
            }

            //Made ModelState valid by removing invalid values
            ModelState.Remove("Recipe.User");
            ModelState.Remove("Recipe.UserId");
            ModelState.Remove("Recipe.Description");
            ModelState.Remove("Recipe.Directions");
            ModelState.Remove("Ingredient.Name");
            ModelState.Remove("Ingredient.User");
            ModelState.Remove("Ingredient.IngredientId");
            ModelState.Remove("IngredientLists.IngredientId");

            //check to see if the ModelState is valid
            if (ModelState.IsValid)
            {
                //Check to see if the ingredientId is valid. 0 is an invalid value for the ingredientId, if so, prompt a message to alert the user.
                if (ingredient.IngredientId == 0)
                {
                    ModelState.AddModelError("", "You must select an ingredient.");
                }
                //Check to see if the ingredient already exists on the ingredient list, if so, prompt a message to alert the user.
                else if (viewModel.IngredientLists.IngredientId == ingredient.IngredientId && viewModel.IngredientLists.RecipeId == recipe.RecipeId)
                {
                    ModelState.AddModelError("", "This ingredient is already in the recipe.");
                }
                //If both conditions have been passed make all the necessary changes that the user has made to the fields.
                else
                {
                    newList.RecipeId = recipe.RecipeId;
                    newList.IngredientId = ingredient.IngredientId;
                    newList.Quantity = ingredientListObj.Quantity;
                    newList.Measurement = ingredientListObj.Measurement;
                    recipe.Title = recipeObj.Title;
                    recipe.Description = recipeObj.Description;
                    recipe.UserId = currentUser.Id;
                    recipe.TechniqueId = recipeObj.TechniqueId;
                    recipe.Directions = recipeObj.Directions;
                    recipe.Comment = recipeObj.Comment;
                    _context.Update(recipe);
                    _context.Add(newList);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Build", new { id = recipe.RecipeId });
                }

            }

            //Check to see if the viewmodel is null
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //Get current user
            var currentUser = await GetCurrentUserAsync();

            RecipeEditViewModel viewModel = new RecipeEditViewModel();

            var recipe = await _context.Recipe.Include(r => r.Technique)
                                              .Include(r => r.User)
                                              .Include(r => r.IngredientLists)
                                              .ThenInclude(il => il.Ingredient)
                                              .FirstOrDefaultAsync(il => il.UserId == currentUser.Id && il.RecipeId == id);

            viewModel.Recipe = recipe;

            var ingredList = await _context.IngredientList
                .Include(i => i.Ingredient)
                .FirstOrDefaultAsync(il => il.RecipeId == id);

            viewModel.IngredientLists = ingredList;

            //Get the dropdowns for ingredients and available techniques
            viewModel.AvailableIngredients = await _context.Ingredient.Where(i => i.User.Id == currentUser.Id).ToListAsync();
            viewModel.AvailableTech = await _context.Technique.Where(t => t.User.Id == currentUser.Id).ToListAsync();

            if (id == null)
            {
                return NotFound();
            }

            if (recipe == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Recipes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RecipeEditViewModel viewModel)
        {
            //Get current user
            var currentUser = await GetCurrentUserAsync();

            //Create instances of view model for ingredient, ingredientlist, and recipe
            var ingredient = viewModel.Ingredient;
            var ingredientListObj = viewModel.IngredientLists;
            var recipeObj = viewModel.Recipe;

            //Instantiate a new list for storage of recipeId and IngredientId
            IngredientList newList = new IngredientList();

            //Check to see if the viewmodel is null
            if (viewModel == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe.Include(r => r.Technique)
                                              .Include(r => r.User)
                                              .Include(r => r.IngredientLists)
                                              .ThenInclude(il => il.Ingredient)
                                              .FirstOrDefaultAsync(il => il.UserId == currentUser.Id && il.RecipeId == id);

            viewModel.Recipe = recipe;

            var ingredList = await _context.IngredientList
                .Include(i => i.Ingredient)
                .FirstOrDefaultAsync(il => il.RecipeId == id);

            viewModel.IngredientLists = ingredList;

            //Get the dropdowns for ingredients and available techniques
            viewModel.AvailableIngredients = await _context.Ingredient.Where(i => i.User.Id == currentUser.Id).ToListAsync();
            viewModel.AvailableTech = await _context.Technique.Where(t => t.User.Id == currentUser.Id).ToListAsync();

            if (id != recipe.RecipeId)
            {
                return NotFound();
            }

            ModelState.Remove("Recipe.User");
            ModelState.Remove("Recipe.UserId");
            ModelState.Remove("Ingredient");
            ModelState.Remove("Ingredient.User");
            ModelState.Remove("Ingredient.IngredientId");
            ModelState.Remove("Ingredient.Name");

            if (ModelState.IsValid)
            {
                //Check to see if the ingredientId is valid. 0 is an invalid value for the ingredientId, if so, prompt a message to alert the user.
                if (ingredient.IngredientId == 0)
                {
                    ModelState.AddModelError("", "You must select an ingredient.");
                    return RedirectToAction("Edit", new { id = recipe.RecipeId });
                }
                else if (ingredient.IngredientId == viewModel.IngredientLists.IngredientId && recipeObj.RecipeId == viewModel.IngredientLists.RecipeId)
                {
                    ModelState.AddModelError("", "This ingredient is already on the recipe.");
                    return RedirectToAction("Edit", new { id = recipe.RecipeId });
                }
                //Check to see if the ingredient already exists on the ingredient list, if so, prompt a message to alert the user.
                else if (ingredient.IngredientId != ingredList.IngredientId && recipe.RecipeId == viewModel.IngredientLists.RecipeId)
                {
                    newList.RecipeId = recipe.RecipeId;
                    newList.IngredientId = ingredient.IngredientId;
                    newList.Quantity = ingredientListObj.Quantity;
                    newList.Measurement = ingredientListObj.Measurement;
                    recipe.Title = recipeObj.Title;
                    recipe.Description = recipeObj.Description;
                    recipe.UserId = currentUser.Id;
                    recipe.TechniqueId = recipeObj.TechniqueId;
                    recipe.Directions = recipeObj.Directions;
                    recipe.Favorite = recipeObj.Favorite;
                    recipe.Comment = recipeObj.Comment;
                    _context.Update(recipe);
                    _context.Add(newList);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Edit", new { id = recipe.RecipeId });
                }
                //If both conditions have been passed make all the necessary changes that the user has made to the fields.
                else
                {
                    return NotFound();
                }
                /*return RedirectToAction(nameof(Index));*/
            }
            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteIngredient(int id, RecipeEditViewModel viewModel)
        {
            //Get current user
            /*var currentUser = await GetCurrentUserAsync();*/

            //Create instances of view model
            var recipeObj = viewModel.Recipe;
            var ingredientObj = viewModel.Ingredient;
            var ingrListObj = viewModel.IngredientLists;

            /*//Instantiate a new list so ingredients can be added if necessary
            var IngredList = viewModel.IngredientLists;*/

            //Check to see if the viewmodel is null
            if (viewModel == null)
            {
                return NotFound();
            }

            /*if (ModelState.IsValid)
            {
                newList.RecipeId = recipeObj.RecipeId;
                newList.IngredientId = ingredientObj.IngredientId;
                newList.Quantity = ingrListObj.Quantity;
                newList.Measurement = ingrListObj.Measurement;
                _context.Add(newList);
                await _context.SaveChangesAsync();
            }*/


            var ingredList = await _context.IngredientList.FindAsync(id);
            _context.IngredientList.Remove(ingredList);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            /*return RedirectToAction("Edit", new { recipeObj.RecipeId });*/
        }
        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.Technique)
                .Include(r => r.User)
                .Include(r => r.IngredientLists)
                .ThenInclude(il => il.Recipe)
                .FirstOrDefaultAsync(m => m.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var currentUser = await GetCurrentUserAsync();*/
            var recipe = await _context.Recipe.FindAsync(id);
            var ingredientLists = _context.IngredientList;
            _context.Recipe.Remove(recipe);

            foreach (IngredientList item in ingredientLists)
            {
                if (item.RecipeId == recipe.RecipeId)
                {
                    ingredientLists.Remove(item);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /*// POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await GetCurrentUserAsync();
            var userid = user.Id;
            var order = await _context.Order.FindAsync(id);
            var orderProducts = _context.OrderProduct;
            var products = _context.Product;


            foreach (OrderProduct item in orderProducts)
            {
                if (item.OrderId == order.OrderId && userid == order.UserId)
                {
                    orderProducts.Remove(item);
                }
            }
            if (userid == order.UserId)
            {
                _context.Order.Remove(order);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }*/

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.RecipeId == id);
        }
    }
}
