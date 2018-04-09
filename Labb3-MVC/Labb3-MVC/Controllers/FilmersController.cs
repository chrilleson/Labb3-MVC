using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb3MVC.Models;
using Labb3MVC.Data;

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
        public async Task<IActionResult> Index(string SortOrder)
        {
            ViewData["NamnSortParam"] = SortOrder == "namn" ? "namn_desc" : "namn";
            ViewData["BiljettSortParam"] = SortOrder == "biljetter" ? "biljetter_desc" : "biljetter";
            ViewData["ShowTimeSortParam"] = SortOrder == "showtime" ? "showtime_desc" : "showtime";
            ViewData["SalongSortParam"] = SortOrder == "salong" ? "salong_desc" : "salong";

            var sortQuery = from m in _context.Filmer.Include(s => s.Salong)
                            select m;

            switch (SortOrder)
            {
                case "namn":
                    sortQuery = sortQuery.OrderBy(m => m.Namn);
                    break;
                case "namn_desc":
                    sortQuery = sortQuery.OrderByDescending(m => m.Namn);
                    break;
                case "biljetter":
                    sortQuery = sortQuery.OrderBy(m => m.Biljetter);
                    break;
                case "biljetter_desc":
                    sortQuery = sortQuery.OrderByDescending(m => m.Biljetter);
                    break;
                case "showtime":
                    sortQuery = sortQuery.OrderBy(m => m.ShowTime);
                    break;
                case "showtime_desc":
                    sortQuery = sortQuery.OrderByDescending(m => m.ShowTime);
                    break;
                case "salong":
                    sortQuery = sortQuery.OrderBy(m => m.Salong);
                    break;
                case "salong_desc":
                    sortQuery = sortQuery.OrderByDescending(m => m.Salong);
                    break;
                default:
                    sortQuery = sortQuery.OrderBy(m => m.ShowTime);
                    break;
            }

            return View(await sortQuery.AsNoTracking().ToListAsync());
        }

        // GET: Filmers/Köp/5
        [HttpGet]
        public async Task<IActionResult> Köp(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmer = _context.Filmer.Include(s => s.Salong).SingleOrDefaultAsync(f => f.Id == id);

            if (filmer == null)
            {
                return NotFound();
            }

            return View(await filmer);
        }

        // Post: Filmers/Köp/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Köp(int id, Filmer validator)
        {
            var filmer = await _context.Filmer.Include(s => s.Salong).SingleOrDefaultAsync(f => f.Id == id);

            if(id != filmer.Id)
            {
                NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if(validator.BiljettValidator > filmer.Biljetter)
                    {
                        ModelState.AddModelError(string.Empty, "Finns inte tillräckligt många biljetter");
                        return View(filmer);
                    }
                    else
                    {
                        filmer.Biljetter = filmer.Biljetter - validator.BiljettValidator;

                        if(filmer.Biljetter >= 0)
                        {
                            filmer.BiljettValidator = validator.BiljettValidator;
                            _context.Update(filmer);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            return View(filmer);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmerExists(filmer.Id))
                    {
                        NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(BiljettBekräftelse), new { id = filmer.Id, KöptaBiljetter = filmer.BiljettValidator });
            }
            return View(filmer);
        }

        //Get: filmer/biljettbekräftelse/id
        public async Task<IActionResult> BiljettBekräftelse(int? id, int köptaBiljetter)
        {
            if (id == null)
            {
                NotFound();
            }

            var filmer = await _context.Filmer.Include(s => s.Salong).SingleOrDefaultAsync(f => f.Id == id);
            filmer.BiljettValidator = köptaBiljetter;

            if(filmer == null)
            {
                NotFound();
            }
            return View(filmer);
        }
       
        private bool FilmerExists(int id)
        {
            return _context.Filmer.Any(e => e.Id == id);
        }
    }
}
