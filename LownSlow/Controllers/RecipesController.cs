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

namespace LownSlow.Controllers
{
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
            var applicationDbContext = _context.Recipe.Include(r => r.IngredientLists).Where(r => r.UserId == currentUser.Id);
            return View(await applicationDbContext.ToListAsync());
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
                return RedirectToAction("Build", new { id = recipe.RecipeId });
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

            RecipeCreateViewModel viewModel = new RecipeCreateViewModel();
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .Include(r => r.Technique)
                .Include(r => r.User)
                .Include(r => r.IngredientLists)
                .ThenInclude(il => il.Ingredient)
                .FirstOrDefaultAsync(il => il.RecipeId == id);

            /*recipe = await _context.Recipe.FindAsync(id);*/
            if (recipe == null)
            {
                return NotFound();
            }

            var ingredList = await _context.IngredientList
                .Include(i => i.Ingredient)
                .FirstOrDefaultAsync(il => il.RecipeId == id);

            viewModel.Recipe = recipe;
            viewModel.IngredientLists = ingredList;
            //Add an ingredient to the ingredient list
            viewModel.AvailableIngredients = await _context.Ingredient.Where(i => i.User.Id == currentUser.Id).ToListAsync();

            return View(viewModel);
        }

        //POST: Recipe/Build/12
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Build(int? id, RecipeCreateViewModel viewModel)
        {
            var recipe = viewModel.Recipe;
            var ingredient = viewModel.Ingredient;
            var ingredList = viewModel.IngredientLists;

            IngredientList newList = new IngredientList();

            if (id != recipe.RecipeId)
            {
                return NotFound();
            }
            ModelState.Remove("Recipe.User");
            ModelState.Remove("Recipe.UserId");
            ModelState.Remove("Recipe.Description");
            ModelState.Remove("Ingredient.Name");
            ModelState.Remove("Ingredient.User");
            ModelState.Remove("IngredientLists.IngredientId");

            if (ModelState.IsValid)
            {
                newList.RecipeId = recipe.RecipeId;
                newList.IngredientId = ingredient.IngredientId;
                newList.Quantity = ingredList.Quantity;
                newList.Measurement = ingredList.Measurement;
                _context.Add(newList);
                await _context.SaveChangesAsync();
                return RedirectToAction("Build", new { id = recipe.RecipeId });
            }

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Recipe recipe)
        {
            if (id != recipe.RecipeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.RecipeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
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
                .ThenInclude(il => il.IngredientListId)
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
            var recipe = await _context.Recipe.FindAsync(id);
            _context.Recipe.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.RecipeId == id);
        }
    }
}
