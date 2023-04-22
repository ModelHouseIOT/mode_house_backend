using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Services.Comunication;

namespace ModelHouse.ServiceManagement.Domain.Services
{
    public interface IRequestService
    {
        Task<IEnumerable<Request>> ListAsync();
        Task<IEnumerable<Request>> ListByBusinessProfileId(long id);
        Task<RequestResponse> CreateAsync(Request order);
        Task<RequestResponse> DeleteAsync(long id);
        Task<RequestResponse> UpdateAsync(long id, Request order);
        Task<RequestResponse> GetOrderById(long id);
    }
}