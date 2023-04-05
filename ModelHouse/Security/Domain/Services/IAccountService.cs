using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services.Communication;

namespace ModelHouse.Security.Domain.Services;

public interface IAccountService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
    Task<IEnumerable<Account>> ListAsync();
    Task<Account> GetByIdAsync(int id);

    Task<Account> GetByEmailAsync(string email);
    Task RegisterAsync(RegisterRequest request);
    Task<Account> UpdateAsync(int id, UpdateRequest request, byte[] file, string contentType,string extension, string container);
    Task DeleteAsync(int id);
}