using CinemaLib.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgettoCinema.ClientWeb.Data;

namespace ProgettoCinema.WebClient.Controller
{
    public class FilmController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly CinemaDbContext _context;

        public FilmController(CinemaDbContext context)
        {
            _context = context;
        }

        // GET: Film
        public async Task<IActionResult> Index()
        {
            var cinemaDbContext = _context.Film.Include(f => f.Genere);
            return View(await cinemaDbContext.ToListAsync());
        }

        // GET: Film/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(f => f.Genere)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Film/Create
        public IActionResult Create()
        {
            ViewData["FilmGenreId"] = new SelectList(_context.GenereFilm, "Id", "FilmGenre");
            return View();
        }

        // POST: Film/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,Producer,salaId,FilmGenreId,Duration,Id")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmGenreId"] = new SelectList(_context.GenereFilm, "Id", "FilmGenre", film.Genere);
            return View(film);
        }

        // GET: Film/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["FilmGenreId"] = new SelectList(_context.GenereFilm, "Id", "FilmGenre", film.Genere);
            return View(film);
        }

        // POST: Film/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Author,Producer,salaId,FilmGenreId,Duration,Id")] Film film)
        {
            if (id != film.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id))
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
            ViewData["FilmGenreId"] = new SelectList(_context.GenereFilm, "Id", "FilmGenre", film.Genere);
            return View(film);
        }

        // GET: Film/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .Include(f => f.Genere)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Film/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Film.FindAsync(id);
            _context.Film.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.Id == id);
        }
    }
}

