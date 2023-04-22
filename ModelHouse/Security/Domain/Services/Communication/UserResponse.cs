using ModelHouse.Security.Domain.Models;
using ModelHouse.Shared.Domain.Services.Communication;

namespace ModelHouse.Security.Domain.Services.Communication
{
    public class UserResponse : BaseResponse<User>
    {
        public UserResponse(User resource) : base(resource)
        {

        }

        public UserResponse(string message) : base(message)
        {

        }
    }
}