﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Bookshop_ASP.NET_Core_MVC_Application.Models
{
    public class Book
    {
        public Book()
        {
            this.BookGenres = new List<BookGenre>();
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
    }
}
