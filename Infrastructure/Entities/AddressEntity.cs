using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AddressEntity
{
    public int Id { get; set; }

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
    public string AddressLine_1 { get; set; } = null!;
    public string? AddressLine_2 { get; set; }

    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public UserEntity User { get; set; } = null!;
}