using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Resources;
using System.Security.Principal;
using ModelHouse.Security.Resources.AccountResource;

namespace ModelHouse.Security.Controllers
{
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
        [HttpGet("business")]
        public async Task<IActionResult> GetAllBusiness()
        {
            var account = await _accountService.ListBusinessAsync();
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRequest request)
        {
            var response = await _accountService.UpdateAsync(id, request);
            var account = _mapper.Map<Account, AccountResource>(response);
            return Ok(new { message = account });
        }
    
        [HttpPut("role/{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] ChangeRole request)
        {
            var response = await _accountService.UpdateRoleAsync(id, request);
            var account = _mapper.Map<Account, AccountResource>(response);
            return Ok(new { message = account });
        }
    
        [HttpPut("isActive/{id}")]
        public async Task<IActionResult> UpdateIsActive(int id, [FromBody] ChangeIsActive request)
        {
            var response = await _accountService.UpdateIsActiveAsync(id, request);
            var account = _mapper.Map<Account, AccountResource>(response);
            return Ok(new { message = account });
        }
    }
}