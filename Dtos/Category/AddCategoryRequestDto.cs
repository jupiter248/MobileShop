using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Category
{
    public class AddCategoryRequestDto
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Category name can not be over 25 characters")]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        [MinLength(20, ErrorMessage = "Description can not be Under 20 characters")]
        public string Description { get; set; } = string.Empty;
    }
}