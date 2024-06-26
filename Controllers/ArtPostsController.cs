﻿using ArtsyApp.Data;
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

        public async Task<IActionResult> MainFeed()
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
        public async Task<IActionResult> Create([Bind("Id,UserId,Title,Artist,ImagePath,ImageFileName,PostDate,Description")] ArtPost artPost, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {

                artPost.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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

                    artPost.ImageFileName = fileName;
                    artPost.ImagePath = "/images/" + fileName;

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Title,Artist,ImagePath,ImageFileName,PostDate,Description")] ArtPost artPost, IFormFile? imageFile)
        {
            if (id != artPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, artPost.ImagePath);
                if (imageFile != null && imageFile.Length > 0)
                {
                    var imagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(imagesFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    artPost.ImageFileName = fileName;
                    artPost.ImagePath = "/images/" + fileName;

                    System.IO.File.Delete($"../ArtsyApp/wwwroot/{imagePath}");
                    _context.ArtPost.Remove(artPost);
                }

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

        // POST: ArtPosts/Delete/4
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artPost = await _context.ArtPost.FindAsync(id);
            if (artPost != null)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, artPost.ImagePath);
                System.IO.File.Delete($"../ArtsyApp/wwwroot/{imagePath}");
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
