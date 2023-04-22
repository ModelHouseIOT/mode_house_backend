using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

namespace ModelHouse.Security.Persistence.Repositories
{
    public class BusinessProfileRepository : BaseRepository, IBusinessProfileRepository
    {
        public BusinessProfileRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateBusinessProfile(BusinessProfile businessProfile)
        {
            await _context.BusinessProfiles.AddAsync(businessProfile);
        }

        public async Task<IEnumerable<BusinessProfile>> GetAllBusinessProfile()
        {
            return await _context.BusinessProfiles.Include(p => p.Account).ToListAsync();    
        }

        public async Task<BusinessProfile> GetBusinessProfileByAccountId(long id)
        {
            return await _context.BusinessProfiles.Include(p => p.Account)
                .FirstOrDefaultAsync(p => p.AccountId == id);
        }
        public async Task<BusinessProfile> GetBusinessProfileById(long id)
        {
            return await _context.BusinessProfiles.Include(p => p.Account)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public void UpdateUser(BusinessProfile businessProfile)
        {
            _context.BusinessProfiles.Update(businessProfile);
        }
    }
}