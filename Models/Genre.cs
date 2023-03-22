using System.ComponentModel.DataAnnotations;

namespace Bookshop_ASP.NET_Core_MVC_Application.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
