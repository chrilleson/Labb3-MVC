using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb3MVC.Models;

namespace Labb3MVC.Controllers
{
    public class FilmersController : Controller
    {
        private readonly Labb3MVCContext _context;

        public FilmersController(Labb3MVCContext context)
        {
            _context = context;
        }

        // GET: Filmers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Filmer.ToListAsync());
        }

        // GET: Filmers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmer = await _context.Filmer
                .SingleOrDefaultAsync(m => m.Id == id);
            if (filmer == null)
            {
                return NotFound();
            }

            return View(filmer);
        }

        // GET: Filmers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filmers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Namn,Biljetter,ShowTime")] Filmer filmer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filmer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filmer);
        }

        // GET: Filmers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmer = await _context.Filmer.SingleOrDefaultAsync(m => m.Id == id);
            if (filmer == null)
            {
                return NotFound();
            }
            return View(filmer);
        }

        // POST: Filmers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Namn,Biljetter,ShowTime")] Filmer filmer)
        {
            if (id != filmer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filmer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmerExists(filmer.Id))
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
            return View(filmer);
        }

        // GET: Filmers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmer = await _context.Filmer
                .SingleOrDefaultAsync(m => m.Id == id);
            if (filmer == null)
            {
                return NotFound();
            }

            return View(filmer);
        }

        // POST: Filmers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filmer = await _context.Filmer.SingleOrDefaultAsync(m => m.Id == id);
            _context.Filmer.Remove(filmer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmerExists(int id)
        {
            return _context.Filmer.Any(e => e.Id == id);
        }
    }
}
