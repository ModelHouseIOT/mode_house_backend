using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services.Communication;

namespace ModelHouse.Security.Domain.Services
{
    public interface IProfileUserService
    {
        Task<IEnumerable<ProfileUser>> GetAllProfile();
        Task<ProfileResponse> GetProfileByUserId(long id);
        Task<ProfileResponse> CreateProfile(ProfileUser profile);
        Task<ProfileResponse> UpdateProfile(ProfileUser profile);
    }
}
