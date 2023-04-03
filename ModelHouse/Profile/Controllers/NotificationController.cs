using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Services;
using ModelHouse.Profile.Resources;
using ModelHouse.Shared.Extensions;

namespace ModelHouse.Profile.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class NotificationController: ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;

    public NotificationController(INotificationService notificationService, IMapper mapper)
    {
        _notificationService = notificationService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<NotificationResource>> GetAllAsync()
    {
        var notification = await _notificationService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notification);
        return resources;
    }
    [HttpGet("" + "user/{id}")]
    public async Task<IEnumerable<NotificationResource>> GetAllByUserId(long id)
    {
        var notifications= await _notificationService.ListByUserId(id);
        var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications);
        return resources;
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _notificationService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);

        return Ok(notificationResource);
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveNotificationResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var post = _mapper.Map<SaveNotificationResource, Notification>(resource);

        var result = await _notificationService.CreateAsync(post);
        if (!result.Success)
            return BadRequest(result.Message);

        var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);
        return Ok(notificationResource);
    }
}