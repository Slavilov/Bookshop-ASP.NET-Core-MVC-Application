﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Bookshop_ASP.NET_Core_MVC_Application.Data;
using Bookshop_ASP.NET_Core_MVC_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookshop_ASP.NET_Core_MVC_Application.Services
{
    public class BookService : IBookService
    {
        private readonly BookshopDbContext _context;

        public BookService(BookshopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {

            try
            {
                var book = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.BookGenres)
                        .ThenInclude(bg => bg.Genre)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (book == null)
                {
                    Console.WriteLine($"No book found with ID: {id}");
                }

                return book;
            }
            catch (Exception ex)
            {
                // Log or throw the error to see if anything goes wrong during the query
                Console.WriteLine($"Error fetching book: {ex.Message}");
                return null;
            }

            //Console.WriteLine($"Attempting to fetch book with ID: {id}");
            //
            //var book = await _context.Books
            //    .Include(b => b.Author)
            //    .Include(b => b.BookGenres)
            //        .ThenInclude(bg => bg.Genre)
            //    .FirstOrDefaultAsync(b => b.Id == id);
            //
            //if (book == null)
            //{
            //    // Log or throw an error here
            //    Console.WriteLine($"No book found with ID: {id}");
            //}
            //
            //return book;

            //return await _context.Books
            //    .Include(b => b.Author)
            //    .Include(b => b.BookGenres)
            //        .ThenInclude(bg => bg.Genre)
            //    .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task CreateBookAsync(Book book)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Author>> GetAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<List<Genre>> GetGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }
    }
}