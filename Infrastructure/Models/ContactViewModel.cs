using Infrastructure.Models;
namespace Infrastructure.Models;

public class ContactViewModel
{
    public ContactFormModel ContactForm { get; set; } = null!;

    public IEnumerable<ContactFormModel> Messages { get; set; } = new List<ContactFormModel>();

}
