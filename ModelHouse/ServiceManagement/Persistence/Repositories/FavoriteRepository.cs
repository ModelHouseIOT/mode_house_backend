using Microsoft.EntityFrameworkCore;
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Repositories;
using ModelHouse.ServiceManagement.Domain.Services;
using ModelHouse.ServiceManagement.Domain.Services.Comunication;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

namespace ModelHouse.ServiceManagement.Persistence.Repositories
{
    public class FavoriteRepository: BaseRepository, IFavoritesRepository
    {
        public FavoriteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Favorite>> ListAsync()
        {
            return await _context.Favorites
                .Include(p => p.Account)
                .ToListAsync();
        }

        public async Task<IEnumerable<Favorite>> ListByAccountId(long id)
        {
            return await _context.Favorites
                .Where(p => p.AccountId == id)
                .Include(p => p.Account)
                .ToListAsync();
        }

        public async Task CreateFavorite(Favorite favorite)
        {
            await _context.Favorites.AddAsync(favorite);
        }

        public async Task<Favorite> GetFavoriteById(long id)
        {
            return await _context.Favorites
                .Include(p => p.Account)
                .FirstOrDefaultAsync(p=>p.Id == id);
        }

        public void DeleteFavorite(Favorite favorite)
        {
            _context.Favorites.AddAsync(favorite);
        }

        public void UpdateFavorite(Favorite favorite)
        {
            _context.Favorites.AddAsync(favorite);
        }
    }
}