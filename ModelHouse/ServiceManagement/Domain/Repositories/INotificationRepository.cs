using ModelHouse.ServiceManagement.Domain.Models;

namespace ModelHouse.ServiceManagement.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> ListAsync();
        Task<IEnumerable<Notification>> ListByAccountId(long id);
        Task<Notification> GetNotificationById(long id);
        Task CreateNotification(Notification notification);
        void DeleteNotification(Notification notification);
        void UpdateNotification(Notification notification);
    }
}