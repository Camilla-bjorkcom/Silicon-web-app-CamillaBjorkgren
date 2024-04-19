using Infrastructure.Contexts;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_API_Camilla.Filters;

namespace Web_API_Camilla.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(WebApiDbContext webApiDbContext, IConfiguration configuration) : ControllerBase
{
    private readonly WebApiDbContext _webApiDbContext = webApiDbContext;
    private readonly IConfiguration _configuration = configuration;


    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(UserRegistrationForm form)
    {
        if (ModelState.IsValid)
        {
            if (!await _webApiDbContext.UserApi.AnyAsync(x => x.Email == form.Email))
            {
                var userApiEntity = UserApiFactory.Create(form);
                _webApiDbContext.UserApi.Add(userApiEntity);
                await _webApiDbContext.SaveChangesAsync();
                return Created("", null);
            }
            return Conflict();
        }

        return BadRequest();
    }

    [UseApiKey]
    [HttpPost]
    [Route("token")]
    public IActionResult GetToken(UserLoginForm form)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
                var tokenDecriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new(ClaimTypes.Email, form.Email),
                        new(ClaimTypes.Name, form.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDecriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(tokenString);
            }
            return Unauthorized();
        
        }
        catch { return BadRequest(); }
    }

       
}

