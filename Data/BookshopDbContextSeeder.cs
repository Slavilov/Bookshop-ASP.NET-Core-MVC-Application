using System;
using System.Collections.Generic;
using System.Linq;
using Bookshop_ASP.NET_Core_MVC_Application.Data;
using Bookshop_ASP.NET_Core_MVC_Application.Models;

namespace Bookshop.Data
{
    public static class BookshopDbContextSeeder
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BookshopDbContext>();

                // Ensure the database is created
                context.Database.EnsureCreated();

                // Check if the database has already been seeded
                if (context.Authors.Any()
                    && context.Genres.Any()
                    && context.Books.Any())
                {
                    return; // Database has been seeded
                }

                // Create some genres
                var genres = new List<Genre>
            {
                new Genre { Name = "Fiction", Description = "Works of imagination or creative expression" },
                new Genre { Name = "Non-Fiction", Description = "Works of informative or factual writing" },
                new Genre { Name = "Horror", Description = "Works intended to scare or frighten" },
                new Genre { Name = "Mystery", Description = "Works intended to intrigue or engage the reader through suspense and plot twists" },
                new Genre { Name = "Romance", Description = "Works focused on love and relationships" }
            };
                context.Genres.AddRange(genres);

                // Create some authors
                var authors = new List<Author>
            {
                new Author { FirstName = "Stephen", LastName = "King", Nationality = "American", Description = "Best-selling author known for his horror and suspense novels", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/e/e3/Stephen_King%2C_Comicon.jpg" },
                new Author { FirstName = "J.K.", LastName = "Rowling", Nationality = "British", Description = "Best-selling author of the Harry Potter series", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/5/5d/J._K._Rowling_2010.jpg" },
                new Author { FirstName = "Dan", LastName = "Brown", Nationality = "American", Description = "Best-selling author of The Da Vinci Code and other thriller novels", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8b/Dan_Brown_bookjacket_cropped.jpg" },
                new Author { FirstName = "Margaret", LastName = "Atwood", Nationality = "Canadian", Description = "Best-selling author of The Handmaid's Tale and other novels", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/f/f4/Margaret_Atwood_in_2015-2.jpg" },
                new Author { FirstName = "Jane", LastName = "Austen", Nationality = "British", Description = "Classic author of novels such as Pride and Prejudice and Sense and Sensibility", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/c/cb/Jane_Austen%2C_from_A_Memoir_of_Jane_Austen_%281870%29.jpg" }
            };
                context.Authors.AddRange(authors);

                // Create some books
                var books = new List<Book>
            {
                new Book { Title = "The Shining", PublicationDate = new DateTime(1977, 1, 28), Description = "A novel about a haunted hotel in Colorado", Price = 15.99m, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/4/4c/Shiningnovel.jpg", Author = authors[0] },
                new Book { Title = "Harry Potter and the Philosopher's Stone", PublicationDate = new DateTime(1997, 6, 26), Description = "The first book in the Harry Potter series", Price = 9.99m, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/7/7a/Harry_Potter_and_the_Philosopher%27s_Stone_banner.jpg", Author = authors[1] },
                new Book { Title = "The Da Vinci Code", PublicationDate = new DateTime(2003, 3, 18), Description = "A thriller novel about a symbologist who is drawn into a conspiracy involving the Catholic Church", Price = 12.99m, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/6/6b/DaVinciCode.jpg", Author = authors[2] },
                new Book { Title = "The Handmaid's Tale", PublicationDate = new DateTime(1985, 8, 12), Description = "A dystopian novel set in a future United States where women are oppressed and used for reproductive purposes", Price = 14.99m, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/1/18/TheHandmaidsTale%281stEd%29.jpg", Author = authors[3] },
                new Book { Title = "Pride and Prejudice", PublicationDate = new DateTime(1813, 1, 28), Description = "A classic novel about the relationship between Elizabeth Bennet and Mr. Darcy", Price = 7.99m, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/0/09/Brock_Pride_and_Prejudice.jpg", Author = authors[4] }
        };
                context.Books.AddRange(books);

                // Create some book-genre mappings
                var bookGenres = new List<BookGenre>();
                {
                    bookGenres.Add(new BookGenre { Book = books[0], Genre = genres[2] }); // The Shining -> Horror
                    bookGenres.Add(new BookGenre { Book = books[0], Genre = genres[3] }); // The Shining -> Mystery
                    bookGenres.Add(new BookGenre { Book = books[1], Genre = genres[0] }); // Harry Potter and the Philosopher's Stone -> Fiction
                    bookGenres.Add(new BookGenre { Book = books[1], Genre = genres[1] }); // Harry Potter and the Philosopher's Stone -> Non-Fiction
                    bookGenres.Add(new BookGenre { Book = books[1], Genre = genres[4] }); // Harry Potter and the Philosopher's Stone -> Romance
                    bookGenres.Add(new BookGenre { Book = books[2], Genre = genres[0] }); // The Da Vinci Code -> Fiction
                    bookGenres.Add(new BookGenre { Book = books[2], Genre = genres[1] }); // The Da Vinci Code -> Non-Fiction
                    bookGenres.Add(new BookGenre { Book = books[2], Genre = genres[4] }); // The Da Vinci Code -> Romance
                    bookGenres.Add(new BookGenre { Book = books[3], Genre = genres[0] }); // The Handmaid's Tale -> Fiction
                    bookGenres.Add(new BookGenre { Book = books[3], Genre = genres[2] }); // The Handmaid's Tale -> Horror
                    bookGenres.Add(new BookGenre { Book = books[4], Genre = genres[0] }); // Pride and Prejudice -> Fiction
                    bookGenres.Add(new BookGenre { Book = books[4], Genre = genres[4] }); // Pride and Prejudice -> Romance
                }
                context.BookGenres.AddRange(bookGenres);

                var authorGenres = new List<AuthorGenre>();
                {
                    authorGenres.Add(new AuthorGenre { Author = authors[0], Genre = genres[2] }); // Stephen King -> Horror
                    authorGenres.Add(new AuthorGenre { Author = authors[0], Genre = genres[3] }); // Stephen King -> Mystery
                    authorGenres.Add(new AuthorGenre { Author = authors[1], Genre = genres[0] }); // J.K. Rowling -> Fiction
                    authorGenres.Add(new AuthorGenre { Author = authors[1], Genre = genres[4] }); // J.K. Rowling -> Romance
                    authorGenres.Add(new AuthorGenre { Author = authors[2], Genre = genres[0] }); // Dan Brown -> Fiction
                    authorGenres.Add(new AuthorGenre { Author = authors[3], Genre = genres[0] }); // Margaret Atwood -> Fiction
                    authorGenres.Add(new AuthorGenre { Author = authors[4], Genre = genres[0] }); // Jane Austen -> Fiction
                    authorGenres.Add(new AuthorGenre { Author = authors[4], Genre = genres[4] }); // Jane Austen -> Romance
                }
                context.AuthorGenres.AddRange(authorGenres);

                // Save the changes to the database
                context.SaveChanges();
            }
        }
    }
}

