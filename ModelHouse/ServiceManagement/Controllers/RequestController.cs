using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Services;
using ModelHouse.ServiceManagement.Resource.RequestResource;
using ModelHouse.Shared.Extensions;

namespace ModelHouse.ServiceManagement.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class RequestController: ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly IMapper _mapper;

        public RequestController(IRequestService requestService, IMapper mapper)
        {
            _requestService = requestService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<GetRequestResource>> GetAllAsync()
        {
            var orders = await _requestService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Request>, IEnumerable<GetRequestResource>>(orders);
            return resources;
        }
        [HttpGet("" +
                 "user/{id}")]
        public async Task<IEnumerable<GetRequestResource>> GetAllByBusinessProfileId(long id)
        {
            var requests = await _requestService.ListByBusinessProfileId(id);
            var resources = _mapper.Map<IEnumerable<Request>, IEnumerable<GetRequestResource>>(requests);
            return resources;
        }
        [HttpGet("{id}")]
        public async Task<GetRequestResource> GetRequestById(long id)
        {
            var requestId = await _requestService.GetOrderById(id);
            var resources = _mapper.Map<Request, GetRequestResource>(requestId.Resource);
            return resources;
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(long id)
        {
            var result = await _requestService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var projectResource = _mapper.Map<Request, GetRequestResource>(result.Resource);

            return Ok(projectResource);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRequestAsync([FromBody] CreateRequestResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var request = _mapper.Map<CreateRequestResource, Request>(resource);

            var result = await _requestService.CreateAsync(request);

            if (!result.Success)
                return BadRequest(result.Message);
            var requestResource = _mapper.Map<Request, CreateRequestResource>(result.Resource);
            return Ok(requestResource);
        }
    }
}