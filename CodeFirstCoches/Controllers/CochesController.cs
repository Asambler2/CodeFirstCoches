using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeFirstCoches.Models;

namespace CodeFirstCoches.Controllers
{
    public class CochesController : Controller
    {
        private readonly CochesContext _context;

        public CochesController(CochesContext context)
        {
            _context = context;
        }

        // GET: Coches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coches.ToListAsync());
        }

        // GET: Coches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coche = await _context.Coches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coche == null)
            {
                return NotFound();
            }

            return View(coche);
        }

        // GET: Coches/Create
        public IActionResult Create()
        {
            ViewData["EscuderisrId"] = new SelectList(_context.Coches, "Id", "Nombre");
            return View();
        }

        // POST: Coches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Motor,EscuderisrId")] Coche coche)
        {
            _context.Add(coche);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Coches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coche = await _context.Coches.FindAsync(id);
            if (coche == null)
            {
                return NotFound();
            }
            return View(coche);
        }

        // POST: Coches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Motor,EscuderisrId")] Coche coche)
        {
            if (id != coche.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coche);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CocheExists(coche.Id))
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
            return View(coche);
        }

        // GET: Coches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coche = await _context.Coches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coche == null)
            {
                return NotFound();
            }

            return View(coche);
        }

        // POST: Coches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coche = await _context.Coches.FindAsync(id);
            if (coche != null)
            {
                _context.Coches.Remove(coche);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CocheExists(int id)
        {
            return _context.Coches.Any(e => e.Id == id);
        }
    }
}
