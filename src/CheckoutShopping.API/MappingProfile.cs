using AutoMapper;

namespace CheckoutShopping.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Infrastructure.Models.Product, Service.Models.Product>().ReverseMap();
            CreateMap<Infrastructure.Models.ShoppingItem, Service.Models.ShoppingItem>().ReverseMap();
        }
    }
}
