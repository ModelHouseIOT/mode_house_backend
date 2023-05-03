using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Resources.AccountResource;

namespace ModelHouse.Security.Domain.Services
{
    public interface IAccountService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
        Task<IEnumerable<Account>> ListAsync();        
        Task<IEnumerable<Account>> ListBusinessAsync();
        Task<Account> GetByIdAsync(int id);
        Task<Account> GetByEmailAsync(string email);
        Task RegisterAsync(RegisterRequest request);
        Task<Account> UpdateAsync(int id, UpdateRequest request);
        Task<Account> UpdateRoleAsync(long id, ChangeRole request);
        Task<Account> UpdateIsActiveAsync(long id, ChangeIsActive request);
        Task<Account> UpdateUserProfileIdAsync(long id, long BusinessProfileId);
        Task<Account> UpdateBusinessProfileIdAsync(long id, long request);
        Task DeleteAsync(int id);
    }
}