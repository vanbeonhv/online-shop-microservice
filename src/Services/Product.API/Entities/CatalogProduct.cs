using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domains;

namespace Product.API.Entities;

public class CatalogProduct : EntityAuditBase<long>
{
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string No { get; set; }

    [Required]
    [Column(TypeName = "varchar(250)")]
    public string Name { get; set; }

    [Column(TypeName = "longtext")]
    public string Summary { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    public decimal Price { get; set; }
}