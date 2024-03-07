using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class AddressEntity
{
    [Key]
    public int Id { get; set; }
    public string AddressLine_1 { get; set; } = null!;
    public string AddressLine_2 { get; set; } = null!;

    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

    //En address kan vara kopplad till en eller flera Users, en till många-relation
    public ICollection<UserEntity> Users { get; set; } = [];
}
