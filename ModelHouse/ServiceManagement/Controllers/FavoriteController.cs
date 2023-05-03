using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Services;
using ModelHouse.ServiceManagement.Resource.FavoriteResource;
using ModelHouse.ServiceManagement.Resource.OrderResource;
using ModelHouse.Shared.Extensions;

namespace ModelHouse.ServiceManagement.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class FavoriteController: ControllerBase
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IMapper _mapper;

        public FavoriteController(IFavoriteService favoriteService, IMapper mapper)
        {
            _favoriteService = favoriteService;
            _mapper = mapper;
        }
    
        [HttpGet]
        public async Task<IEnumerable<GetFavoriteResource>> GetAllAsync()
        {
            var favorites = await _favoriteService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Favorite>, IEnumerable<GetFavoriteResource>>(favorites);
            return resources;
        }
        [HttpGet("" +
                 "user/{id}")]
        public async Task<IEnumerable<GetFavoriteResource>> GetAllByUserId(long id)
        {
            var favorites = await _favoriteService.ListByUserId(id);
            var resources = _mapper.Map<IEnumerable<Favorite>, IEnumerable<GetFavoriteResource>>(favorites);
            return resources;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _favoriteService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var favoriteResource = _mapper.Map<Favorite, GetFavoriteResource>(result.Resource);
            return Ok(favoriteResource);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateFavoriteResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var favorite = _mapper.Map<CreateFavoriteResource, Favorite>(resource);
            var result = await _favoriteService.CreateAsync(favorite);
            if (!result.Success)
                return BadRequest(result.Message);
            var favoriteResource = _mapper.Map<Favorite, GetFavoriteResource>(result.Resource);
            return Ok(favoriteResource);
        }    

    }
}