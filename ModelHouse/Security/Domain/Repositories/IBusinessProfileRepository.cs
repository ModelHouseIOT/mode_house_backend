using ModelHouse.Security.Domain.Models;

namespace ModelHouse.Security.Domain.Repositories
{
    public interface IBusinessProfileRepository
    {
        Task CreateBusinessProfile(BusinessProfile businessProfile);
        void UpdateUser(BusinessProfile businessProfile);
        Task<IEnumerable<BusinessProfile>> GetAllBusinessProfile();
        Task<BusinessProfile> GetBusinessProfileByAccountId(long id);
        Task<BusinessProfile> GetBusinessProfileById(long id);
    }
}
