using MediatR;

namespace abbeys_bakery_api.Features.v1.Models.BakeryItem
{
    public class GetAllBakeryItemsQuery
    {
        public class GetAllBakeryItemsRequest : IRequest<GetAllBakeryItemsResponse>
        {

        }

        public class GetAllBakeryItemsResponse
        {
            public List<BakeryItem> BakeryItems { get; set; }
        }

        public class BakeryItem
        {

        }

        public class Handler : IRequestHandler<GetAllBakeryItemsRequest, GetAllBakeryItemsResponse>
    }
}
