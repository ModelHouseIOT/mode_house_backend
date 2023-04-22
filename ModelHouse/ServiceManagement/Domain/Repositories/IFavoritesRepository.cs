using ModelHouse.ServiceManagement.Domain.Models;

namespace ModelHouse.ServiceManagement.Domain.Repositories
{
    public interface IFavoritesRepository
    {
        Task<IEnumerable<Favorite>> ListAsync();
        Task<IEnumerable<Favorite>> ListByAccountId(long id);
        Task CreateFavorite(Favorite favorite);
        Task<Favorite> GetFavoriteById(long id);
        void DeleteFavorite(Favorite favorite);
        void UpdateFavorite(Favorite favorite);
    }
}