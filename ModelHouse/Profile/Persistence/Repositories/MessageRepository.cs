using Microsoft.EntityFrameworkCore;
using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Repositories;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

namespace ModelHouse.Profile.Persistence.Repositories;

public class MessageRepository: BaseRepository, IMessageRepository
{
    public MessageRepository(AppDbContext context) : base(context)
    {
        
    }
    public async Task<IEnumerable<Message>> ListAsync()
    {
        return await _context.Messages
            .Include(p => p.Contact)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> ListByContactId(long contactId, long userId)
    {
        return await _context.Messages
            .Where(p => p.ContactId == contactId && p.UserId == userId)
            .Include(p => p.Contact)
            .ToListAsync();
    }

    public async Task<Message> FindByIdAsync(long id)
    {
        return await _context.Messages
            .Include(p => p.Contact)
            .FirstOrDefaultAsync(p=>p.Id == id);
    }

    public async Task AddAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
    }

    public void DeleteAsync(Message message)
    {
        _context.Remove(message);
    }

    public void UpdateAsync(Message message)
    {
        _context.Update(message);
    }
}