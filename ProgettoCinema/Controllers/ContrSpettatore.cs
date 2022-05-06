using CinemaLib.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoCinema.ClientWeb.Data;

namespace ProgettoCinema.WebClient.Controller
{
    public class SpettatoreController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly CinemaDbContext _context;

        public SpettatoreController(CinemaDbContext context)
        {
            _context = context;
        }

        // GET: Spettatore
        public async Task<IActionResult> Index()
        {
            return View(await _context.Spettatori.ToListAsync());
        }

        // GET: Spettatore/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spettatore = await _context.Spettatori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spettatore == null)
            {
                return NotFound();
            }

            return View(spettatore);
        }

        // GET: Spettatore/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spettatore/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Birthdate,TicketId,OverSeventyYear,UnderFiveYear,Id")] Spettatore spettatore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spettatore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spettatore);
        }

        // GET: Spettatore/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spettatore = await _context.Spettatori.FindAsync(id);
            if (spettatore == null)
            {
                return NotFound();
            }
            return View(spettatore);
        }

        // POST: Spettatore/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Surname,Birthdate,TicketId,OverSeventyYear,UnderFiveYear,Id")] Spettatore spettatore)
        {
            if (id != spettatore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spettatore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpettatoreExists(spettatore.Id))
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
            return View(spettatore);
        }

        // GET: Spettatore/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spettatore = await _context.Spettatori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spettatore == null)
            {
                return NotFound();
            }

            return View(spettatore);
        }

        // POST: Spettatore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spettatore = await _context.Spettatori.FindAsync(id);
            _context.Spettatori.Remove(spettatore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpettatoreExists(int id)
        {
            return _context.Spettatori.Any(e => e.Id == id);
        }
    }
}

