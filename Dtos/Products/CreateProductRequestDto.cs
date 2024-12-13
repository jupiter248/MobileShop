using System;
using System.ComponentModel.DataAnnotations;

namespace MainApi.Dtos.Products;

public class CreateProductRequestDto
{
    [Required]
    [MaxLength(25, ErrorMessage = "Product name can not be over 25 characters")]
    public string ProductName { get; set; } = string.Empty;
    [Required]
    [MaxLength(25, ErrorMessage = "Brand can not be over 25 characters")]
    public string Brand { get; set; } = string.Empty;
    [Required]
    [MaxLength(15, ErrorMessage = "Model can not be over 25 characters")]
    public string Model { get; set; } = string.Empty;
    [Required]
    [Range(1, 100000000)]
    public decimal Price { get; set; }
    [Required]
    [Range(1, 100)]
    public int Quantity { get; set; }
    [Required]
    [MinLength(40 , ErrorMessage = "Description can not be Under 40 characters")]
    public string Description { get; set; } = string.Empty;
}
