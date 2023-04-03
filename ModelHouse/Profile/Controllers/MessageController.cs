using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Services;
using ModelHouse.Profile.Resources;
using ModelHouse.Shared.Extensions;

namespace ModelHouse.Profile.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class MessageController: ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IMapper _mapper;

    public MessageController(IMessageService messageService, IMapper mapper)
    {
        _messageService = messageService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IEnumerable<MessageResource>> GetAllAsync()
    {
        var messages = await _messageService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);
        return resources;
    }
    
    [HttpGet("" +
             "contact/{contactId}/user/{userId}")]
    public async Task<IEnumerable<MessageResource>> GetAllByUserId(long contactId, long userId)
    {
        var messages = await _messageService.ListByContactId(contactId, userId);
        var resources = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);
        return resources;
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _messageService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var messageResource = _mapper.Map<Message, MessageResource>(result.Resource);

        return Ok(messageResource);
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveMessageResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var message = _mapper.Map<SaveMessageResource, Message>(resource);

        var result = await _messageService.CreateAsync(message);
        if (!result.Success)
            return BadRequest(result.Message);

        var postResource = _mapper.Map<Message, MessageResource>(result.Resource);
        return Ok(postResource);
    }
}