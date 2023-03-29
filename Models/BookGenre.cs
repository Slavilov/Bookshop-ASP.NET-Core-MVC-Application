namespace Bookshop_ASP.NET_Core_MVC_Application.Models
{
    public class BookGenre
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
