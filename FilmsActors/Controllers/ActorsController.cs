using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FilmsActors.Data;
using FilmsActors.Models;
using Microsoft.AspNetCore.Identity;

namespace FilmsActors.Controllers
{
    public class ActorsController : Controller
    {
        private static bool PosPressed = false;
        private static bool NegPressed = false;
        private readonly ApplicationDbContext _context;


        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //-----------------------------------------------------------------
        public async Task<IActionResult> FilmsIncluded(int ID)   //***
        {
            var filmsId = _context.FilmsActorsMod.Where(j => j.ActorID == ID).Select(j => j.FilmID).ToList();
            var films = await _context.Film.Where(j => filmsId.Contains(j.ID)).ToListAsync();
            return View("View", films);
        }
        public async Task<IActionResult> AlphabeticalView()   //***
        {
            return View("Index", await _context.Actor.OrderBy(j => j.Name).ToListAsync());
        }
        public async Task<IActionResult> PopularityView()   //***
        {
            return View("Index", await _context.Film.OrderBy(j => j.PosRating).ToListAsync());
        }
        public async Task<IActionResult> ShowSearchResults(string SearchName)   //***
        {
            return View("Index", await _context.Actor.Where(j => j.Name.Contains(SearchName)).ToListAsync());
        }
        // + reputation
        public async Task<IActionResult> PlusRating(int? id)   //***
        {
            var actor = _context.Actor.Find(id);

            if (actor.PosRated == 1)
            {
                actor.PosRating--;
                actor.PosRated = 0;
                _context.SaveChanges();
            }
            else
            {
                if (actor.NegRated == 1)
                {
                    actor.NegRating--;
                    actor.NegRated = 0;
                    actor.PosRating++;
                    actor.PosRated = 1;
                    _context.SaveChanges();
                }
                else
                {
                    actor.PosRating++;
                    actor.PosRated = 1;
                    _context.SaveChanges();
                }
            }
            return View("Index", await _context.Actor.ToListAsync());
        }
        // - reputation
        public async Task<IActionResult> MinusRating(int? id)         //***
        {
            var actor = _context.Actor.Find(id);

            if (actor.NegRated == 1)
            {
                actor.NegRating--;
                actor.NegRated = 0;
                _context.SaveChanges();
            }
            else
            {
                if (actor.PosRated == 1)
                {
                    actor.PosRating--;
                    actor.PosRated = 0;
                    actor.NegRating++;
                    actor.NegRated = 1;
                    _context.SaveChanges();
                }
                else
                {
                    actor.NegRating++;
                    actor.NegRated = 1;
                    _context.SaveChanges();
                }
            }

            return View("Index", await _context.Actor.ToListAsync());
        }
        // GET: Actors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actor.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .FirstOrDefaultAsync(m => m.ID == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PosRating,NegRating,Name,Surname,Description")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PosRating,NegRating,Name,Surname,Description")] Actor actor)
        {
            if (id != actor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.ID))
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
            return View(actor);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actor
                .FirstOrDefaultAsync(m => m.ID == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actor.FindAsync(id);
            _context.Actor.Remove(actor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actor.Any(e => e.ID == id);
        }
    }
}
