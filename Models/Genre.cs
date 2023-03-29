using System.ComponentModel.DataAnnotations;

namespace Bookshop_ASP.NET_Core_MVC_Application.Models
{
    public class Genre
    {
        public Genre()
        {
            this.BookGenres = new List<BookGenre>();
            this.AuthorGenres = new List<AuthorGenre>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<BookGenre> BookGenres { get; set; }
        public ICollection<AuthorGenre> AuthorGenres { get; set; }
    }
}
