namespace Web_app_Camilla.ViewModels;

public class SubscriberViewModel
{
    public SubscriberModel? Subscriber { get; set; }
    public IEnumerable<SubscriberModel> Subscribers { get; set; } = new List<SubscriberModel>();
}
