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

    public async Task<AddressEntity> GetAddressAsync(int id)
    {
        var addressEntity = await _addressRepository.GetOneAsync(x => x.Id == id);
        return addressEntity!;

        //https://youtu.be/PKfSRf9VZrw?t=5365
    }

    

    public async Task<bool> CreateAddressAsync(AddressEntity address)
    {
        try
        {
            var result = await _addressRepository.CreateAsync(address);
            if (result != null)
                return true;
            return false;
        }
        
        catch (Exception ex)
        {
            return false;
        }
    }
    public async Task<bool> UpdateAddressAsync(AddressEntity address)
    {
        try
        {
            var exisiting = await _addressRepository.GetOneAsync(x => x.Id == address.Id);
            if (exisiting != null)
            {
                var result = await _addressRepository.UpdateAsync(address);
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
