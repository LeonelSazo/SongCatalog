using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SongCatalog.Data;
using SongCatalog.Models;

namespace SongCatalog
{
    public class TbsongController : Controller
    {
        private readonly EFTbsongContext _context;

        public TbsongController(EFTbsongContext context)
        {
            _context = context;
        }

        // GET: Tbsong
        public async Task<IActionResult> Index()
        {
              return _context.Tbsong != null ? 
                          View(await _context.Tbsong.ToListAsync()) :
                          Problem("Entity set 'EFTbsongContext.Tbsong'  is null.");
        }

        // GET: Tbsong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tbsong == null)
            {
                return NotFound();
            }

            var tbsong = await _context.Tbsong
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbsong == null)
            {
                return NotFound();
            }

            return View(tbsong);
        }

        // GET: Tbsong/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tbsong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombrecancion,Genero,Artista,Fechalanzamiento,Duracion,Calificacion")] Tbsong tbsong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbsong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbsong);
        }

        // GET: Tbsong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tbsong == null)
            {
                return NotFound();
            }

            var tbsong = await _context.Tbsong.FindAsync(id);
            if (tbsong == null)
            {
                return NotFound();
            }
            return View(tbsong);
        }

        // POST: Tbsong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombrecancion,Genero,Artista,Fechalanzamiento,Duracion,Calificacion")] Tbsong tbsong)
        {
            if (id != tbsong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbsong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbsongExists(tbsong.Id))
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
            return View(tbsong);
        }

        // GET: Tbsong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tbsong == null)
            {
                return NotFound();
            }

            var tbsong = await _context.Tbsong
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbsong == null)
            {
                return NotFound();
            }

            return View(tbsong);
        }

        // POST: Tbsong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tbsong == null)
            {
                return Problem("Entity set 'EFTbsongContext.Tbsong'  is null.");
            }
            var tbsong = await _context.Tbsong.FindAsync(id);
            if (tbsong != null)
            {
                _context.Tbsong.Remove(tbsong);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbsongExists(int id)
        {
          return (_context.Tbsong?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
