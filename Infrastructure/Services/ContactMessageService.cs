using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;
public class ContactMessageService(ContactMessageRepository contactMessageRepository)
{
    private readonly ContactMessageRepository _contactMessageRepository = contactMessageRepository;

    public async Task<IEnumerable<ContactMessageEntity>> GetAllAsync()
    {
        try
        {
            var messages = await _contactMessageRepository.GetAllAsync();
            if (messages != null)
            {
                return messages;
            }
            return null!;
        }
        catch (Exception)
        {

            return null!;
        }
    }

    public async Task<ContactMessageEntity> GetOneAsync(string id)
    {
        try
        {
            var message = await _contactMessageRepository.GetOneAsync(x => x.Id == id);
            if (message != null)
            {
                return message;
            }
            return null!;
        }
        catch (Exception)
        {
            return null!;
        }
    }

    public async Task<bool> CreateAsync(ContactMessageEntity contactEntity)
    {
        try
        {
            if (contactEntity != null)
            {
                var result = await _contactMessageRepository.CreateAsync(contactEntity);
                if (result != null)
                {
                    return true;
                }
            }
            return false;
        }
        catch { return false; }
        
        
    }
    public async Task<bool> DeleteOneAsync(ContactMessageEntity contactEntity)
    {
        try
        {
            if (contactEntity != null)
            {
                var result = await _contactMessageRepository.DeleteAsync(x => x.Id == contactEntity.Id);
                if (result)
                {
                    return true;
                }
            }
            return false;
        }
        catch { return false; }
    }
}
