using CinemaLib.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgettoCinema.ClientWeb.Data;

namespace ProgettoCinema.WebClient.Controller
{
    public class SalaCinematograficaController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly CinemaDbContext _context;

        public SalaCinematograficaController(CinemaDbContext context)
        {
            _context = context;
        }

        // GET: SalaCinematografica
        public async Task<IActionResult> Index()
        {
            var cinemaDbContext = _context.SaleCinematografiche.Include(s => s.Cinema).Include(s => s.Film);
            return View(await cinemaDbContext.ToListAsync());
        }

        // GET: SalaCinematografica/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaCinematografica = await _context.SaleCinematografiche
                .Include(s => s.Cinema)
                .Include(s => s.Film)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salaCinematografica == null)
            {
                return NotFound();
            }

            return View(salaCinematografica);
        }

        // GET: SalaCinematografica/Create
        public IActionResult Create()
        {
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "Name");
            ViewData["FilmId"] = new SelectList(_context.Movies, "Id", "Author");
            return View();
        }

        // POST: SalaCinematografica/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,RoomCapacity,OccupiedSeats,CinemaId,FilmId,Id")] SalaCinematografica salaCinematografica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salaCinematografica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "Name", salaCinematografica.CinemaId);
            ViewData["FilmId"] = new SelectList(_context.Movies, "Id", "Author", salaCinematografica.FilmId);
            return View(salaCinematografica);
        }

        // GET: SalaCinematografica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaCinematografica = await _context.SaleCinematografiche.FindAsync(id);
            if (salaCinematografica == null)
            {
                return NotFound();
            }
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "Name", salaCinematografica.CinemaId);
            ViewData["FilmId"] = new SelectList(_context.Movies, "Id", "Author", salaCinematografica.FilmId);
            return View(salaCinematografica);
        }

        // POST: SalaCinematografica/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,RoomCapacity,OccupiedSeats,CinemaId,FilmId,Id")] SalaCinematografica salaCinematografica)
        {
            if (id != salaCinematografica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salaCinematografica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaCinematograficaExists(salaCinematografica.Id))
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
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "Name", salaCinematografica.CinemaId);
            ViewData["FilmId"] = new SelectList(_context.Movies, "Id", "Author", salaCinematografica.FilmId);
            return View(salaCinematografica);
        }

        // GET: SalaCinematografica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salaCinematografica = await _context.SaleCinematografiche
                .Include(s => s.Cinema)
                .Include(s => s.Film)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salaCinematografica == null)
            {
                return NotFound();
            }

            return View(salaCinematografica);
        }

        // POST: SalaCinematografica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salaCinematografica = await _context.SaleCinematografiche.FindAsync(id);
            _context.SaleCinematografiche.Remove(salaCinematografica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaCinematograficaExists(int id)
        {
            return _context.SaleCinematografiche.Any(e => e.Id == id);
        }
    }
}

