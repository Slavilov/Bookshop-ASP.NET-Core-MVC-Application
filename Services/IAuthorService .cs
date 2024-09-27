using System.Collections.Generic;
using System.Threading.Tasks;
using Bookshop_ASP.NET_Core_MVC_Application.Models;

namespace Bookshop_ASP.NET_Core_MVC_Application.Services
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task CreateAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
    }
}
