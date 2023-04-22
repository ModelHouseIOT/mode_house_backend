using Microsoft.EntityFrameworkCore;
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Repositories;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

namespace ModelHouse.ServiceManagement.Persistence.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);

        }

        public void DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
        }

        public async Task<Order> FindByIdAsync(long id)
        {
            return await _context.Orders
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Order>> ListAsync()
        {
            return await _context.Orders
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> ListByUserId(long id)
        {
            return await _context.Orders.
                Where(p => p.UserId == id)
                .Include(p => p.User)
                .ToListAsync();
        }

        public void UpdateAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}