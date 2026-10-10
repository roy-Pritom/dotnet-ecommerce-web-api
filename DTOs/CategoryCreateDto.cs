using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Web_Api.DTOs
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters.")]
        public string Name { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Category description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Url(ErrorMessage = "Image URL must be a valid URL.")]
        public string? ImageUrl { get; set; }
    }
}
