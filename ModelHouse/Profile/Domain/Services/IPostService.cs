using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Services.Communication;

namespace ModelHouse.Profile.Domain.Services;

public interface IPostService
{
    Task<IEnumerable<Post>> ListAsync();
    Task<IEnumerable<Post>> ListByUserId(long id);
    Task<PostResponse> CreateAsync(Post post, byte[] file,string contentType,string extension, string container);
    Task<PostResponse> DeleteAsync(long id);
    Task<PostResponse> UpdateAsync(long id, Post post);
    Task<IEnumerable<Post>> GetPostByTitle(string title);
}