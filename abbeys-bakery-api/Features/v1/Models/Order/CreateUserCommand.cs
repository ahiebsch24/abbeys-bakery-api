using abbeys_bakery_api.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace abbeys_bakery_api.Features.v1.Models.Order
{
    public class CreateUserCommand
    {
        public class CreateUserRequest : IRequest<CreateUserResponse>
        {
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? StreetAddress1 { get; set; }
            public string? StreetAddress2 { get; set; }
            public string? ZipCode { get; set; }
            public string? State { get; set; }
            public string? City { get; set; }
            public string? UniqueUserId { get; set; }
        }

        public class CreateUserResponse{
            public string UserID { get; set; }
        }

        public class Handler: IRequestHandler<CreateUserRequest, CreateUserResponse>
        {
            private AbbeysBakeryContext _abbeysBakeryContext;

            public Handler(AbbeysBakeryContext abbeysBakeryContext)
            {
                _abbeysBakeryContext = abbeysBakeryContext;
            }

            public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellation)
            {
                
                CreateUserResponse response = new CreateUserResponse();
                //Check if the User Already is in storage
                var alreadyInStorageUser = this._abbeysBakeryContext.Users.Where(x => x.UniqueUserId == new Guid(request.UniqueUserId)).FirstOrDefault();
                if (alreadyInStorageUser != null)
                {
                    response.UserID = alreadyInStorageUser.UserId.ToString();
                    return response;
                }
                else
                {
                    //Add the user's address
                    Address address = new Address();
                    address.StreetAddress1 = request.StreetAddress1;
                    address.StreetAddress2 = request.StreetAddress2;
                    address.ZipCode = request.ZipCode;
                    address.State = request.State;
                    this._abbeysBakeryContext.Add(address);
                    this._abbeysBakeryContext.SaveChanges();

                    var usersAddressId = address.AddressId;

                    //Add the user to the database
                    User user = new User();
                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.Email = request.Email;
                    user.AddressId = usersAddressId;
                    user.PhoneNumber = request.Phone;
                    this._abbeysBakeryContext.Add(user);
                    this._abbeysBakeryContext.SaveChanges();

                    var userId = user.UserId;
                    response.UserID = userId.ToString();
                    return response;
                }
            }
        }
    }
}
