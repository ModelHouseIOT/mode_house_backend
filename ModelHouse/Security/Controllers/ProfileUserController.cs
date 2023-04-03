using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Resources;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services;
using ModelHouse.Security.Resources;
using ModelHouse.Shared.Extensions;
using MySqlX.XDevAPI.Common;

namespace ModelHouse.Security.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ProfileUserController: ControllerBase
    {
        private readonly IProfileUserService _profileUserService;
        private readonly IMapper _mapper;

        public ProfileUserController(IProfileUserService profileUserService, IMapper mapper)
        {
            _profileUserService = profileUserService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<GetProfileUserResource>> GetAllAsync()
        {
            var orders = await _profileUserService.GetAllProfile();
            var resources = _mapper.Map<IEnumerable<ProfileUser>, IEnumerable<GetProfileUserResource>>(orders);
            return resources;
        }
        [HttpGet("" +"user/{id}")]
        public async Task<IActionResult> GetByUserId(long id)
        {
            var profile = await _profileUserService.GetProfileByUserId(id);

            if (!profile.Success)
                return BadRequest(profile.Message);

            var resources = _mapper.Map<ProfileUser, GetProfileUserResource>(profile.Resource);
            return Ok(resources);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateProfileUseResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var profile = _mapper.Map<CreateProfileUseResource, ProfileUser>(resource);

            var result = await _profileUserService.CreateProfile(profile);

            if (!result.Success)
                return BadRequest(result.Message);

            var profileResource = _mapper.Map<ProfileUser, GetProfileUserResource>(result.Resource);
            return Ok(profileResource);
        }
    }
}
