using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mochilog.Models;
using Mochilog.Data;

namespace Mochilog.Controllers
{
    public class MochiPhotosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MochiPhotosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MochiPhotos
        public async Task<IActionResult> Index()
        {
            return View(await _context.MochiPhoto.ToListAsync());
        }

        // GET: MochiPhotos/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,PicTakenDate,UploadDate,ImageFileName")] MochiPhoto mochiPhoto)
        {
            if (id != mochiPhoto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mochiPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MochiPhotoExists(mochiPhoto.Id))
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
            return View(mochiPhoto);
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

        private bool MochiPhotoExists(int id)
        {
            return _context.MochiPhoto.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GetImage(int id)
        {
            var mochi = await _context.MochiPhoto.FindAsync(id);
            if (mochi == null || mochi.ImageData == null) return NotFound();
            return File(mochi.ImageData, "image/jpeg");
        }
    }
}
