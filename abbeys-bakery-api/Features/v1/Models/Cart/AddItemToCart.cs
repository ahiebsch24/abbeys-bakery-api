

using abbeys_bakery_api.Entities;
using AutoMapper;
using MediatR;

namespace abbeys_bakery_api.Features.v1.Models.Cart
{
    public class AddItemToCart
    {
        public class AddItemToCartRequest : IRequest
        {
            public string UserId { get; set; }
            public string MenuItemGuid { get; set; }
            public int Quantity { get; set; }
        }

        public class Handler : IRequestHandler<AddItemToCartRequest>
        {
            private AbbeysBakeryContext _abbeysBakeryContext;
            private readonly IMapper _mapper;

            public Handler(AbbeysBakeryContext abbeysBakeryContext, IMapper mapper)
            {
                _abbeysBakeryContext = abbeysBakeryContext;
                _mapper = mapper;
            }

            public async Task Handle(AddItemToCartRequest request, CancellationToken cancellationToken)
            {
                Guid UserId = new Guid(request.UserId);
                Guid MenuItemGuid = new Guid(request.MenuItemGuid);
                // Make sure that if the user already has the item you just increase the quantithy
                var userAlreadyHasItem = this._abbeysBakeryContext.CartItems.Where(x => x.MenuItemGuid == MenuItemGuid).FirstOrDefault();
                if(userAlreadyHasItem != null)
                {
                    userAlreadyHasItem.Quantity = request.Quantity + userAlreadyHasItem.Quantity;
                    this._abbeysBakeryContext.SaveChanges();
                }
                else
                {
                    CartItem cartItem = new CartItem();
                    cartItem.MenuItemGuid = MenuItemGuid;
                    cartItem.UniqueUserId = UserId;
                    cartItem.Quantity = request.Quantity;
                    this._abbeysBakeryContext.Add(cartItem);
                    this._abbeysBakeryContext.SaveChanges();
                }
            }
        }
    }
}
