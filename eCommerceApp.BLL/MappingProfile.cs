using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerceApp.BLL.DTO;
using eCommerceApp.DAL.Models;

namespace eCommerceApp.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoleDTO, Role>();
            CreateMap<UserDTO, User>(); 
            CreateMap<CategoryDTO, Category>(); 
            CreateMap<ProductDTO, Product>();
            CreateMap<OrderDetailDTO, Orderdetails>();
           
        }
    }
}
