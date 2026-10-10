using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Web_Api.Profiles
{
    public class CategoryProfile : AutoMapper.Profile
    {

        public CategoryProfile()
        {
            CreateMap<DTOs.CategoryCreateDto, Models.Category>();
            CreateMap<DTOs.CategoryUpdateDto, Models.Category>();
            CreateMap<Models.Category, DTOs.CategoryReadDto>();
        }
    }
}