using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Services.Comunication;

namespace ModelHouse.ServiceManagement.Domain.Services
{
    public interface IFavoriteService
    {
        Task<IEnumerable<Favorite>> ListAsync();
        Task<IEnumerable<Favorite>> ListByUserId(long id);
        Task<FavoriteResponse> CreateAsync(Favorite favorite);
        Task<FavoriteResponse> DeleteAsync(long id);
        Task<FavoriteResponse> UpdateAsync(long id, Order favorite);
    }
}