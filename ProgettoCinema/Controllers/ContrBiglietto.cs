using CinemaLib.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgettoCinema.ClientWeb.Data;

namespace ProgettoCinema.WebClient.Controller
{
    public class BigliettoController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly CinemaDbContext _context;

        public BigliettoController(CinemaDbContext context)
        {
            _context = context;
        }

        // GET: Biglietto
        public async Task<IActionResult> Index()
        {
            var cinemaDbContext = _context.Biglietti.Include(b => b.SaleCinematografiche).Include(b => b.Cliente);
            return View(await cinemaDbContext.ToListAsync());
        }

        // GET: Biglietto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biglietto = await _context.Biglietti
                .Include(b => b.SaleCinematografiche)
                .Include(b => b.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (biglietto == null)
            {
                return NotFound();
            }

            return View(biglietto);
        }

        // GET: Biglietto/Create
        public IActionResult Create()
        {
            ViewData["salaId"] = new SelectList(_context.SaleCinematografiche, "Id", "Name");
            ViewData["PersonId"] = new SelectList(_context.Spettatori, "Id", "Name");
            return View();
        }

        // POST: Biglietto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Seat,Price,salaId,PersonId,Id")] Biglietto biglietto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(biglietto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["salaId"] = new SelectList(_context.SaleCinematografiche, "Id", "Name", biglietto.salaId);
            ViewData["PersonId"] = new SelectList(_context.Spettatori, "Id", "Name", biglietto.Cliente);
            return View(biglietto);
        }

        // GET: Biglietto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biglietto = await _context.Biglietti.FindAsync(id);
            if (biglietto == null)
            {
                return NotFound();
            }
            ViewData["salaId"] = new SelectList(_context.SaleCinematografiche, "Id", "Name", biglietto.salaId);
            ViewData["PersonId"] = new SelectList(_context.Spettatori, "Id", "Name", biglietto.Cliente);
            return View(biglietto);
        }

        // POST: Biglietto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Seat,Price,salaId,PersonId,Id")] Biglietto biglietto)
        {
            if (id != biglietto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(biglietto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BigliettoExists(biglietto.Id))
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
            ViewData["salaId"] = new SelectList(_context.SaleCinematografiche, "Id", "Name", biglietto.salaId);
            ViewData["PersonId"] = new SelectList(_context.Spettatori, "Id", "Name", biglietto.Cliente);
            return View(biglietto);
        }

        // GET: Biglietto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biglietto = await _context.Biglietti
                .Include(b => b.salaId)
                .Include(b => b.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (biglietto == null)
            {
                return NotFound();
            }

            return View(biglietto);
        }

        // POST: Biglietto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var biglietto = await _context.Biglietti.FindAsync(id);
            _context.Biglietti.Remove(biglietto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BigliettoExists(int id)
        {
            return _context.Biglietti.Any(e => e.Id == id);
        }
    }
}

