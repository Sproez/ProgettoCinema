using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoCinema.ClientWeb.Data;

namespace ProgettoCinema.WebClient.Controller
{
    public class GenereFilmController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly CinemaDbContext _context;

        public GenereFilmController(CinemaDbContext context)
        {
            _context = context;
        }

        // GET: GenereFilm
        public async Task<IActionResult> Index()
        {
            return View(await _context.GenereFilm.ToListAsync());
        }

        // GET: GenereFilm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genereFilm = await _context.GenereFilm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genereFilm == null)
            {
                return NotFound();
            }

            return View(genereFilm);
        }

        // GET: GenereFilm/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GenereFilm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilmGenre,Id")] GenereFilm genereFilm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genereFilm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genereFilm);
        }

        // GET: GenereFilm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genereFilm = await _context.GenereFilm.FindAsync(id);
            if (genereFilm == null)
            {
                return NotFound();
            }
            return View(genereFilm);
        }

        // POST: GenereFilm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilmGenre,Id")] GenereFilm genereFilm)
        {
            if (id != genereFilm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genereFilm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenereFilmExists(genereFilm.Id))
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
            return View(genereFilm);
        }

        // GET: GenereFilm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genereFilm = await _context.GenereFilm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genereFilm == null)
            {
                return NotFound();
            }

            return View(genereFilm);
        }

        // POST: GenereFilm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genereFilm = await _context.GenereFilm.FindAsync(id);
            _context.GenereFilm.Remove(genereFilm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenereFilmExists(int id)
        {
            return _context.GenereFilm.Any(e => e.Id == id);
        }
    }
}

