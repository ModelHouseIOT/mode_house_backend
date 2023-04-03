using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Services.Communication;

namespace ModelHouse.Profile.Domain.Services;

public interface IContactService
{
    Task<IEnumerable<Contact>> ListAsync();
    Task<IEnumerable<Contact>> ListByUserId(long id);
    Task<ContactResponse> CreateAsync(Contact order);
    Task<ContactResponse> DeleteAsync(long id);
    Task<ContactResponse> UpdateAsync(long id, Contact order);
    Task<ContactResponse> GetOrderById(long id);
}