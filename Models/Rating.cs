namespace Bookshop_ASP.NET_Core_MVC_Application.Models
{
        public class Rating
        {
            public int Id { get; set; }
            public int BookId { get; set; }
            public Book Book { get; set; }
            public int Value { get; set; } // This would be your rating value, e.g., 1-5
        }
}
