using ModelHouse.Profile.Domain.Models;

namespace ModelHouse.Profile.Domain.Repositories;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> ListAsync();
    Task<IEnumerable<Contact>> ListByUserId(long id);
    Task<Contact> FindByIdAsync(long id);
    Task AddAsync(Contact order);
    void DeleteAsync(Contact order);
    void UpdateAsync(Contact order);
}