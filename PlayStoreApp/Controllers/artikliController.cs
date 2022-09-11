using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlayStoreApp.Data;
using PlayStoreApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace PlayStoreApp.Controllers
{
    public class artikliController : Controller
    {
        private readonly ApplicationDbContext _context;

        public artikliController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: artikli
        public async Task<IActionResult> Index(string search)
        {
            if(!String.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                var artikli = from artikli in _context.artikli
                              select artikli;
                
                }
           
            return View(await _context.artikli.ToListAsync());
        }

        // GET: artikli/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikli = await _context.artikli
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artikli == null)
            {
                return NotFound();
            }

            return View(artikli);
        }

        // GET: artikli/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: artikli/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Cijena,Proizvođač,Dostupnost")] artikli artikli)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artikli);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artikli);
        }

        // GET: artikli/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikli = await _context.artikli.FindAsync(id);
            if (artikli == null)
            {
                return NotFound();
            }
            return View(artikli);
        }

        // POST: artikli/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Cijena,Proizvođač,Dostupnost")] artikli artikli)
        {
            if (id != artikli.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artikli);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!artikliExists(artikli.Id))
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
            return View(artikli);
        }

        // GET: artikli/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikli = await _context.artikli
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artikli == null)
            {
                return NotFound();
            }

            return View(artikli);
        }

        // POST: artikli/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artikli = await _context.artikli.FindAsync(id);
            _context.artikli.Remove(artikli);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool artikliExists(int id)
        {
            return _context.artikli.Any(e => e.Id == id);
        }
    }
}
