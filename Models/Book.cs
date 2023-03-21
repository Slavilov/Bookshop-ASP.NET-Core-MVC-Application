using System.ComponentModel.DataAnnotations;

namespace Bookshop_ASP.NET_Core_MVC_Application.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string ImageUrl { get; set; }
    }
}
