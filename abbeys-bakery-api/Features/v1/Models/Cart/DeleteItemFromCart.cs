using abbeys_bakery_api.Entities;
using AutoMapper;
using MediatR;

namespace abbeys_bakery_api.Features.v1.Models.Cart
{
    public class DeleteItemFromCart
    {
        public class DeleteItemFromCartRequest : IRequest
        {
            public string CartItemId { get; set; }
        }

        public class Handler : IRequestHandler<DeleteItemFromCartRequest>
        {
            private AbbeysBakeryContext _abbeysBakeryContext;

            public Handler(AbbeysBakeryContext abbeysBakeryContext)
            {
                _abbeysBakeryContext = abbeysBakeryContext;
            }

            public async Task Handle(DeleteItemFromCartRequest request, CancellationToken cancellationToken)
            {
                Guid CartItemId = new Guid(request.CartItemId);
                CartItem CartItemForDeleting = this._abbeysBakeryContext.CartItems.Where(x => x.Equals(CartItemId)).FirstOrDefault();
                if(CartItemForDeleting != null)
                {
                    this._abbeysBakeryContext.Remove(CartItemForDeleting);
                    this._abbeysBakeryContext.SaveChanges();
                }
            }
        }
    }
}
