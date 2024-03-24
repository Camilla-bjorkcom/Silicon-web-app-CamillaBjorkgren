using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class SubscriberEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string Email { get; set; } = null!;
    public bool AdvertisingUpdates { get; set; } = false;
    public bool WeekInReview { get; set; } = false;
    public bool Podcasts { get; set; } = false;
    public bool StartupsWeekly { get; set; } = false;
    public bool DailyNewsletter { get; set; } = false;
    public bool EventUpdates { get; set; } = false;

    public static implicit operator SubscriberEntity(SubscribeDto dto)
    {
        return new SubscriberEntity
        {
            Email = dto.Email,
            DailyNewsletter = dto.DailyNewsletter,
            AdvertisingUpdates = dto.AdvertisingUpdates,
            WeekInReview = dto.WeekInReview,
            EventUpdates = dto.EventUpdates,
            StartupsWeekly = dto.StartupsWeekly,
            Podcasts = dto.Podcasts,
        };

    }
}
