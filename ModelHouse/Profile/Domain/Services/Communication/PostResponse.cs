using ModelHouse.Profile.Domain.Models;
using ModelHouse.Shared.Domain.Services.Communication;

namespace ModelHouse.Profile.Domain.Services.Communication;

public class PostResponse: BaseResponse<Post>
{
    public PostResponse(Post resource) : base(resource)
    {
    }

    public PostResponse(string message) : base(message)
    {
    }
}