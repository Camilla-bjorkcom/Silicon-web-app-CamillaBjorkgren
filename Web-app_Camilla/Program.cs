using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Helpers.Middlewares;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<WebAppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDatabase_Users")));
builder.Services.AddDbContext<WebApiDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDatabase_Users")));
builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<WebAppDbContext>();


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

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("SuperAdmins", policy => policy.RequireRole("SuperAdmin"));
    x.AddPolicy("CIO", policy => policy.RequireRole("SuperAdmin", "CIO"));
    x.AddPolicy("Admins", policy => policy.RequireRole("SuperAdmin", "CIO", "Admin"));
    x.AddPolicy("Users", policy => policy.RequireRole("SuperAdmin", "CIO", "Admin", "User"));
});

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<UserCoursesRepository>();

var app = builder.Build();
app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "? statusCode ={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseUserSessionValidation();
app.UseAuthentication();
app.UseAuthorization();


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = ["SuperAdmin", "CIO", "Admin", "User"];
    foreach(var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
