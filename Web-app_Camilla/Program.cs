using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Helpers.Middlewares;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDatabase")));
builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<DataContext>();

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AddressRepository>();
builder.Services.ConfigureApplicationCookie(x =>
{
    //webbläsaren kan inte komma åt, minimerar risk för crossside-scripting
    x.Cookie.HttpOnly = true;
    x.LoginPath = "/signin";
    x.LogoutPath = "/signout";
});

var app = builder.Build();
app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "? statusCode ={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseUserSessionValidation();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
//https://youtu.be/OGjePJrqUa4?t=1400
//Use autentication https://youtu.be/bUdCONPJuFc?t=710