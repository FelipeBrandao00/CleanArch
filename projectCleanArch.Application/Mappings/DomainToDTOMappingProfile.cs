using AutoMapper;
using projectCleanArch.Application.DTOs;
using projectCleanArch.Application.Products.Commands;
using projectCleanArch.Domain.Entities;

namespace projectCleanArch.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            
            CreateMap<ProductCreateCommand, ProductDTO>().ReverseMap();
            CreateMap<ProductUpdateCommand, ProductDTO>().ReverseMap();
        }
    }
}
