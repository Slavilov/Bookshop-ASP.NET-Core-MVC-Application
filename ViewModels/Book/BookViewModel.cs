namespace Bookshop_ASP.NET_Core_MVC_Application.ViewModels.Book
{
    public class BookViewModel
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
