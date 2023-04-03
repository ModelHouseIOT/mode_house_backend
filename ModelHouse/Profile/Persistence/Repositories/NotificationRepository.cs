using Microsoft.EntityFrameworkCore;
using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Repositories;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

namespace ModelHouse.Profile.Persistence.Repositories;

public class NotificationRepository: BaseRepository, INotificationRepository
{
    public NotificationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Notification>> ListAsync()
    {
        return await _context.Notifications
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task<Notification> FindById(long id)
    {
        return await _context.Notifications
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Notification>> ListByUserId(long id)
    {
        return await _context.Notifications
            .Where(p => p.UserId == id)
            .Include(p=>p.User)
            .ToListAsync();
    }

    public async Task AddAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
    }

    public void Delete(Notification notification)
    {
        _context.Notifications.Remove(notification);
    }
}