using Infrastructure.Models;

namespace Infrastructure.Entities;
public class ContactMessageEntity
{
    public string Id { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string Message {  get; set; } = null!;
    public string? ServiceOption { get; set; }
    public DateTime Created { get; set; }

    public static implicit operator ContactMessageEntity(ContactFormModel dto)
    {
        return new ContactMessageEntity
        {
            Id = Guid.NewGuid().ToString(),
            FullName = dto.FullName,
            EmailAddress = dto.Email,
            Message = dto.Message,
            ServiceOption = dto.ServiceOption,
            Created = DateTime.Now,
        };
    }
}
