using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Web_API_Camilla.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WebApiDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDatabase_Courses")));
builder.Services.AddCors(x =>
{
    x.AddPolicy("CustomOriginPolicy", options =>
    {
        options
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader(); 
    });
});
builder.Services.RegisterJwt(builder.Configuration);

var app = builder.Build();
app.UseCors("CustomOriginPolicy");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
