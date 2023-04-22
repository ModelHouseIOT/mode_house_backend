using Microsoft.EntityFrameworkCore;
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Repositories;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

namespace ModelHouse.ServiceManagement.Persistence.Repositories
{
    public class RequestRepository : BaseRepository, IRequestRepository
    {
        public RequestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateRequest(Request request)
        {
            await _context.Request.AddAsync(request);
        }

        public void DeleteRequest(Request request)
        {
            _context.Request.Remove(request);
        }

        public async Task<Request> FindById(long id)
        {
            return await _context.Request
                .Include(p => p.BusinessProfile)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Request>> ListAsync()
        {
            return await _context.Request
                .Include(p => p.BusinessProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<Request>> ListByBusinessProfileId(long id)
        {
            return await _context.Request.
                Where(p => p.BusinessProfileId == id)
                .Include(p => p.BusinessProfile)
                .ToListAsync();
        }

        public void UpdateRequest(Request request)
        {
            throw new NotImplementedException();
        }
    }
}