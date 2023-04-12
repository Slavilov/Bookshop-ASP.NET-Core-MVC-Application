using Bookshop_ASP.NET_Core_MVC_Application.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Bookshop.Data;

var builder = WebApplication.CreateBuilder(args);

//var confBuilder = new ConfigurationBuilder();
//confBuilder.AddJsonFile("appsettings.json");
//var configuration = confBuilder.Build();
//
//IConfiguration confString = confBuilder.Build();


// Add services to the container.
//builder.Services.AddDbContext<BookshopDbContext>(options => options.UseSqlServer());

//builder.Services.AddDbContext<BookshopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<BookshopDbContext>();
builder.Services.AddControllersWithViews();
//DefaultConnectionString


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
BookshopDbContextSeeder.Seed(app);

app.Run();
