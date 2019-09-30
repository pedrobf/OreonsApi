using AutoMapper;
using OreonsApi.core.Models;
using OreonsApi.Models;

namespace OreonsApi.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<SubCategoryDTO, SubCategory>().ReverseMap();
            CreateMap<SubCategoryCreateDTO, SubCategory>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<ProductCreateDTO, Product>().ReverseMap();
            CreateMap<ProductUpdateDTO, Product>().ReverseMap();
        }
    }
}
