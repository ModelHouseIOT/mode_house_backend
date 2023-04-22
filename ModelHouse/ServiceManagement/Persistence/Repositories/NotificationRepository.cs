using Microsoft.EntityFrameworkCore;
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Repositories;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

namespace ModelHouse.ServiceManagement.Persistence.Repositories
{
    public class NotificationRepository: BaseRepository, INotificationRepository
    {
        public NotificationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Notification>> ListAsync()
        {
            return await _context.Notifications
                .Include(p => p.Account)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> ListByAccountId(long id)
        {
            return await _context.Notifications
                .Where(p => p.AccountId == id)
                .Include(p => p.Account)
                .ToListAsync();
        }

        public async Task<Notification> GetNotificationById(long id)
        {
            return await _context.Notifications
                .Include(p => p.Account)
                .FirstOrDefaultAsync(p=>p.Id == id);
        }

        public async Task CreateNotification(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }

        public void DeleteNotification(Notification notification)
        {
            _context.Notifications.Remove(notification);
        }

        public void UpdateNotification(Notification notification)
        {
            _context.Notifications.Update(notification);
        }
    }
}