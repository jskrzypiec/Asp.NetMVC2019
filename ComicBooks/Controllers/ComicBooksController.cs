using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComicBooks.Data;
using ComicBooks.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections;

namespace ComicBooks.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly ComicBooksContext _context;

        public ComicBooksController(ComicBooksContext context)
        {
            _context = context;
        }

        // GET: ComicBooks
        public async Task<IActionResult> Index()
        {
            var comicBooksContext = _context.ComicBook.Include(c => c.Genre);
            return View(await comicBooksContext.ToListAsync());
        }

        // GET: ComicBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBook
                .Include(c => c.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comicBook == null)
            {
                return NotFound();
            }

            return View(comicBook);
        }

        //RANDOM - generates random Comic Book for you :)
        // GET : ComicBooks/Random
        public async Task<IActionResult> Random()
        {
            var comics = from c in _context.ComicBook.
                         Include(g => g.Genre) 
                         select c;
            var ids = new List<int>();
            foreach (ComicBook comic in comics)
            {
                ids.Add(comic.Id);
            }
            //foreach(int id in ids){
            //    Console.WriteLine("idd:"+id);
            //}
            Random r = new Random();
            int randIdIndex = r.Next(ids.Count);
            int randId = ids[randIdIndex];
            //Console.WriteLine("id: " + randId);
            //linq
            comics = comics
                    .Where(c => c.Id.Equals(randId))
                    .OrderByDescending(c => c.Title)              //sortowanie zbędne,ale zrobione
                    .ThenBy(c => c.Id);
            return View(await comics.ToListAsync());
        }


        [Authorize(Roles = "Admin")]
        // GET: ComicBooks/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name");
            return View();
        }


        [Authorize(Roles = "Admin")]
        // POST: ComicBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,GenreId,Price")] ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comicBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", comicBook.GenreId);
            return View(comicBook);
        }


        [Authorize(Roles = "Admin")]
        // GET: ComicBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBook.FindAsync(id);
            if (comicBook == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", comicBook.GenreId);
            return View(comicBook);
        }

        // POST: ComicBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,GenreId,Price")] ComicBook comicBook)
        {
            if (id != comicBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comicBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComicBookExists(comicBook.Id))
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
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", comicBook.GenreId);
            return View(comicBook);
        }


        [Authorize(Roles = "Admin")]
        // GET: ComicBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBook
                .Include(c => c.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comicBook == null)
            {
                return NotFound();
            }

            return View(comicBook);
        }


        [Authorize(Roles = "Admin")]
        // POST: ComicBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comicBook = await _context.ComicBook.FindAsync(id);
            _context.ComicBook.Remove(comicBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComicBookExists(int id)
        {
            return _context.ComicBook.Any(e => e.Id == id);
        }
    }
}
