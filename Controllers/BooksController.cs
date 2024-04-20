using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Bookshop_ASP.NET_Core_MVC_Application.Data;
using Bookshop_ASP.NET_Core_MVC_Application.Models;
using System.Net;

namespace Bookshop_ASP.NET_Core_MVC_Application.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookshopDbContext _context;

        public BooksController(BookshopDbContext context)
        {
            _context = context;
        }

        // GET: BooksController
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .ToListAsync();

            return View(books);
        }

        // GET: BooksController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, Price, PublicationDate, Description, AuthorId, GenreId, ImageUrl")] Book book)
        {
            if (!ModelState.IsValid)
            {
                // Fetch the corresponding Author object based on the AuthorId
                var author = await _context.Authors.FindAsync(book.AuthorId);
                if (author != null)
                {
                    book.Author = author;

                    // Add book to context and save changes
                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("AuthorId", "Invalid author selected.");
                }
            }
            // Handle ModelState invalid scenario
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            return View(book);
        }



        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var book = await _context.Books.FindAsync(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }
        //
        //    _context.Books.Remove(book);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
