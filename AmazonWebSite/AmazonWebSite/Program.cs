using AmazonWebSiteBLL.Interfaces;
using AmazonWebSiteBLL.Repositories;
using AmazonWebSiteDAL.Contexts;
using AmazonWebSiteDAL.Entities;
using AmazonWebSitePL.Mapper;
using Department_project_DAL.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Dynamic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AmazonContext>(options=>options
.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddScoped<IItemsRepository,ItemsRepository>();
builder.Services.AddScoped<IImagesRepository, ImagesRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();
builder.Services.AddAutoMapper(map =>map.AddProfile( new MappingProfiles()));//instead of writing the create map here u use class
builder.Services.AddIdentity<UserApplication, IdentityRole>(options=>
{
	options.Password.RequiredLength = 6;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;

})
    .AddEntityFrameworkStores<AmazonContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(Options=>
    {
        Options.LoginPath = "Account/LogIn";
        Options.AccessDeniedPath = "Home/Error";
    }
     );

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
