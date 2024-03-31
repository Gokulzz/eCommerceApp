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
            CreateMap<Role, RoleDTO>()
               .ForMember(dest => dest.roleId, opt => opt.MapFrom(src => src.RoleId))
               .ForMember(dest => dest.Role_Name, opt => opt.MapFrom(src => src.Role_Name))
               .ForMember(dest => dest.Role_Description, opt => opt.MapFrom(src => src.Role_Description));
            CreateMap<RoleDTO, Role>();
            CreateMap<ShippingAddressDTO, ShippingAddress>();
            CreateMap<Order, CheckOutOrderDTO>()
                .ForMember(dest => dest.orderId, opt => opt.MapFrom(src => src.orderId))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.totalAmount))
                .ForMember(dest => dest.GrandTotal, opt => opt.MapFrom(src => src.grandTotal.ToString("0.00")));
            CreateMap<UserDTO, User>();
            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.categoryId, opt => opt.MapFrom(src => src.categoryId))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description));
            CreateMap<Product, ProductResultDTO>()
                .ForMember(dest => dest.productId, opt => opt.MapFrom(src => src.productId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.filePath, opt => opt.MapFrom(src => src.FilePath));
            CreateMap<CategoryDTO, Category>(); 
            CreateMap<ProductDTO, Product>();
            CreateMap<OrderDetailDTO, Orderdetails>();
            CreateMap<PaginationFiltersDTO, PaginationFilters>();
           
        }
    }
}
