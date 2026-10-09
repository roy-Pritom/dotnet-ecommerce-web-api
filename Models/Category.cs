using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Web_Api.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();


        public required String Name { get; set; }

        public String? Description { get; set; }

        public String? ImageUrl { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}