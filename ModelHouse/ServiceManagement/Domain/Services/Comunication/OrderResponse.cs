using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.Shared.Domain.Services.Communication;

namespace ModelHouse.ServiceManagement.Domain.Services.Comunication
{
    public class OrderResponse : BaseResponse<Order>
    {
        public OrderResponse(Order resource) : base(resource)
        {
        }

        public OrderResponse(string message) : base(message)
        {
        }
    }
}