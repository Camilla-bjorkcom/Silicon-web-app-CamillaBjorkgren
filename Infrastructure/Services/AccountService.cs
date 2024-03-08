using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace Infrastructure.Services;

public class AccountService(UserRepository userRepository, UserManager<UserEntity> userManager)
{
    private readonly UserRepository _userRepository = userRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;

    public async Task<ResponseResult> UpdateUserAsync(UserEntity user)
    {
        var existingUser = await _userManager.FindByEmailAsync(user.Email!);
        if (existingUser != null)
        {
            var result = await _userRepository.UpdateAsync(user);
            if (result.StatusCode == StatusCode.OK && result.ContentResult != null)
            {
                var userEntity = (UserEntity)result.ContentResult;
                return ResponseFactory.Ok(userEntity);
            }
        }
        return ResponseFactory.Error("Failed to update user");
    }
}
