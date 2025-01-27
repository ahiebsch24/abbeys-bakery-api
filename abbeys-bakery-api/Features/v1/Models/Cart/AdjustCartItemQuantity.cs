using abbeys_bakery_api.Entities;
using MediatR;

namespace abbeys_bakery_api.Features.v1.Models.Cart
{
    public class AdjustCartItemQuantity
    {
        public class AdjustCartItemQuantityRequest : IRequest
        {
            public int Quantity { get; set; }
            public string UniqueUserId { get; set; }
            public string CartItemId { get; set; }
        }

        public class Handler : IRequestHandler<AdjustCartItemQuantityRequest>
        {
            private AbbeysBakeryContext _abbeysBakeryContext;

            public Handler(AbbeysBakeryContext abbeysBakeryContext)
            {
                _abbeysBakeryContext = abbeysBakeryContext;
            }

            public async Task Handle(AdjustCartItemQuantityRequest request, CancellationToken cancellationToken)
            {
                Guid UniqueUserId = new Guid(request.UniqueUserId);
                Guid CartId = new Guid(request.CartItemId);
                CartItem cartItem = this._abbeysBakeryContext.CartItems.Where(x => x.UniqueUserId == UniqueUserId && x.CartItemId == CartId).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity = request.Quantity;
                    this._abbeysBakeryContext.SaveChanges();
                }
            }
        }
    }
}
