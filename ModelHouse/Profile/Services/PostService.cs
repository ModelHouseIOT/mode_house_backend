using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Repositories;
using ModelHouse.Profile.Domain.Services;
using ModelHouse.Profile.Domain.Services.Communication;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Shared.Domain.Repositories;

namespace ModelHouse.Profile.Services;

public class PostService: IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IAccountRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PostService(IUnitOfWork unitOfWork, IAccountRepository userRepository, IPostRepository postRepository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _postRepository = postRepository;
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Post>> ListAsync()
    {
        return await _postRepository.ListAsync();
    }

    public async Task<IEnumerable<Post>> ListByUserId(long id)
    {
        return await _postRepository.ListByUserId(id);
    }

    public async Task<PostResponse> CreateAsync(Post post, byte[] file, string contentType,string extension, string container)
    {
        var User = await _userRepository.FindByIdAsync(post.AccountId);
        if (User == null)
            return new PostResponse("User is not exist");
        
        try
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwrootPath))
                throw new Exception();
            string carpetaArchivo = Path.Combine(wwwrootPath, container);
            if (!Directory.Exists(carpetaArchivo))
                Directory.CreateDirectory(carpetaArchivo);
            string nombreFinal = $"{Guid.NewGuid()}{extension}";
            string rutaFinal = Path.Combine(carpetaArchivo, nombreFinal);
            File.WriteAllBytesAsync(rutaFinal, file);
            string urlActual =
                $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            string dbUrl = Path.Combine(urlActual, container, nombreFinal).Replace("\\", "/");

            post.Foto = dbUrl;
            
            await _postRepository.AddAsync(post);
            await _unitOfWork.CompleteAsync();
            return new PostResponse(post);
        }
        catch (Exception e)
        {
            return new PostResponse($"Failed to register a Project: {e.Message}");
        }
    }

    public async Task<PostResponse> DeleteAsync(long id)
    {
        var post_exist = await _postRepository.FindByIdAsync(id);
        if (post_exist == null)
            return new PostResponse("the Post is not existing");
        try
        {
            _postRepository.DeleteAsync(post_exist);
            await _unitOfWork.CompleteAsync();
            return new PostResponse(post_exist);
        }
        catch (Exception e)
        {
            return new PostResponse($"An error occurred while deleting the Post: {e.Message}");
        }
    }

    public Task<PostResponse> UpdateAsync(long id, Post post)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Post>> GetPostByTitle(string title)
    {
        return await _postRepository.FindByTitleAsync(title);
    }
}
