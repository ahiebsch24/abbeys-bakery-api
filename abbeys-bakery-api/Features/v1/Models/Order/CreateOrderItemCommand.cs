using abbeys_bakery_api.Entities;
using MediatR;

namespace abbeys_bakery_api.Features.v1.Models.Order
{
    public class CreateOrderItemCommand
    {
        public class CreateOrderItemRequest : IRequest
        {
            public string UniqueUserIdentifier { get; set; }
            public string UserId { get; set; }
        }


        public class Handler : IRequestHandler<CreateOrderItemRequest>
        {
            private AbbeysBakeryContext _abbeysBakeryContext;

            public Handler(AbbeysBakeryContext abbeysBakeryContext)
            {
                _abbeysBakeryContext = abbeysBakeryContext;
            }

            public async Task Handle(CreateOrderItemRequest request, CancellationToken cancellationToken)
            {
                await this._abbeysBakeryContext.Procedures.CreateOrderAsync(new Guid(request.UniqueUserIdentifier), new Guid(request.UserId));
                this._abbeysBakeryContext.SaveChanges();
            }
        }
    }
}
