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
    public class ArtPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArtPosts
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArtPost.ToListAsync());
        }

        // GET: ArtPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artPost = await _context.ArtPost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artPost == null)
            {
                return NotFound();
            }

            return View(artPost);
        }

        // GET: ArtPosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArtPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Artist,PhotoUrl,Description")] ArtPost artPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artPost);
        }

        // GET: ArtPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artPost = await _context.ArtPost.FindAsync(id);
            if (artPost == null)
            {
                return NotFound();
            }
            return View(artPost);
        }

        // POST: ArtPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Artist,PhotoUrl,Description")] ArtPost artPost)
        {
            if (id != artPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtPostExists(artPost.Id))
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
            return View(artPost);
        }

        // GET: ArtPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artPost = await _context.ArtPost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artPost == null)
            {
                return NotFound();
            }

            return View(artPost);
        }

        // POST: ArtPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artPost = await _context.ArtPost.FindAsync(id);
            if (artPost != null)
            {
                _context.ArtPost.Remove(artPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtPostExists(int id)
        {
            return _context.ArtPost.Any(e => e.Id == id);
        }
    }
}
