using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Services.Comunication;

namespace ModelHouse.ServiceManagement.Domain.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> ListAsync();
        Task<IEnumerable<Notification>> ListByUserId(long id);
        Task<NotificationResponse> CreateAsync(Notification notification);
        Task<NotificationResponse> DeleteAsync(long id);
        Task<NotificationResponse> UpdateAsync(long id, Notification notification);
    }
}