﻿using System.ComponentModel.DataAnnotations;

namespace Bookshop_ASP.NET_Core_MVC_Application.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Nationality { get; set; }
        public string ImageUrl { get; set; }
    }
}
