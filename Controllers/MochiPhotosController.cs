using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mochilog.Models;
using Mochilog.Data;
using Microsoft.AspNetCore.Authorization;

namespace Mochilog.Controllers
{
    [Authorize]
    public class MochiPhotosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MochiPhotosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MochiPhotos
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.MochiPhoto.ToListAsync());
        }

        // GET: MochiPhotos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MochiPhotos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MochiPhoto mochiPhoto, IFormFile? ImageUpload)
        {
            if (ImageUpload != null && ImageUpload.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await ImageUpload.CopyToAsync(memoryStream);
                mochiPhoto.ImageData = memoryStream.ToArray();
            }

            if (ModelState.IsValid)
            {
                _context.Add(mochiPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mochiPhoto);
        }

        // GET: MochiPhotos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mochiPhoto = await _context.MochiPhoto.FindAsync(id);
            if (mochiPhoto == null)
            {
                return NotFound();
            }
            return View(mochiPhoto);
        }

        // POST: MochiPhotos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MochiPhoto mochiPhoto, IFormFile ImageUpload)
        {
            if (id != mochiPhoto.Id)
            {
                return NotFound();
            }

            var existingPhoto = await _context.MochiPhoto.FindAsync(id);
            if (existingPhoto == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(mochiPhoto);
            }

            existingPhoto.Title = mochiPhoto.Title;
            existingPhoto.PicTakenDate = mochiPhoto.PicTakenDate;
            existingPhoto.UploadDate = mochiPhoto.UploadDate;

            if (ImageUpload != null && ImageUpload.Length > 0)
            {
                using var ms = new MemoryStream();
                await ImageUpload.CopyToAsync(ms);
                existingPhoto.ImageData = ms.ToArray();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: MochiPhotos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mochiPhoto = await _context.MochiPhoto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mochiPhoto == null)
            {
                return NotFound();
            }

            return View(mochiPhoto);
        }

        // POST: MochiPhotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mochiPhoto = await _context.MochiPhoto.FindAsync(id);
            if (mochiPhoto != null)
            {
                _context.MochiPhoto.Remove(mochiPhoto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetImage(int id)
        {
            var mochi = await _context.MochiPhoto.FindAsync(id);
            if (mochi == null || mochi.ImageData == null) return NotFound();
            return File(mochi.ImageData, "image/jpeg");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetImageIds()
        {
            var ids = _context.MochiPhoto.Select(m => m.Id).ToList();
            return Json(ids);
        }
    }
}
