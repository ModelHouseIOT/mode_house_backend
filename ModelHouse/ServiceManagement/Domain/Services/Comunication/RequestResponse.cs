using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.Shared.Domain.Services.Communication;

namespace ModelHouse.ServiceManagement.Domain.Services.Comunication
{
    public class RequestResponse : BaseResponse<Request>
    {
        public RequestResponse(Request resource) : base(resource)
        {
        }

        public RequestResponse(string message) : base(message)
        {
        }
    }
}