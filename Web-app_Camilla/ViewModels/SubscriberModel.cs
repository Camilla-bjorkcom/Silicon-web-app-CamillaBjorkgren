using System.ComponentModel.DataAnnotations;

namespace Web_app_Camilla.ViewModels;

public class SubscriberModel
{
    [Required(ErrorMessage ="You must enter a valid email.")]
    [Display(Name = "Subscribe", Prompt ="Enter your email address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Advertising Updates")]
    public bool AdvertisingUpdates { get; set; } = false;

    [Display(Name = "Week in Review")]
    public bool WeekInReview { get; set; } = false;

    [Display(Name = "Posdcasts")]
    public bool Podcasts { get; set; } = false;

    [Display(Name = "Startups Weekly")]
    public bool StartupsWeekly { get; set; } = false;

    [Display(Name = "Daily Newsletter")]
    public bool DailyNewsletter { get; set; } = false;

    [Display(Name = "Event Updates")]
    public bool EventUpdates { get; set; } = false;

    public bool IsSubscribed { get; set; } = false;
}

