using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Resources;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Resources;
using ModelHouse.Shared.Extensions;
using MySqlX.XDevAPI.Common;

namespace ModelHouse.Security.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<GetUserResource>> GetAllUser()
        {
            var account = await _userService.GetAllUser();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<GetUserResource>>(account);
            return resources;
        }
        [HttpGet("" +"account/{id}")]
        public async Task<IActionResult> GetByUserId(long id)
        {
            var user = await _userService.GetUserByUserId(id);

            if (!user.Success)
                return BadRequest(user.Message);

            var resources = _mapper.Map<User, GetUserResource>(user.Resource);
            return Ok(resources);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUseResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<CreateUseResource, User>(resource);

            var result = await _userService.CreateUser(user);

            if (!result.Success)
                return BadRequest(result.Message);

            var profileResource = _mapper.Map<User, GetUserResource>(result.Resource);
            return Ok(profileResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, [FromForm] UpdateUserResource input)
        {
            using var stream = new MemoryStream();
            IFormFile foto = input.Image;
            await foto.CopyToAsync(stream);
            var fileBytes = stream.ToArray();

            var response = await _userService.UpdateUser(id, input, fileBytes, foto.ContentType, Path.GetExtension(foto.FileName), "ImageUser");
            var user = _mapper.Map<User, GetUserResource>(response.Resource);

            return Ok(user);
        }
    }
}
