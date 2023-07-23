using AutoMapper;
using SM.Application.Interfaces;
using SM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.Models.DTO
{
    public class CategoryDTO : IMapWith<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDTO>()
                .ReverseMap();
        }
    }
}
