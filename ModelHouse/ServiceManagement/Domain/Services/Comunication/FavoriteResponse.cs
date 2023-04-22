using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.Shared.Domain.Services.Communication;

namespace ModelHouse.ServiceManagement.Domain.Services.Comunication
{
    public class FavoriteResponse: BaseResponse<Favorite>
    {
        public FavoriteResponse(Favorite resource) : base(resource)
        {
        }

        public FavoriteResponse(string message) : base(message)
        {
        }
    }
}