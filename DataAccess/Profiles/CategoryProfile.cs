using AutoMapper;
using DataAccess.DTOs.Categories;
using DataAccess.Models;

namespace DataAccess.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<UpdateCategoryDTO, Category>();
            CreateMap<CreateCategoryDTO, Category>();
        }
    }
}
