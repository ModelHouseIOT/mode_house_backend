using ModelHouse.Security.Domain.Models;
using ModelHouse.Shared.Domain.Services.Communication;

namespace ModelHouse.Security.Domain.Services.Communication
{
    public class BusinessProfileResponse : BaseResponse<BusinessProfile>
    {
        public BusinessProfileResponse(BusinessProfile resource) : base(resource)
        {
        }
        public BusinessProfileResponse(string message) : base(message)
        {
        }
    }
}
