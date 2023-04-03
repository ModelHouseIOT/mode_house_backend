using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Repositories;
using ModelHouse.Profile.Domain.Services;
using ModelHouse.Profile.Domain.Services.Communication;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Shared.Domain.Repositories;

namespace ModelHouse.Profile.Services;

public class MessageService: IMessageService
{
    private readonly IContactRepository _contactRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MessageService(IContactRepository contactRepository, IMessageRepository messageRepository, IUnitOfWork unitOfWork)
    {
        _contactRepository = contactRepository;
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Message>> ListAsync()
    {
        return await _messageRepository.ListAsync();
    }

    public async Task<IEnumerable<Message>> ListByContactId(long contactId, long userId)
    {
        return await _messageRepository.ListByContactId(contactId, userId);
    }

    public async Task<MessageResponse> CreateAsync(Message message)
    {
        var contact_exist = await _contactRepository.FindByIdAsync(message.ContactId);
        if (contact_exist == null)
            return new MessageResponse("The Contact User is not exist");
        var user_exist = await _contactRepository.FindByIdAsync(message.UserId);
        if (user_exist == null)
            return new MessageResponse("The User is not exist");
        try
        {
            await _messageRepository.AddAsync(message);
            await _unitOfWork.CompleteAsync();
            return new MessageResponse(message);
        }
        catch (Exception e)
        {
            return new MessageResponse($"Failed to register a Message: {e.Message}");
        }
    }

    public async Task<MessageResponse> DeleteAsync(long id)
    {
        var message_exist = await _messageRepository.FindByIdAsync(id);
        if (message_exist == null)
            return new MessageResponse("the Message is not existing");
        try
        {
            _messageRepository.DeleteAsync(message_exist);
            await _unitOfWork.CompleteAsync();
            return new MessageResponse(message_exist);
        }
        catch (Exception e)
        {
            return new MessageResponse($"An error occurred while deleting the Message: {e.Message}");
        }
    }

    public async Task<MessageResponse> UpdateAsync(long id, Message order)
    {
        var message_exist = await _messageRepository.FindByIdAsync(id);
        if (message_exist == null)
            return new MessageResponse("the Message is not existing");
        try
        {
            _messageRepository.UpdateAsync(message_exist);
            await _unitOfWork.CompleteAsync();
            return new MessageResponse(message_exist);
        }
        catch (Exception e)
        {
            return new MessageResponse($"An error occurred while deleting the Message: {e.Message}");
        }    }

    public async Task<MessageResponse> GetOrderById(long id)
    {
        try
        {
            var account = await _messageRepository.FindByIdAsync(id);
            return new MessageResponse(account);
        }
        catch (Exception e)
        {
            return new MessageResponse($"Failed to find a current user Message: {e.Message}");
        }
    }
}