using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mochilog.Data;
using Mochilog.Models;

namespace Mochilog.Controllers
{
    public class MochiPhotosController : Controller
    {
        private readonly AppDbContext _context;

        public MochiPhotosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MochiPhotos
        public async Task<IActionResult> Index()
        {
            return View(await _context.MochiPhotos.ToListAsync());
        }

        // GET: MochiPhotos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mochiPhoto = await _context.MochiPhotos
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
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ImageFileName,UploadedAt")] MochiPhoto mochiPhoto)
        {
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

            var mochiPhoto = await _context.MochiPhotos.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageFileName,UploadedAt")] MochiPhoto mochiPhoto)
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

            var mochiPhoto = await _context.MochiPhotos
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
            var mochiPhoto = await _context.MochiPhotos.FindAsync(id);
            if (mochiPhoto != null)
            {
                _context.MochiPhotos.Remove(mochiPhoto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MochiPhotoExists(int id)
        {
            return _context.MochiPhotos.Any(e => e.Id == id);
        }
    }
}
