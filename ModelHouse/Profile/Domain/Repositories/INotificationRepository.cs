using ModelHouse.Profile.Domain.Models;

namespace ModelHouse.Profile.Domain.Repositories;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> ListAsync();
    Task<Notification> FindById(long id);
    Task<IEnumerable<Notification>> ListByUserId(long id);
    Task AddAsync(Notification notification);
    void Delete(Notification notification);
}