using Microsoft.EntityFrameworkCore;
using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Repositories;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

namespace ModelHouse.Profile.Persistence.Repositories;

public class ContactRepository: BaseRepository, IContactRepository
{

    public ContactRepository(AppDbContext context) : base(context)
    {
        
    }
    public async Task<IEnumerable<Contact>> ListAsync()
    {
        return await _context.Contacts
            .Include(p => p.Account)
            .ToListAsync(); 
    }

    public async Task<IEnumerable<Contact>> ListByUserId(long id)
    {
        return await _context.Contacts.Where(p => p.UserId == id)
            .Include(p => p.Account)
            .ToListAsync();
    }

    public async Task<Contact> FindByIdAsync(long id)
    {
        return await _context.Contacts
            .Include(p => p.Account)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Contact contact)
    {
        await _context.Contacts.AddAsync(contact);
    }

    public void DeleteAsync(Contact contact)
    {
        _context.Remove(contact);
    }

    public void UpdateAsync(Contact contact)
    {
        _context.Update(contact);
    }
}