using Bookshop_ASP.NET_Core_MVC_Application.Models;
using Bookshop_ASP.NET_Core_MVC_Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bookshop_ASP.NET_Core_MVC_Application.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return View(authors);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName, LastName, Description, Nationality, ImageUrl")] Author author)
        {
            if (ModelState.IsValid)
            {
                await _authorService.CreateAuthorAsync(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        // POST: Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FirstName, LastName, Description, Nationality, ImageUrl")] Author author)
        {
            if (id != author.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _authorService.UpdateAuthorAsync(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _authorService.DeleteAuthorAsync(id);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
