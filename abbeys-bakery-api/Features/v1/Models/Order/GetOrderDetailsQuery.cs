using abbeys_bakery_api.Entities;
using AutoMapper;
using MediatR;

namespace abbeys_bakery_api.Features.v1.Models.Order
{
    public class GetOrderDetailsQuery
    {
        public class GetOrderDetailsRequest : IRequest<GetOrderDetailsResponse>
        {
            public string UniqueUserIdentifier { get; set; }
        }

        public class GetOrderDetailsResponse
        {
            public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        }

        public class OrderItem
        {
            public decimal? Price { get; set; }
            public string ItemTitle { get; set; }
            public string ItemDescription { get; set; }
            public string AllergyNotes { get; set; }
        }

        public class Handler : IRequestHandler<GetOrderDetailsRequest, GetOrderDetailsResponse>
        {
            private AbbeysBakeryContext _abbeysBakeryContext;
            private readonly IMapper _mapper;

            public Handler(AbbeysBakeryContext abbeysBakeryContext, IMapper mapper)
            {
                _abbeysBakeryContext = abbeysBakeryContext;
                _mapper = mapper;
            }

            public async Task<GetOrderDetailsResponse> Handle(GetOrderDetailsRequest request, CancellationToken cancellationToken)
            {
                GetOrderDetailsResponse getOrderDetailsResponse = new GetOrderDetailsResponse();
                var orderDetails = await this._abbeysBakeryContext.Procedures.GetOrderDetailsAsync(new Guid(request.UniqueUserIdentifier));
                List<OrderItem> items = new List<OrderItem>();
                foreach (var item in orderDetails)
                {
                    OrderItem orderItem = this._mapper.Map<OrderItem>(item);
                    items.Add(orderItem);
                }
                getOrderDetailsResponse.Items = items;
                return getOrderDetailsResponse;
            }
        }
    }
}
