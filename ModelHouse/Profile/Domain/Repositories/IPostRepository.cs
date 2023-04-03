using ModelHouse.Profile.Domain.Models;

namespace ModelHouse.Profile.Domain.Repositories;

public interface IPostRepository
{
    Task<IEnumerable<Post>> ListAsync();
    Task<IEnumerable<Post>> ListByUserId(long id);
    Task<Post> FindByIdAsync(long id);
    Task<IEnumerable<Post>> FindByTitleAsync(string title);
    Task AddAsync(Post post);
    void DeleteAsync(Post post);
    void UpdateAsync(Post post);
}