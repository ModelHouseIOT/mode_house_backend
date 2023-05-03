using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.Shared.Domain.Services.Communication;

namespace ModelHouse.ServiceManagement.Domain.Services.Comunication
{
    public class NotificationResponse: BaseResponse<Notification>
    {
        public NotificationResponse(Notification resource) : base(resource)
        {
        }

        public NotificationResponse(string message) : base(message)
        {
        }
    }
}