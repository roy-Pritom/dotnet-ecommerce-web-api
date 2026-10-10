using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Web_Api.DTOs
{
    // Used by PUT: represents the full category, so Name is required
    public class CategoryUpdateDto
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters.")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Category description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Url(ErrorMessage = "Image URL must be a valid URL.")]
        public string? ImageUrl { get; set; }
    }
}
