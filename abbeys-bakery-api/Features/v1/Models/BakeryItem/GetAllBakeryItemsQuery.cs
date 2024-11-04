using abbeys_bakery_api.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace abbeys_bakery_api.Features.v1.Models.BakeryItem
{
    public class GetAllBakeryItemsQuery
    {
        public class GetAllBakeryItemsRequest : IRequest<GetAllBakeryItemsResponse>
        {

        }

        public class GetAllBakeryItemsResponse
        {
            public List<BakeryItem> BakeryItems { get; set; } = new List<BakeryItem>();
        }

        public class BakeryItem
        {
            public string ItemTitle { get; set; }
        }

        public class Handler : IRequestHandler<GetAllBakeryItemsRequest, GetAllBakeryItemsResponse>
        {
            private AbbeysBakeryContext _abbeysBakeryContext;
            private readonly IMapper _mapper;
            public Handler(AbbeysBakeryContext abbeysBakeryContext, IMapper mapper)
            {
                _abbeysBakeryContext = abbeysBakeryContext;
                _mapper = mapper;
            }

            public async Task<GetAllBakeryItemsResponse> Handle(GetAllBakeryItemsRequest request, CancellationToken cancellationToken)
            {
                // TODO: Implement GetAllBakeryItems Handler method
                GetAllBakeryItemsResponse response = new GetAllBakeryItemsResponse();
                var bakeryItems = await this._abbeysBakeryContext.MenuItems.ToListAsync(cancellationToken);
                foreach (var item in bakeryItems)
                {
                    BakeryItem bakeryItem = _mapper.Map<BakeryItem>(item);
                    response.BakeryItems.Add(bakeryItem);
                }
                return response;
            }
        }
    }
}
