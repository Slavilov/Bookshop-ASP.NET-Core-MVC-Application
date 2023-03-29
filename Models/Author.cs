﻿using System.ComponentModel.DataAnnotations;

namespace Bookshop_ASP.NET_Core_MVC_Application.Models
{
    public class Author
    {
        public Author()
        {
            this.Books = new List<Book>();
            this.AuthorGenres = new List<AuthorGenre>();
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Nationality { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<AuthorGenre> AuthorGenres { get; set; }
    }
}
