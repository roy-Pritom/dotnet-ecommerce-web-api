using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Web_Api.DTOs
{
    public class CategoryUpdateDto
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters.")]
        public String Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Category description cannot exceed 500 characters.")]

        public String? Description { get; set; } = string.Empty;
        public String? ImageUrl { get; set; }
    }
}