using Microsoft.EntityFrameworkCore;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

namespace ModelHouse.Security.Persistence.Repositories
{
    public class ProfileUserRepository : BaseRepository, IProfileUserRepository
    {
        public ProfileUserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateProfile(ProfileUser profile)
        {
            await _context.Profiles.AddAsync(profile);
        }

        public async Task<IEnumerable<ProfileUser>> GetAllProfile()
        {
            return await _context.Profiles.Include(p => p.User).ToListAsync();
        }

        public async Task<ProfileUser> GetProfileById(long id)
        {
            return await _context.Profiles.Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void UpdateProfile(ProfileUser profile)
        {
            _context.Profiles.Update(profile);
        }
    }
}
