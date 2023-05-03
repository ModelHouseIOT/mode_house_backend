using ModelHouse.Security.Domain.Repositories;
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Repositories;
using ModelHouse.ServiceManagement.Domain.Services;
using ModelHouse.ServiceManagement.Domain.Services.Comunication;
using ModelHouse.Shared.Domain.Repositories;

namespace ModelHouse.ServiceManagement.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IAccountRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequestService(IRequestRepository requestRepository, IAccountRepository userRepository, IUnitOfWork unitOfWork)
        {
            _requestRepository = requestRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse> CreateAsync(Request request)
        {
            var businessProfile = await _userRepository.FindByIdAsync(request.BusinessProfileId);
            if (businessProfile == null)
                return new RequestResponse("User is not exist");
            try
            {
                await _requestRepository.CreateRequest(request);
                await _unitOfWork.CompleteAsync();
                return new RequestResponse(request);
            }
            catch (Exception e)
            {
                return new RequestResponse($"Failed to register a Request: {e.Message}");
            }
        }

        public async Task<RequestResponse> DeleteAsync(long id)
        {
            var request_exist = await _requestRepository.FindById(id);
            if (request_exist == null)
                return new RequestResponse("the Request is not existing");
            try
            {
                _requestRepository.DeleteRequest(request_exist);
                await _unitOfWork.CompleteAsync();
                return new RequestResponse(request_exist);
            }
            catch (Exception e)
            {
                return new RequestResponse($"An error occurred while deleting the Request: {e.Message}");
            }
        }

        public async Task<RequestResponse> GetOrderById(long id)
        {
            try
            {
                var account = await _requestRepository.FindById(id);
                return new RequestResponse(account);
            }
            catch (Exception e)
            {
                return new RequestResponse($"Failed to find a current Business Profile Request: {e.Message}");
            }
        }

        public async Task<IEnumerable<Request>> ListAsync()
        {
            return await _requestRepository.ListAsync();
        }

        public async Task<IEnumerable<Request>> ListByBusinessProfileId(long id)
        {
            return await _requestRepository.ListByBusinessProfileId(id);
        }

        public Task<RequestResponse> UpdateAsync(long id, Request order)
        {
            throw new NotImplementedException();
        }
    }
}