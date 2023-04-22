using Microsoft.EntityFrameworkCore;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

namespace ModelHouse.Security.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateUser(User profile)
        {
            await _context.Users.AddAsync(profile);
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _context.Users.Include(p => p.Account).ToListAsync();
        }

        public async Task<User> GetUserById(long id)
        {
            return await _context.Users.Include(p => p.Account)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void UpdateUser(User profile)
        {
            _context.Users.Update(profile);
        }
    }
}