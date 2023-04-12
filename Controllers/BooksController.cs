using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Bookshop_ASP.NET_Core_MVC_Application.Data;
using Bookshop_ASP.NET_Core_MVC_Application.Models;
using Newtonsoft.Json;

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

        public IActionResult Create()
        {
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //
        //public IActionResult Create([Bind("Id,Title,Price,PublicationDate,Description,ImageUrl,AuthorId,SelectedGenres")] Book book, List<int> SelectedGenres)
        //{
        //    ModelState.Remove("Author");
        //
        //    if (SelectedGenres != null && SelectedGenres.Count > 0)
        //    {
        //        foreach (var genreId in SelectedGenres)
        //        {
        //            book.BookGenres.Add(new BookGenre { GenreId = genreId });
        //        }
        //    }
        //    // Find the author from the database
        //    var author = _context.Authors.FirstOrDefault(a => a.Id == book.AuthorId);
        //
        //    // Assign the author to the book
        //    if (author != null)
        //    {
        //        book.Author = author;
        //    }
        //
        //    //ERROR CHECKER
        //    Console.WriteLine($"Book object: {JsonConvert.SerializeObject(book)}");
        //
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors);
        //        foreach (var error in errors)
        //        {
        //            Console.WriteLine(error.ErrorMessage);
        //        }
        //    }
        //
        //    // Validate the data entered by the user
        //    if (ModelState.IsValid)
        //    {
        //        // Check if the book already exists in the database
        //        var existingBook = _context.Books.FirstOrDefault(b => b.Title == book.Title);
        //
        //        if (existingBook == null)
        //        {
        //            // Book does not exist in the database, add it
        //            foreach (var genreId in SelectedGenres)
        //            {
        //                var bookGenre = new BookGenre { Book = book, GenreId = genreId };
        //                book.BookGenres.Add(bookGenre);
        //            }
        //            _context.Books.Add(book);
        //            try
        //            {
        //                _context.SaveChanges();
        //            }
        //            catch (Exception ex)
        //            {
        //                ModelState.AddModelError("", "Error saving the book: " + ex.Message);
        //                return View(book);
        //            }
        //
        //            // Redirect to the Index view of BooksController
        //            return RedirectToAction("Index", "Books");
        //        }
        //        else
        //        {
        //            // Book already exists in the database, show an error message
        //            ModelState.AddModelError("", "Book already exists in the database.");
        //        }
        //    }
        //    else
        //    {
        //        // Add this line to display model state errors
        //        ModelState.AddModelError("", "Model state is not valid.");
        //    }
        //
        //    // If the ModelState is not valid or there is an error, return to the Create view with the entered data
        //    return View(book);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Price,PublicationDate,Description,ImageUrl,AuthorId,SelectedGenres")] Book book, List<int> SelectedGenres)
        {
            ModelState.Remove("Author");

            //if (SelectedGenres != null && SelectedGenres.Count > 0)
            //{
            //    foreach (var genreId in SelectedGenres)
            //    {
            //        book.BookGenres.Add(new BookGenre { GenreId = genreId });
            //    }
            //}

            // Find the author from the database
            var author = _context.Authors.FirstOrDefault(a => a.Id == book.AuthorId);

            // Assign the author to the book
            if (author != null)
            {
                book.Author = author;
            }

            // Validate the data entered by the user
            if (ModelState.IsValid)
            {
                // Check if the book already exists in the database
                var existingBook = _context.Books.FirstOrDefault(b => b.Title == book.Title);

                if (existingBook == null)
                {
                    // Book does not exist in the database, add it
                    HashSet<int> addedGenres = new HashSet<int>();
                    foreach (var genreId in SelectedGenres)
                    {
                        if (!addedGenres.Contains(genreId))
                        {
                            var bookGenre = new BookGenre { Book = book, GenreId = genreId };
                            book.BookGenres.Add(bookGenre);
                            addedGenres.Add(genreId);
                        }
                    }
                    _context.Books.Add(book);
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error saving the book: " + ex.Message);
                        return View(book);
                    }

                    // Redirect to the Index view of BooksController
                    return RedirectToAction("Index", "Books");
                }
                else
                {
                    // Book already exists in the database, show an error message
                    ModelState.AddModelError("", "Book already exists in the database.");
                }
            }
            else
            {
                // Add this line to display model state errors
                ModelState.AddModelError("", "Model state is not valid.");
            }

            // If the ModelState is not valid or there is an error, return to the Create view with the entered data
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
