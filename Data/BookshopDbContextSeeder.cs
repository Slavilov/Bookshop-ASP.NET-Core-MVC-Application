using Bookshop_ASP.NET_Core_MVC_Application.Models;

namespace Bookshop_ASP.NET_Core_MVC_Application.Data
{
    public class BookshopDbContextSeeder
    {
        public static void Seed(BookshopDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if the database has already been seeded
            if (context.Authors.Any()
                && context.Genres.Any()
                && context.Books.Any())
            {
                return;
            }

            // Add some genres
            var genres = new Genre[]
            {
                new Genre { Name = "Fiction" },
                new Genre { Name = "Non-Fiction" },
                new Genre { Name = "Mystery" },
                new Genre { Name = "Horror" }
            };

            context.Genres.AddRange(genres);

            // Add some authors
            var authors = new Author[]
            {
                new Author { FirstName = "Stephen", LastName = "King" },
                new Author { FirstName = "J.K.", LastName = "Rowling" },
                new Author { FirstName = "Dan", LastName = "Brown" }
            };

            context.Authors.AddRange(authors);

            // Add some books
            var books = new Book[]
            {
                new Book { Title = "The Shining", PublicationDate = DateTime.Parse("1977-1-28"), Author = authors[0] },
                new Book { Title = "Harry Potter and the Philosopher's Stone", PublicationDate = DateTime.Parse("1997-6-26"), Author = authors[1] },
                new Book { Title = "The Da Vinci Code", PublicationDate = DateTime.Parse("2003-3-18"), Author = authors[2] }
            };

            context.Books.AddRange(books);

            // Add some author-genre mappings
            var authorGenres = new AuthorGenre[]
            {
                new AuthorGenre { Author = authors[0], Genre = genres[3] },
                new AuthorGenre { Author = authors[1], Genre = genres[0] },
                new AuthorGenre { Author = authors[1], Genre = genres[2] },
                new AuthorGenre { Author = authors[2], Genre = genres[1] },
                new AuthorGenre { Author = authors[2], Genre = genres[2] }
            };

            context.AuthorGenres.AddRange(authorGenres);

            // Add some book-genre mappings
            var bookGenres = new BookGenre[]
            {
                new BookGenre { Book = books[0], Genre = genres[3] },
                new BookGenre { Book = books[1], Genre = genres[0] },
                new BookGenre { Book = books[1], Genre = genres[2] },
                new BookGenre { Book = books[2], Genre = genres[1] },
                new BookGenre { Book = books[2], Genre = genres[2] }
            };

            context.BookGenres.AddRange(bookGenres);

            // Save changes to the database
            context.SaveChanges();
        }
    }
}
