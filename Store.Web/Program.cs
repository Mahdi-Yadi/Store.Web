using Application.Services.Account;
using Application.Services.Categories;
using Application.Services.Orders;
using Application.Services.Products;
using DataLayer.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Parbad.Builder;
using Parbad.Gateway.ZarinPal;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json")
	.Build();

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IOrderService, OrderService>();

// parbad
builder.Services.AddParbad()
    .ConfigureGateways(geteway =>
    {
        geteway.AddZarinPal()
            .WithAccounts(accounts =>
            {
                accounts.AddInMemory(account =>
                {
                    account.MerchantId = "d0879652-f0af-4da0-b803-000629ef2225";
                    account.IsSandbox = true;
                });
            });
    })
    .ConfigureHttpContext(build => build.UseDefaultAspNetCore())
    .ConfigureStorage(build => build.UseMemoryCache());

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.Cookie.HttpOnly = true;
		options.ExpireTimeSpan = TimeSpan.FromDays(30);
		options.SlidingExpiration = true;
		options.LoginPath = "/Login";
		options.LogoutPath = "/Logout";
		options.AccessDeniedPath = "/AccessDenied";
	});

builder.Services.AddDbContext<DBContext>(options =>
{
	options.UseSqlServer(configuration.GetConnectionString("DBConnectionStrings"));
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.Run();