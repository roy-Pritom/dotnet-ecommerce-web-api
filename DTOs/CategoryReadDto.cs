using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Web_Api.DTOs
{
    public class CategoryReadDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required String Name { get; set; }

        public String Description { get; set; } = "This is a category description";

        public String? ImageUrl { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}