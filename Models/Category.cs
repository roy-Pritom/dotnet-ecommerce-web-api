using System;

namespace Ecommerce_Web_Api.Models
{
    public class Category
    {
        // Required by EF Core to materialize entities from the database
        private Category() { }

        public Category(string name, string? description, string? imageUrl)
        {
            Id = Guid.NewGuid();
            Name = name.Trim();
            Description = description?.Trim();
            ImageUrl = imageUrl?.Trim();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; } = null!;

        public string? Description { get; private set; }

        public string? ImageUrl { get; private set; }

        public bool IsDeleted { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // Full replace (PUT): every field is overwritten with the given value
        public void Update(string name, string? description, string? imageUrl)
        {
            Name = name.Trim();
            Description = description?.Trim();
            ImageUrl = imageUrl?.Trim();
            UpdatedAt = DateTime.UtcNow;
        }

        // Partial update (PATCH): only non-null values are applied
        public void Patch(string? name, string? description, string? imageUrl)
        {
            if (name is not null) Name = name.Trim();
            if (description is not null) Description = description.Trim();
            if (imageUrl is not null) ImageUrl = imageUrl.Trim();
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
