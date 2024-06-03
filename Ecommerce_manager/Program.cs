// vô program để đăng ký
using AutoMapper;
using Ecommerce_manager.Data;
using Ecommerce_manager.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EcommerceShopContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Ecommerce_shop"));
});



// https://docs.automapper.org/en/stable/Dependency-injection.html
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-8.0
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-8.0
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
	options.LoginPath = "/KhachHang/DangNhap";
	options.AccessDeniedPath = "/AccessDenied";
});


var app = builder.Build();//đăng ký services trước build

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
app.UseAuthorization();
app.UseAuthentication();//Confirm user
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


//Từ net core 6. trở lên có program.cs