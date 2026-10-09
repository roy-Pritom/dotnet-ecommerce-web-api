using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Web_Api.DTOs
{
    public class CategoryUpdateDto
    {
        public required String Name { get; set; }
        public String? Description { get; set; } = "This is a category description";
        public String? ImageUrl { get; set; }
    }
}