//using Bookshop_ASP.NET_Core_MVC_Application.Data;
//using Microsoft.EntityFrameworkCore;
//using System.Configuration;
//using Microsoft.Extensions.Configuration;
//using Bookshop.Data;
//using Bookshop_ASP.NET_Core_MVC_Application.Services;
//
//var builder = WebApplication.CreateBuilder(args);
//
////var confBuilder = new ConfigurationBuilder();
////confBuilder.AddJsonFile("appsettings.json");
////var configuration = confBuilder.Build();
////
////IConfiguration confString = confBuilder.Build();
//
//
//// Add services to the container.
////builder.Services.AddDbContext<BookshopDbContext>(options => options.UseSqlServer());
//builder.Services.AddDbContext<BookshopDbContext>();
//builder.Services.AddScoped<IAuthorService, AuthorService>();
//builder.Services.AddScoped<IBookService, BookService>();
//builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30); 
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});
//builder.Services.AddControllersWithViews();
//
//builder.Services.AddDbContext<BookshopDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
//           .EnableSensitiveDataLogging() // This logs SQL queries
//           .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information));
//
//
////DefaultConnectionString
//
//
//var app = builder.Build();
//
//
//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
//
//app.UseSession();
//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseAuthorization();
//
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
//BookshopDbContextSeeder.Seed(app);
//
//app.Run();

using Bookshop_ASP.NET_Core_MVC_Application.Data;
using Microsoft.EntityFrameworkCore;
using Bookshop_ASP.NET_Core_MVC_Application.Services;
using Bookshop.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BookshopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information));

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust this as necessary
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

BookshopDbContextSeeder.Seed(app);

app.Run();

