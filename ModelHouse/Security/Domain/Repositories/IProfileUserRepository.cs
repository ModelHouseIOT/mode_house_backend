using ModelHouse.Security.Domain.Models;

namespace ModelHouse.Security.Domain.Repositories
{
    public interface IProfileUserRepository
    {
        Task<IEnumerable<ProfileUser>> GetAllProfile();
        Task<ProfileUser> GetProfileById(long id);
        Task CreateProfile(ProfileUser profile);
        void UpdateProfile(ProfileUser profile);
    }
}
