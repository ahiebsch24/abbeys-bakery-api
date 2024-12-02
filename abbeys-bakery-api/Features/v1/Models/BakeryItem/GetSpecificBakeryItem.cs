using abbeys_bakery_api.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace abbeys_bakery_api.Features.v1.Models.BakeryItem
{
    public class GetSpecificBakeryItem
    {
        public class GetSpecificBakeryItemRequest : IRequest<GetSpecificBakeryItemResponse>
        {
            public Guid MenuItemGuid { get; set; }
        }

        public class GetSpecificBakeryItemResponse
        {
            public decimal Price { get; set; }
            public string ItemTitle { get; set; }
            public string ItemDescription { get; set; }
            public string AllergyDescription { get; set; }
            public Guid MenuItemGuid { get; set;}
            public string FileLocation { get; set; }

        }

        public class Handler : IRequestHandler<GetSpecificBakeryItemRequest, GetSpecificBakeryItemResponse>
        {
            private AbbeysBakeryContext _abbeysBakeryContext;
            private readonly IMapper _mapper;
            public Handler(AbbeysBakeryContext abbeysBakeryContext, IMapper mapper)
            {
                _abbeysBakeryContext = abbeysBakeryContext;
                _mapper = mapper;
            }

            public async Task<GetSpecificBakeryItemResponse>? Handle(GetSpecificBakeryItemRequest request, CancellationToken cancellationToken)
            {
                var MenuItems = this._abbeysBakeryContext.MenuItems;
                var MenuItem = await MenuItems.Where(x => x.MenuItemGuid == request.MenuItemGuid).FirstOrDefaultAsync(cancellationToken);
                if (MenuItem != null)
                {
                    return this._mapper.Map<GetSpecificBakeryItemResponse>(MenuItem);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
