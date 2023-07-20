using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Interfaces;
using SM.Domain.Entities;

namespace SM.Application.Models.DTO
{
    public class ProductCreateDTO : IMapWith<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string BrandName { get; set; }
        public string? ImgURL { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }



        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductCreateDTO>()
                .ForMember(d => d.CategoryName, 
                    opt => opt.MapFrom(src => src.Category.Name));
            profile.CreateMap<ProductCreateDTO, Product>();
        }
    }
}
