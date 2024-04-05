using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Infrastructure.Services;
public class SubscriberService(SubscriberRepository subscriberRepository)
{
    private readonly SubscriberRepository _subscriberRepository = subscriberRepository;


    public async Task<bool> ExistsSubscriberAsync(string email)
    {
        try
        {
            var courseExisting = await _subscriberRepository.Exists(x => x.Email == email);
            return courseExisting;

        }
        catch (Exception)
        {

            return false!;
        }
    }

    public async Task<bool> AddSubscriberAsync(SubscriberEntity entity)
    {
        try
        {
            var result = await _subscriberRepository.CreateAsync(entity);
            if (result != null)
            {
                return true;
            }
            return false;

        }
        catch (Exception)
        {

            return false!;
        }
    }

    public async Task<IEnumerable<SubscriberEntity>> GetAllAsync()
    {
        try
        {
            var subscribers = await _subscriberRepository.GetAllAsync();
            if (subscribers != null)
            {
                return subscribers;
            }
            return null!;
        }
        catch (Exception)
        {

            return null!;
        }
    }

    public async Task<SubscriberEntity> GetOneAsyncEmail(string email)
    {
        try
        {
            var subscriber = await _subscriberRepository.GetOneAsync(x => x.Email == email);
            if (subscriber != null)
            {
                return subscriber;
            }
            return null!;
        }
        catch (Exception) { return null!; }
    }
    public async Task<SubscriberEntity> GetOneAsyncId(string id)
    {
        try
        {
            var subscriber = await _subscriberRepository.GetOneAsync(x => x.Id == id);
            if (subscriber != null)
            {
                return subscriber;
            }
            return null!;
        }
        catch (Exception) { return null!; }
    }

    public async Task<bool> DeleteSubscriberAsync(SubscriberEntity subscriber)
    {
        try
        {
            var result = await _subscriberRepository.DeleteAsync(x => x.Id == subscriber.Id);
            if (result)
                return true;
            return false;
        }
        catch (Exception)
        {

            return false!;
        }
    }
}
