using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Claims;

namespace Infrastructure.Services;

public class AccountService(UserRepository userRepository, UserManager<UserEntity> userManager, AddressRepository addressRepository, IConfiguration configuration, UserCoursesRepository userCoursesRepository)
{
    private readonly UserRepository _userRepository = userRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AddressRepository _addressRepository = addressRepository;
    private readonly UserCoursesRepository _userCoursesRepository = userCoursesRepository;
    private readonly IConfiguration _configuration = configuration;

    public async Task<UserEntity> UpdateUserAsync(UserEntity user)
    {
        try
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email!);
            if (existingUser != null)
            {
                var result = await _userRepository.UpdateAsync(user);
                return result;
            }
            return null!;
        }
        catch { return null!; }
    }

    public async Task<AddressEntity> GetAddressAsync(string userId)
    {
        try
        {
            var addressEntity = await _addressRepository.GetOneAsync(x => x.UserId == userId);
            if (addressEntity != null)
            {
                return addressEntity!;
            }
            return null!;
        }
     
        catch { return null!; }
    }



    public async Task<bool> CreateAddressAsync(AddressEntity address, UserEntity user)
    {
        try
        {
            var result = await _addressRepository.CreateAsync(address);
            if (result != null)
            {
                user.AddressId = address.Id;
                user.Modified = DateTime.Now;
                var updating = await _userRepository.UpdateAsync(user);
                if (updating != null)
                    return true;
                return false;
            }

            return false;
        }

        catch
        {
            return false;
        }
    }

    public async Task<bool> JoinCourseAsync(string userId, string courseId)
    {
        try
        {
            if (userId != null && courseId != null)
            {
                UserCoursesEntity entity = new UserCoursesEntity
                {
                    UserId = userId,
                    CourseId = courseId
                };
                var result = await _userCoursesRepository.CreateAsync(entity);
                if (result != null)
                {
                    return true;

                }
            }
            return false;
        }

        catch 
        {
            return false;
        }
    }

    public async Task<bool> RemoveCourseAsync(string userId, string courseId)
    {
        try
        {
            if (userId != null && courseId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {            
                    var result = await _userCoursesRepository.DeleteAsync(x => x.UserId == userId && x.CourseId == courseId);
                    if (result)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveAllCourseAsync(string userId)
    {
        try
        {
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {                  
                    var result = await _userCoursesRepository.DeleteAllAsync(x => x.UserId == userId);
                    if (result)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAddressAsync(AddressEntity address, UserEntity user)
    {
        try
        {
            var exisiting = await _addressRepository.GetOneAsync(x => x.UserId == address.UserId);
            if (exisiting != null)
            {
                var result = await _addressRepository.UpdateAsync(address);
                user.Modified = DateTime.Now;
                var updating = await _userRepository.UpdateAsync(user);
                if (updating != null)
                    return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    public async Task<bool> UpdatePasswordAsync(UserEntity user, string newPassword)
    {
        try
        {
            var exisitingUser = await _userManager.FindByEmailAsync(user.Email!);
            if (exisitingUser != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
                var result = await _userRepository.UpdateAsync(user);
                if (result != null)
                {
                    user.Modified = DateTime.Now;
                    return true;
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(UserEntity user, bool CheckDelete)
    {
        try
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email!);
            if (existingUser != null)
            {
                if (CheckDelete)
                {
                    var result = await _userManager.DeleteAsync(existingUser);
                    if (result == IdentityResult.Success)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UploadUserProfileImageAsync(ClaimsPrincipal user, IFormFile file)
    {
        try
        {
            if (user != null && file != null && file.Length != 0)
            {
                var userEntity = await _userManager.GetUserAsync(user);
                if (userEntity != null)
                {
                    var fileName = $"p_{userEntity.Id}_{Guid.NewGuid()}_{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["FileUploadPath"]!, fileName);

                    using var fs = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fs);

                    userEntity.ProfileImg = fileName;
                    var result = await _userRepository.UpdateAsync(userEntity);
                    if (result != null)
                    {
                        return true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }


}
