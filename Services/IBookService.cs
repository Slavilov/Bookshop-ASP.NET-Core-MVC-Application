using System.Collections.Generic;
using System.Threading.Tasks;
using Bookshop_ASP.NET_Core_MVC_Application.Models;

namespace Bookshop_ASP.NET_Core_MVC_Application.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<List<Author>> GetAuthorsAsync();
        Task<List<Genre>> GetGenresAsync();
    }
}
