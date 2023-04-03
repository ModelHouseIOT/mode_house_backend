using ModelHouse.Profile.Domain.Models;
using ModelHouse.Shared.Domain.Services.Communication;

namespace ModelHouse.Profile.Domain.Services.Communication;

public class NotificationResponse: BaseResponse<Notification>
{
    public NotificationResponse(Notification resource) : base(resource)
    {
    }

    public NotificationResponse(string message) : base(message)
    {
    }
}