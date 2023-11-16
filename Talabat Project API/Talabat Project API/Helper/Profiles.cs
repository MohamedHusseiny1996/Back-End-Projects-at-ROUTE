using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Talabat_Project_API.DTOS;

namespace Talabat_Project_API.Helper
{
	public class Profiles:Profile
	{
        public Profiles()
        {


			CreateMap<Product, ProductToReturnDto>()
			 .ForMember(o => o.ProductBrand, m => m.MapFrom(s => s.ProductBrand.Name))      //for member that has this name=> ProductBrandName map it from source that has this name ProductBrand.Name
			 .ForMember(o => o.ProductType, m => m.MapFrom(s => s.ProductType.Name))
			 .ForMember(o => o.PictureUrl, m => m.MapFrom<PictureUrlResolver>())
			 .ReverseMap();

			CreateMap<UserDto,LoginDto>().ReverseMap();
			CreateMap<UserDto, RegisterDto>().ReverseMap();
			CreateMap<AppUser, UserDto>().ReverseMap();
			CreateMap<Core.Entities.Identity.Addresse, UserAddresseDto>().ReverseMap();
			CreateMap<Core.Entities.OrderAggregation.Addresse, UserAddresseDto>().ReverseMap();
			CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
			CreateMap<BasketItemDto,BasketItem>().ReverseMap();
		}
    }
}
