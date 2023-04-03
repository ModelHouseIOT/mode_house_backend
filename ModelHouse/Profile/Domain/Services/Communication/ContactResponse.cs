using ModelHouse.Profile.Domain.Models;
using ModelHouse.Shared.Domain.Services.Communication;

namespace ModelHouse.Profile.Domain.Services.Communication;

public class ContactResponse: BaseResponse<Contact>
{
    public ContactResponse(Contact resource) : base(resource)
    {
    }

    public ContactResponse(string message) : base(message)
    {
    }
}