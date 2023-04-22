using ModelHouse.Security.Domain.Models;

namespace ModelHouse.Security.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(long id);
        Task CreateUser(User profile);
        void UpdateUser(User profile);
    }
}