using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmsActors.Data;
using FilmsActors.Models;
using Microsoft.AspNetCore.Authorization;

namespace FilmsActors.Controllers
{
    public class FilmsController : Controller
    {
        public static float ratingMath(float PosRating, float NegRating)
        {
            float Rating;
            if (PosRating == 0)
            {
                Rating = (PosRating + 1) / (PosRating + 1 + NegRating);
            }
            else
            {
                Rating = (float)PosRating / (float)(PosRating + NegRating);
            }
            return Rating;
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        private readonly ApplicationDbContext _context;
        public FilmsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //-----------------------------------------------------------------
        
        public async Task<IActionResult> ActorsIncluded(int ID)   //***
        {
            var actorsId = _context.FilmsActorsMod.Where(j => j.FilmID == ID).Select(j => j.ActorID).ToList();
            var actors = await _context.Actor.Where(j => actorsId.Contains(j.ID)).ToListAsync();
            return View("View", actors);
        }
        public async Task<IActionResult> AlphabeticalView()   //***
        {
            return View("Index", await _context.Film.OrderBy(j => j.Title).ToListAsync());
        }
        public async Task<IActionResult> ReleaseDateView()   //***
        {
            return View("Index", await _context.Film.OrderBy(j => j.ReleaseDate).ToListAsync());
        }
        public async Task<IActionResult> PopularityView()   //***
        {
            return View("Index", await _context.Film.OrderBy(j => j.PosRating).ToListAsync());
        }
        public async Task<IActionResult> ShowSearchResults(string SearchTitle)   //***
        {
            return View("Index", await _context.Film.Where(j => j.Title.Contains(SearchTitle)).ToListAsync());
        }
        // + reputation
        [Authorize]
        public async Task<IActionResult> PlusRating(int? id)   //***
        {
            var film = _context.Film.Find(id);

            if (film.PosRated == 1)
            {
                film.PosRating--;
                film.PosRated = 0;
                _context.SaveChanges();
            }
            else
            {
                if (film.NegRated == 1)
                {
                    film.NegRating--;
                    film.NegRated = 0;
                    film.PosRating++;
                    film.PosRated = 1;
                    _context.SaveChanges();
                }
                else
                {
                    film.PosRating++;
                    film.PosRated = 1;
                    _context.SaveChanges();
                }
            }
            return View("Index", await _context.Film.ToListAsync());
        }
        // - reputation
        [Authorize]
        public async Task<IActionResult> MinusRating(int? id)         //***
        {
            var film = _context.Film.Find(id);

            if (film.NegRated == 1)
            {
                film.NegRating--;
                film.NegRated = 0;
                _context.SaveChanges();
            }
            else
            {
                if (film.PosRated == 1)
                {
                    film.PosRating--;
                    film.PosRated = 0;
                    film.NegRating++;
                    film.NegRated = 1;
                    _context.SaveChanges();
                }
                else
                {
                    film.NegRating++;
                    film.NegRated = 1;
                    _context.SaveChanges();
                }
            }

            return View("Index", await _context.Film.ToListAsync());
        }
        // GET: Films
        public async Task<IActionResult> Index()
        {
            return View(await _context.Film.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .FirstOrDefaultAsync(m => m.ID == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        
        public IActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PosRating,NegRating,Title,Info,ReleaseDate")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Films/Edit/5
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
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PosRating,NegRating,Title,Info,ReleaseDate")] Film film)
        {
            if (id != film.ID)
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
                    if (!FilmExists(film.ID))
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
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .FirstOrDefaultAsync(m => m.ID == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
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
            return _context.Film.Any(e => e.ID == id);
        }
    }
}
