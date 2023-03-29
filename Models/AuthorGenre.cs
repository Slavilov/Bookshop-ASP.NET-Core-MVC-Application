namespace Bookshop_ASP.NET_Core_MVC_Application.Models
{
    public class AuthorGenre
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
