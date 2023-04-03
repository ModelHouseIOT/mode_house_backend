using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Services.Communication;

namespace ModelHouse.Profile.Domain.Services;

public interface IMessageService
{
    Task<IEnumerable<Message>> ListAsync();
    Task<IEnumerable<Message>> ListByContactId(long contactId, long userId);
    Task<MessageResponse> CreateAsync(Message order);
    Task<MessageResponse> DeleteAsync(long id);
    Task<MessageResponse> UpdateAsync(long id, Message order);
    Task<MessageResponse> GetOrderById(long id);
}