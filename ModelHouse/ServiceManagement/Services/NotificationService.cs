using ModelHouse.Security.Domain.Repositories;
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Repositories;
using ModelHouse.ServiceManagement.Domain.Services;
using ModelHouse.ServiceManagement.Domain.Services.Comunication;
using ModelHouse.Shared.Domain.Repositories;

namespace ModelHouse.ServiceManagement.Services
{
    public class NotificationService: INotificationService
    {
    
        private readonly INotificationRepository _notificationRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(INotificationRepository notificationRepository, IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Notification>> ListAsync()
        {
            return await _notificationRepository.ListAsync();
        }

        public async Task<IEnumerable<Notification>> ListByUserId(long id)
        {
            return await _notificationRepository.ListByAccountId(id);
        }

        public async Task<NotificationResponse> CreateAsync(Notification notification)
        {
            var user = await _accountRepository.FindByIdAsync(notification.AccountId);
            if (user == null)
                return new NotificationResponse("Account is not exist");
            notification.StartDate = DateTime.Now;
            try
            {
                await _notificationRepository.CreateNotification(notification);
                await _unitOfWork.CompleteAsync();
                return new NotificationResponse(notification);
            }
            catch  (Exception e)
            {
                return new NotificationResponse($"Failed to register a Notification: {e.Message}");
            }
        }

        public async Task<NotificationResponse> DeleteAsync(long id)
        {
            var favorite = await _notificationRepository.GetNotificationById(id);
            if (favorite == null)
                return new NotificationResponse("Notification is not exist");
        
            try
            {
                _notificationRepository.DeleteNotification(favorite);
                await _unitOfWork.CompleteAsync();
                return new NotificationResponse(favorite);
            }
            catch (Exception e)
            {
                return new NotificationResponse($"An error occurred while deleting the Favorite: {e.Message}");
            }
        }

        public Task<NotificationResponse> UpdateAsync(long id, Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}