using Bookshop_ASP.NET_Core_MVC_Application.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Unity;

namespace Bookshop_ASP.NET_Core_MVC_Application.Data
{
    public class BookshopDbContext : DbContext
    {

        private readonly IConfiguration _configuration;

        [InjectionConstructor]
        public BookshopDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public BookshopDbContext(DbContextOptions<BookshopDbContext> options) : base(options)
        //{
        //
        //}

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<AuthorGenre> AuthorGenres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        // Add a DbSet Comment and Model Comment
        // Add a DbSet Rating and Model Rating (min rating 1 - max rating 5)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookGenre>()
                .HasKey(bg => new { bg.BookId, bg.GenreId });

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.BookId);

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Genre)
                .WithMany(g => g.BookGenres)
                .HasForeignKey(bg => bg.GenreId);



            modelBuilder.Entity<AuthorGenre>()
                .HasKey(ag => new { ag.AuthorId, ag.GenreId });

            modelBuilder.Entity<AuthorGenre>()
                .HasOne(ag => ag.Author)
                .WithMany(a => a.AuthorGenres)
                .HasForeignKey(ag => ag.AuthorId);

            modelBuilder.Entity<AuthorGenre>()
                .HasOne(ag => ag.Genre)
                .WithMany(g => g.AuthorGenres)
                .HasForeignKey(bg => bg.GenreId);

            //I don't know if i wrote the Fluent API right or wrong!

            modelBuilder.Entity<Book>()
           .Property(b => b.Price)
           .HasColumnType("decimal(18,2)");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}