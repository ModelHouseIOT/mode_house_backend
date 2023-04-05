using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Resources;

namespace ModelHouse.Security.Domain.Services
{
    public interface IBusinessProfileService
    {
        Task<IEnumerable<BusinessProfile>> GetAllBusinessProfile();
        Task<BusinessProfileResponse> GetBusinessProfileByAccountId(long id);
        Task<BusinessProfileResponse> CreateBusinessProfile(BusinessProfile profile);
        Task<BusinessProfileResponse> UpdateBusinessProfile(long id, UpdateBusinessProfileResource profile, byte[] file, string contentType, string extension, string container);
    }
}
