using ModelHouse.Profile.Domain.Models;

namespace ModelHouse.Profile.Domain.Repositories;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> ListAsync();
    Task<IEnumerable<Message>> ListByContactId(long contactId, long userId);
    Task<Message> FindByIdAsync(long id);
    Task AddAsync(Message order);
    void DeleteAsync(Message order);
    void UpdateAsync(Message order);
}