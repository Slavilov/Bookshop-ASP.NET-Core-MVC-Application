using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshop_ASP.NET_Core_MVC_Application.Models
{
    public class Book
    {
        public Book()
        {
            this.BookGenres = new List<BookGenre>();
            //this.Ratings = new List<Rating>();
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<BookGenre> BookGenres { get; set; }

        public int AuthorId { get; set; }

        [ValidateNever]
        public Author Author { get; set; }

        //public ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>(); // Initialize the collection
        public double AverageRating => Ratings.Any() ? Ratings.Average(r => r.Value) : 0.0;
    }
}
