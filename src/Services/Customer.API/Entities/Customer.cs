using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace Customer.API.Entities;

public class Customer : EntityBase<long>
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string FirstName { get; set; }

    [Required]
    [Column(TypeName = "varchar(150)")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }
}
