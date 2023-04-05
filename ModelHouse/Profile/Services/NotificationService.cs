using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Repositories;
using ModelHouse.Profile.Domain.Services;
using ModelHouse.Profile.Domain.Services.Communication;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Shared.Domain.Repositories;

namespace ModelHouse.Profile.Services;

public class NotificationService: INotificationService
{
    private readonly IAccountRepository _userRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NotificationService(IAccountRepository userRepository, INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Notification>> ListAsync()
    {
        return await _notificationRepository.ListAsync();
    }

    public async Task<IEnumerable<Notification>> ListByUserId(long id)
    {
        return await _notificationRepository.ListByUserId(id);
    }

    public async Task<NotificationResponse> CreateAsync(Notification notification)
    {
        var user = await _userRepository.FindByIdAsync(notification.AccountId);
        if (user == null)
            return new NotificationResponse("Notification is not exist");
        try
        {
            await _notificationRepository.AddAsync(notification);
            await _unitOfWork.CompleteAsync();
            return new NotificationResponse(notification);
        }
        catch (Exception e)
        {
            return new NotificationResponse($"Failed to register Notification: {e.Message}");
        }
    }

    public async Task<NotificationResponse> DeleteAsync(long id)
    {
        var notification_exist = await _notificationRepository.FindById(id);
        if (notification_exist == null)
            return new NotificationResponse("The Notification is not exist");
        try
        {
            _notificationRepository.Delete(notification_exist);
            await _unitOfWork.CompleteAsync();
            return new NotificationResponse(notification_exist);
        }
        catch (Exception e)
        {
            return new NotificationResponse($"An error occurred while deleting the Notification: {e.Message} ");
        }
    }
}