using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Product;

public class ProductCreateDto : CreateOrUpdateProductDto
{
    [Required]
    [StringLength(50)]
    public string No { get; set; }
}