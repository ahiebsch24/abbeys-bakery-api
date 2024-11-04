using abbeys_bakery_api.Features.v1.Models.BakeryItem;
using AutoMapper;

namespace abbeys_bakery_api.Configuration
{
    public class AutoMapper : Profile
    {
        public AutoMapper() { 
            CreateMap<abbeys_bakery_api.Entities.MenuItem, GetAllBakeryItemsQuery.BakeryItem>();
        }

    }
}
