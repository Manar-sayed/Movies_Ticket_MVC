using Microsoft.EntityFrameworkCore;
using MovieTicketApp_MVC_project.Data;
using static System.Net.Mime.MediaTypeNames;
using System;
using MovieTicketApp_MVC_project.Data.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using MovieTicketApp_MVC_project.Data.Cart;
using MovieTicketApp_MVC_project.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//----------------------
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//db context configrations------------------------
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("con1")));


//services configrations------------------------
builder.Services.AddScoped<IActorsService, ActorService>();
builder.Services.AddScoped<IProducersService, ProducersService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<ICinemaService, CinemaService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc=>ShoppingCart.GetShoppingCart(sc));


//Authentication and authorization--------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(option =>
    { option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;});



//Cookies configrations------------------------
//builder.Services.AddAuthentication(
//    CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(option =>
//    {
//        option.LoginPath = "/Access/Login";
//        option.ExpireTimeSpan=TimeSpan.FromMinutes(20);
//        option.Cookie.SameSite=SameSiteMode.None;//***********************
//        option.Cookie.HttpOnly= true;

//    });



//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer());
//.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

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
app.UseSession();
//Authorization & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");
//seed
AppDbInitializer.Seed(app);
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
app.Run();
