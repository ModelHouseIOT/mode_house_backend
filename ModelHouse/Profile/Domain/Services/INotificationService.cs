using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Services.Communication;

namespace ModelHouse.Profile.Domain.Services;

public interface INotificationService
{
    Task<IEnumerable<Notification>> ListAsync();
    Task<IEnumerable<Notification>> ListByUserId(long id);
    Task<NotificationResponse> CreateAsync(Notification notification);
    Task<NotificationResponse> DeleteAsync(long id);
}