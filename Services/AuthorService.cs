using System.Collections.Generic;
using System.Threading.Tasks;
using Bookshop_ASP.NET_Core_MVC_Application.Data;
using Bookshop_ASP.NET_Core_MVC_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookshop_ASP.NET_Core_MVC_Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly BookshopDbContext _context;

        public AuthorService(BookshopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task CreateAuthorAsync(Author author)
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            var existingAuthor = await _context.Authors.FindAsync(author.Id);
            if (existingAuthor != null)
            {
                _context.Update(author);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
