using Bookshop_ASP.NET_Core_MVC_Application.Models;
using Bookshop_ASP.NET_Core_MVC_Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bookshop_ASP.NET_Core_MVC_Application.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: BooksController
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }

        // GET: BooksController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _bookService.GetAuthorsAsync();
            ViewBag.Genres = await _bookService.GetGenresAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, int[] GenreIds)
        {
            Console.WriteLine("AuthorId: " + book.AuthorId);
            Console.WriteLine("BookGenres: " + book.BookGenres);

            // Manually find the author based on AuthorId and assign it
            var author = (await _bookService.GetAuthorsAsync())
                                 .FirstOrDefault(a => a.Id == book.AuthorId);
            book.Author = author;

            // Map selected genres to BookGenres collection
            var selectedGenres = await _bookService.GetGenresAsync();
            foreach (var genreId in GenreIds)
            {
                var genre = selectedGenres.FirstOrDefault(g => g.Id == genreId);
                if (genre != null)
                {
                    book.BookGenres.Add(new BookGenre { Book = book, Genre = genre });
                }
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                if (author != null)
                {
                    await _bookService.CreateBookAsync(book);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("AuthorId", "Invalid author selected.");
                }
            }

            ViewBag.Authors = await _bookService.GetAuthorsAsync();
            ViewBag.Genres = await _bookService.GetGenresAsync();
            return View(book);
        }




        // GET: BooksController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            ViewBag.Authors = await _bookService.GetAuthorsAsync();
            ViewBag.Genres = await _bookService.GetGenresAsync();
            return View(book);
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Title, Price, PublicationDate, Description, AuthorId, GenreId, ImageUrl")] Book book)
        {
            if (id != book.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _bookService.UpdateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Authors = await _bookService.GetAuthorsAsync();
            ViewBag.Genres = await _bookService.GetGenresAsync();
            return View(book);
        }

        // GET: BooksController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookByIdAsync(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
