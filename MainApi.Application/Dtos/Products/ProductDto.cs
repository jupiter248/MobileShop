using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Comment;
using MainApi.Application.Dtos.Image;
using MainApi.Domain.Models;

namespace MainApi.Application.Dtos.Products;
public class ProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; } = string.Empty;
    public int categoryId { get; set; }
    public List<ImageDto>? Images { get; set; }
    public List<CommentDto>? Comments { get; set; }
}