using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Services;
using ModelHouse.Profile.Resources;
using ModelHouse.Shared.Extensions;

namespace ModelHouse.Profile.Controllers;
[ApiController]
[Route("/api/v1/[controller]")]
public class PostController: ControllerBase
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;

    public PostController(IPostService postService, IMapper mapper)
    {
        _postService = postService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IEnumerable<PostResource>> GetAllAsync()
    {
        var posts = await _postService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(posts);
        return resources;
    }
    
    [HttpGet("" +
             "user/{id}")]
    public async Task<IEnumerable<PostResource>> GetAllByUserId(long id)
    {
        var posts = await _postService.ListByUserId(id);
        var resources = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(posts);
        return resources;
    }
    [HttpGet("{title}")]
    public async Task<IEnumerable<PostResource>> GetAccountByTitle(string title)
    {
        var posts = await _postService.GetPostByTitle(title);
        var postResources = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(posts);
        return postResources;
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _postService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var postResource = _mapper.Map<Post, PostResource>(result.Resource);

        return Ok(postResource);
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromForm] SavePostResource resource)
    {
        using var stream = new MemoryStream();
        IFormFile foto = resource.Foto;
        await foto.CopyToAsync(stream);
        var fileBytes = stream.ToArray();
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var post = _mapper.Map<SavePostResource, Post>(resource);

        var result = await _postService.CreateAsync(post, fileBytes, foto.ContentType,Path.GetExtension(foto.FileName), "ImagePost");
        if (!result.Success)
            return BadRequest(result.Message);

        var postResource = _mapper.Map<Post, PostResource>(result.Resource);
        return Ok(postResource);
    }
}