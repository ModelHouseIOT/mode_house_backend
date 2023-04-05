using ModelHouse.Security.Domain.Models;

namespace ModelHouse.Security.Domain.Repositories;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> ListAsync();
    Task AddAsync(Account user);
    Task<Account> FindByIdAsync(long id);
    Task<Account> FindByEmailAsync(string email);
    bool ExistsByEmail(string username);
    Account FindById(long id);
    void Update(Account user);
    void Remove(Account user);
}