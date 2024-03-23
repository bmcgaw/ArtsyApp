using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArtsyApp.Data;
using ArtsyApp.Models;

namespace ArtsyApp.Controllers
{
    public class ArtWorksController : Controller
    {
        private readonly ArtsyAppContext _context;

        public ArtWorksController(ArtsyAppContext context)
        {
            _context = context;
        }

        // GET: ArtWorksModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArtWorksModel.ToListAsync());
        }

        // GET: ArtWorksModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artWorksModel = await _context.ArtWorksModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artWorksModel == null)
            {
                return NotFound();
            }

            return View(artWorksModel);
        }

        // GET: ArtWorksModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArtWorksModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Artist,Category,CreationDate")] ArtWorksModel artWorksModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artWorksModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artWorksModel);
        }

        // GET: ArtWorksModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artWorksModel = await _context.ArtWorksModel.FindAsync(id);
            if (artWorksModel == null)
            {
                return NotFound();
            }
            return View(artWorksModel);
        }

        // POST: ArtWorksModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Artist,Category,CreationDate")] ArtWorksModel artWorksModel)
        {
            if (id != artWorksModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artWorksModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtWorksModelExists(artWorksModel.Id))
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
            return View(artWorksModel);
        }

        // GET: ArtWorksModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artWorksModel = await _context.ArtWorksModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artWorksModel == null)
            {
                return NotFound();
            }

            return View(artWorksModel);
        }

        // POST: ArtWorksModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artWorksModel = await _context.ArtWorksModel.FindAsync(id);
            if (artWorksModel != null)
            {
                _context.ArtWorksModel.Remove(artWorksModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtWorksModelExists(int id)
        {
            return _context.ArtWorksModel.Any(e => e.Id == id);
        }
    }
}
