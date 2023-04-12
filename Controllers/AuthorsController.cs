using Bookshop_ASP.NET_Core_MVC_Application.Data;
using Bookshop_ASP.NET_Core_MVC_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookshop_ASP.NET_Core_MVC_Application.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly BookshopDbContext _context;

        public AuthorsController(BookshopDbContext context)
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var authors = await _context.Authors.ToListAsync();
            return View(authors);
        }

        // GET: AuthorsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        ////To check if that's how to save and add data, this was given to me from chat gpt
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
            // Validate the data entered by the user
            if (ModelState.IsValid)
            {
                // Check if the author already exists in the database
                var existingAuthor = _context.Authors.FirstOrDefault(a => a.FirstName == author.FirstName && a.LastName == author.LastName);
        
                if (existingAuthor == null)
                {
                    // Author does not exist in the database, add it
                    _context.Authors.Add(author);
                    _context.SaveChanges();

                    // Redirect to the Index view of AuthorsController
                    // Da napravq novo View v koeto shte izpisva Succesfully Added new Author!
                    return View("AuthorCreated");
                }
                else
                {
                    // Author already exists in the database, show an error message
                    return View("AuthorExists");
                }
            }
        
            // If the ModelState is not valid or there is an error, return to the Create view with the entered data
            return View(author);
        }

        // GET: AuthorsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthorsController/Edit/5
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

        // GET: AuthorsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthorsController/Delete/5
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
