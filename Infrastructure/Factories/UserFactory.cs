using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Factories;

public class UserApiFactory
{
    public static UserApiEntity Create(UserRegistrationForm form)
    {
        try
        {
            return new UserApiEntity
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                Password = PasswordHasherTwo.GenerateSecurePassword(form.Password),
            };
        }
        catch { }
        return null!;
    }
}
