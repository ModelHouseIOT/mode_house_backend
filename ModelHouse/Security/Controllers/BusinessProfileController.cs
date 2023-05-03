using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services;
using ModelHouse.Security.Resources;
using ModelHouse.Security.Resources.BusinessProfileResource;
using ModelHouse.Security.Services;
using ModelHouse.Shared.Extensions;

namespace ModelHouse.Security.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class BusinessProfileController: ControllerBase
    {
        private readonly IBusinessProfileService businessProfileService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public BusinessProfileController(IBusinessProfileService businessProfileService, IMapper mapper, IAccountService accountService)
        {
            this.businessProfileService = businessProfileService;
            _mapper = mapper;
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<IEnumerable<GetBusinessProfileResource>> GetAllBusinessProfile()
        {
            var account = await businessProfileService.GetAllBusinessProfile();
            var resources = _mapper.Map<IEnumerable<BusinessProfile>, IEnumerable<GetBusinessProfileResource>>(account);
            return resources;
        }
        [HttpGet("" + "account/{id}")]
        public async Task<IActionResult> GetAllBusinessProfileByAccountId(long id)
        {
            var user = await businessProfileService.GetBusinessProfileByAccountId(id);

            if (!user.Success)
                return BadRequest(user.Message);

            var resources = _mapper.Map<BusinessProfile, GetBusinessProfileResource>(user.Resource);
            return Ok(resources);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBusinessProfile([FromBody] CreateBusinessProfileResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var businessProfile = _mapper.Map<CreateBusinessProfileResource, BusinessProfile>(resource);

            var result = await businessProfileService.CreateBusinessProfile(businessProfile);

            if (!result.Success)
                return BadRequest(result.Message);

            var profileResource = _mapper.Map<BusinessProfile, GetBusinessProfileResource>(result.Resource);
            var account = _accountService.UpdateBusinessProfileIdAsync(result.Resource.AccountId, result.Resource.Id);
            return Ok(profileResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBusinessProfile(long id, [FromForm] UpdateBusinessProfileResource input)
        {
            using var stream = new MemoryStream();
            IFormFile foto = input.Image;
            await foto.CopyToAsync(stream);
            var fileBytes = stream.ToArray();

            var response = await businessProfileService.UpdateBusinessProfile(id, input, fileBytes, foto.ContentType, Path.GetExtension(foto.FileName), "ImageBusinessProfile");
            var user = _mapper.Map<BusinessProfile, GetBusinessProfileResource>(response.Resource);

            return Ok(user);
        }
    }
}