using ModelHouse.Security.Domain.Models;

namespace ModelHouse.Security.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task AddAsync(User user);
    Task<User> FindByIdAsync(long id);
    Task<User> FindByEmailAsync(string email);
    bool ExistsByEmail(string username);
    User FindById(long id);
    void Update(User user);
    void Remove(User user);
}