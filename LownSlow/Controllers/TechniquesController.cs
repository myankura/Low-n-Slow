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

namespace LownSlow.Controllers
{
    public class TechniquesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TechniquesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Techniques
        public async Task<IActionResult> Index()
        {
            //Get current user's UserId
            var currentUser = await GetCurrentUserAsync();
            var userTechniques = _context.Technique.Where(t => t.UserId == currentUser.Id);

            return View(await userTechniques.ToListAsync());
        }

        // GET: Techniques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technique = await _context.Technique
                .FirstOrDefaultAsync(m => m.TechniqueId == id);
            if (technique == null)
            {
                return NotFound();
            }

            return View(technique);
        }

        // GET: Techniques/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Techniques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TechniqueId,Name,Description,UserId")] Technique technique)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technique);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technique);
        }

        // GET: Techniques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technique = await _context.Technique.FindAsync(id);
            if (technique == null)
            {
                return NotFound();
            }
            return View(technique);
        }

        // POST: Techniques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TechniqueId,Name,Description,UserId")] Technique technique)
        {
            if (id != technique.TechniqueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technique);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechniqueExists(technique.TechniqueId))
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
            return View(technique);
        }

        // GET: Techniques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technique = await _context.Technique
                .FirstOrDefaultAsync(m => m.TechniqueId == id);
            if (technique == null)
            {
                return NotFound();
            }

            return View(technique);
        }

        // POST: Techniques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technique = await _context.Technique.FindAsync(id);
            _context.Technique.Remove(technique);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechniqueExists(int id)
        {
            return _context.Technique.Any(e => e.TechniqueId == id);
        }
    }
}
