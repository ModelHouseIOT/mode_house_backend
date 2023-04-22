using ModelHouse.ServiceManagement.Domain.Models;

namespace ModelHouse.ServiceManagement.Domain.Repositories
{
    public interface IRequestRepository
    {
        Task<IEnumerable<Request>> ListAsync();
        Task<IEnumerable<Request>> ListByBusinessProfileId(long id);
        Task<Request> FindById(long id);
        Task CreateRequest(Request request);
        void DeleteRequest(Request request);
        void UpdateRequest(Request request);
    }
}