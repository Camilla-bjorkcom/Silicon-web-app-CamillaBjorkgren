using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities;

public class UserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    public bool IsExternalAccount { get; set; } = false;

    //Behöver inte fylla i vid create
    public string? Bio { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Modified { get; set; }

    public int? AddressId { get; set; }
    public AddressEntity? Address { get; set; }
}
