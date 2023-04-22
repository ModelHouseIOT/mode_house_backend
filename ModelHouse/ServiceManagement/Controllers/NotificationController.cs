using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Services;
using ModelHouse.ServiceManagement.Resource.FavoriteResource;
using ModelHouse.ServiceManagement.Resource.NotificationResource;
using ModelHouse.Shared.Extensions;

namespace ModelHouse.ServiceManagement.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class NotificationController:ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<GetNotificationResource>> GetAllAsync()
        {
            var notifications = await _notificationService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<GetNotificationResource>>(notifications);
            return resources;
        }
        [HttpGet("" +
                 "user/{id}")]
        public async Task<IEnumerable<GetNotificationResource>> GetAllByUserId(long id)
        {
            var notifications = await _notificationService.ListByUserId(id);
            var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<GetNotificationResource>>(notifications);
            return resources;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _notificationService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var notificationResource = _mapper.Map<Notification, GetNotificationResource>(result.Resource);
            return Ok(notificationResource);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateNotificationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var notification = _mapper.Map<CreateNotificationResource, Notification>(resource);
            var result = await _notificationService.CreateAsync(notification);
            if (!result.Success)
                return BadRequest(result.Message);
            var notificationResource = _mapper.Map<Notification, GetNotificationResource>(result.Resource);
            return Ok(notificationResource);
        }    
    }
}