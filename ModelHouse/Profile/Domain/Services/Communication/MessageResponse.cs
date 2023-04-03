using ModelHouse.Profile.Domain.Models;
using ModelHouse.Shared.Domain.Services.Communication;

namespace ModelHouse.Profile.Domain.Services.Communication;

public class MessageResponse: BaseResponse<Message>
{
    public MessageResponse(Message resource) : base(resource)
    {
    }

    public MessageResponse(string message) : base(message)
    {
    }
}