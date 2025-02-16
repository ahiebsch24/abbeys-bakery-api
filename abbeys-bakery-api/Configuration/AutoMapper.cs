using abbeys_bakery_api.Entities;
using abbeys_bakery_api.Features.v1.Models.BakeryItem;
using abbeys_bakery_api.Features.v1.Models.Cart;
using abbeys_bakery_api.Features.v1.Models.Order;
using AutoMapper;
using static abbeys_bakery_api.Features.v1.Models.BakeryItem.GetSpecificBakeryItem;

namespace abbeys_bakery_api.Configuration
{
    public class AutoMapper : Profile
    {
        public AutoMapper() { 
            CreateMap<abbeys_bakery_api.Entities.MenuItem, GetAllBakeryItemsQuery.BakeryItem>();
            CreateMap<abbeys_bakery_api.Entities.MenuItem, GetSpecificBakeryItemResponse>();
            CreateMap<abbeys_bakery_api.Entities.MenuItem, GetAllCartItemsForASpecificUser.UserCartItem>();
            CreateMap<abbeys_bakery_api.Entities.GetOrderDetailsResult, GetOrderDetailsQuery.OrderItem>();
        }

    }
}
