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
builder.Services.ConfigureApplicationCookie(x =>
{
    //webbläsaren kan inte komma åt cookien, minimerar risk för crossside-scripting
    x.Cookie.HttpOnly = true;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;

    x.LoginPath = "/signin";
    x.LogoutPath = "/signout";
    x.AccessDeniedPath = "/denied";
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    x.SlidingExpiration = true;
});
builder.Services.AddAuthentication().AddFacebook(x =>
{
    x.AppId = "1890598498064693";
    x.AppSecret = "6dd553045023489c06894a3855f1c50a";
    x.Fields.Add("first_name");
    x.Fields.Add("last_name");
});
builder.Services.AddAuthentication().AddGoogle(x =>
{
    x.ClientId = "396448238264-ki05vobsns45a7rif0kl4fe2aqpqqp9c.apps.googleusercontent.com";
    x.ClientSecret = "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";
});

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AddressRepository>();


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


//Gör en denied-sida? 