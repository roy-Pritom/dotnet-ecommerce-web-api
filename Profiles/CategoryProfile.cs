namespace Ecommerce_Web_Api.Profiles
{
    public class CategoryProfile : AutoMapper.Profile
    {
        public CategoryProfile()
        {
            // Only entity -> read DTO is mapped automatically.
            // Create/update are written by hand through the Category entity's
            // constructor and Update/Patch methods so write logic stays explicit.
            CreateMap<Models.Category, DTOs.CategoryReadDto>();
        }
    }
}
