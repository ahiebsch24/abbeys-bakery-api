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
            public List<BakeryItem> BakeryItems { get; set; } = new List<BakeryItem>();
        }

        public class BakeryItem
        {
            public string BakeryItemName { get; set; }
        }

        public class Handler : IRequestHandler<GetAllBakeryItemsRequest, GetAllBakeryItemsResponse>
        {
            public Handler()
            {

            }

            public async Task<GetAllBakeryItemsResponse> Handle(GetAllBakeryItemsRequest request, CancellationToken cancellationToken)
            {
                // TODO: Implement GetAllBakeryItems Handler method
                GetAllBakeryItemsResponse response = new GetAllBakeryItemsResponse();
                BakeryItem item = new BakeryItem();
                item.BakeryItemName = "Apple Pie";
                response.BakeryItems.Add(item);
                return response;
            }
        }
    }
}
