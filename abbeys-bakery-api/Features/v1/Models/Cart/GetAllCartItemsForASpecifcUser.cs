using abbeys_bakery_api.Entities;
using AutoMapper;
using MediatR;

namespace abbeys_bakery_api.Features.v1.Models.Cart
{
    public class GetAllCartItemsForASpecificUser
    {
        public class GetAllCartItemsForASpecificUserRequest : IRequest<GetAllCartItemsForASpecificUserResponse>
        {
            public string UniqueUserId { get; set;}
        }

        public class GetAllCartItemsForASpecificUserResponse
        {
            public List<UserCartItem> Items { get; set;} = new List<UserCartItem>();
        }

        public class UserCartItem
        {
            public string ItemDescription { get; set;}
            public string Title { get; set;}
            public decimal? Price { get; set;}
            public int? Quantity { get; set;}
            public Guid CartItemId { get; set;}
        }

        public class Handler : IRequestHandler<GetAllCartItemsForASpecificUserRequest, GetAllCartItemsForASpecificUserResponse>
        {
            private AbbeysBakeryContext _abbeysBakeryContext;
            private readonly IMapper _mapper;

            public Handler(AbbeysBakeryContext abbeysBakeryContext, IMapper mapper)
            {
                _abbeysBakeryContext = abbeysBakeryContext;
                _mapper = mapper;
            }   

            public async Task<GetAllCartItemsForASpecificUserResponse> Handle(GetAllCartItemsForASpecificUserRequest request, CancellationToken cancellationToken)
            {
                Guid uniqueUserId = new Guid(request.UniqueUserId);
                GetAllCartItemsForASpecificUserResponse response = new GetAllCartItemsForASpecificUserResponse();
                var cartItems = from cartItem in this._abbeysBakeryContext.CartItems
                                join menuItem in this._abbeysBakeryContext.MenuItems
                                on cartItem.MenuItemGuid equals menuItem.MenuItemGuid
                                select new
                                {
                                    menuItem.ItemTitle,
                                    menuItem.ItemDescription,
                                    menuItem.Price,
                                    cartItem.Quantity,
                                    cartItem.CartItemId
                                };
                foreach ( var item in cartItems )
                {
                    UserCartItem cartItem = new UserCartItem();
                    cartItem.Title = item.ItemTitle;
                    cartItem.Quantity = item.Quantity;
                    cartItem.ItemDescription = item.ItemDescription;
                    cartItem.Price = item.Price;
                    cartItem.CartItemId = item.CartItemId;
                    response.Items.Add( cartItem );
                }
                return response;
            }
        }
    }
}
