using ModelHouse.Security.Domain.Models;
using ModelHouse.Shared.Domain.Services.Communication;

namespace ModelHouse.Security.Domain.Services.Communication
{
    public class ProfileResponse : BaseResponse<ProfileUser>
    {
        public ProfileResponse(ProfileUser resource) : base(resource)
        {

        }

        public ProfileResponse(string message) : base(message)
        {
        }
    }
}
