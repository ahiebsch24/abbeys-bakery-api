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
            public decimal Price { get; set;}
            public int Quantity { get; set;}
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
                var cartItems = this._abbeysBakeryContext.CartItems.Where(x => x.UniqueUserId == uniqueUserId).ToList();
                foreach ( var cartItem in cartItems )
                {
                    UserCartItem item = this._mapper.Map<UserCartItem>(cartItem);
                    response.Items.Add(item);
                }
                return response;
            }
        }
    }
}
