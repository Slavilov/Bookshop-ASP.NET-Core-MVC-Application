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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BooksController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: BooksController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Title, Price, PublicationDate, Description, AuthorId, GenreId, ImageUrl")] Book book)
        //{
        //
        //    if (ModelState.IsValid)
        //    {
        //        // Fetch the corresponding Author object based on the AuthorId
        //        book.Author = await _context.Authors.FindAsync(book.AuthorId);
        //
        //
        //        _context.Add(book);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    //I was getting an ModelState.Isvalid = false, that's why i added this part!
        //    if (!ModelState.IsValid)
        //    {
        //        book.Author = await _context.Authors.FindAsync(book.AuthorId);
        //        Console.WriteLine(book.Author.FirstName);
        //
        //        foreach (var modelStateEntry in ModelState.Values)
        //        {
        //            foreach (var error in modelStateEntry.Errors)
        //            {
        //                Console.WriteLine(error.ErrorMessage);
        //            }
        //        }
        //    }
        //    ViewBag.Authors = _context.Authors.ToList();
        //    ViewBag.Genres = _context.Genres.ToList();
        //    return View(book);
        //}

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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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


    }
}
