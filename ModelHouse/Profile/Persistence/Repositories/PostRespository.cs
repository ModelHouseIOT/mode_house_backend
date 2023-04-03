using Microsoft.EntityFrameworkCore;
using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Repositories;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

namespace ModelHouse.Profile.Persistence.Repositories;

public class PostRepository: BaseRepository, IPostRepository
{
    public PostRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Post>> ListAsync()
    {
        return await _context.Posts
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Post>> ListByUserId(long id)
    {
        return await _context.Posts.Where(p => p.UserId == id)
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task<Post> FindByIdAsync(long id)
    {
        return await _context.Posts
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Post>> FindByTitleAsync(string title)
    {
        return await _context.Posts
            .Where(p=>p.Title == title)
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task AddAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
    }

    public void DeleteAsync(Post post)
    {
        _context.Remove(post);
    }

    public void UpdateAsync(Post post)
    {
        _context.Update(post);
    }
}