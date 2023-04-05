using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Resources;
using System.Security.Principal;

namespace ModelHouse.Security.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AccountController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _accountService.Authenticate(request);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _accountService.RegisterAsync(request);
        return Ok(new { message = "Registration successful" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var account = await _accountService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Account>, IEnumerable<AccountResource>>(account);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var account = await _accountService.GetByIdAsync(id);
        var resource = _mapper.Map<Account, AccountResource>(account);

        return Ok(resource);
    }
    [HttpGet("find/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var account = await _accountService.GetByEmailAsync(email);
        var resource = _mapper.Map<Account, AccountResource>(account);

        return Ok(resource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateRequest request)
    {
        using var stream = new MemoryStream();
        var fileBytes = stream.ToArray();
        
        var response = "await _userService.UpdateAsync(id, request, fileBytes, foto.ContentType,Path.GetExtension(foto.FileName))";
        return Ok(new { message = response });
    }
}