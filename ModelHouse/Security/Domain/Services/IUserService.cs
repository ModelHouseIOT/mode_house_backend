using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Resources;
using ModelHouse.Security.Resources.UserProfileResource;

namespace ModelHouse.Security.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<UserResponse> GetUserByUserId(long id);
        Task<UserResponse> CreateUser(User profile);
        Task<UserResponse> UpdateUser(long id, UpdateUserResource profile, byte[] file, string contentType, string extension, string container);
    }
}