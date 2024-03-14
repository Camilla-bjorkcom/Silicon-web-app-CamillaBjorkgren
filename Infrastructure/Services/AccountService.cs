using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace Infrastructure.Services;

public class AccountService(UserRepository userRepository, UserManager<UserEntity> userManager, AddressRepository addressRepository)
{
    private readonly UserRepository _userRepository = userRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AddressRepository _addressRepository = addressRepository;

    public async Task<UserEntity> UpdateUserAsync(UserEntity user)
    {
        var existingUser = await _userManager.FindByEmailAsync(user.Email!);
        if (existingUser != null)
        {
            var result = await _userRepository.UpdateAsync(user);
            return result;
        }
        return null!;
    }

    public async Task<AddressEntity> GetAddressAsync(string userId)
    {
        var addressEntity = await _addressRepository.GetOneAsync(x => x.UserId == userId);
        return addressEntity!;
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

        catch (Exception ex)
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
        catch (Exception ex)
        {
            return false;
        }
    }
}
