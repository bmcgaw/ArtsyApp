using ArtsyApp.Data;
using ArtsyApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArtsyApp.Controllers
{
    public class ArtPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArtPostsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create([Bind("Id,Title,Artist,ImagePath,Description")] ArtPost artPost, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != null) artPost.UserId = userId;
                artPost.PostDate = DateTime.Now;

                if (imageFile != null || imageFile?.Length > 0)
                {

                    var imagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(imagesFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    artPost.ImagePath = filePath;

                    _context.Add(artPost);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
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
