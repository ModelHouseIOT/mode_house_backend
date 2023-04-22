using Microsoft.EntityFrameworkCore;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

namespace ModelHouse.Security.Persistence.Repositories
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Account>> ListAsync()
        {
            return await _context.Accounts.ToListAsync();

        }

        public async Task AddAsync(Account user)
        {
            await _context.Accounts.AddAsync(user);
        }

        public async Task<Account> FindByIdAsync(long id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Account> FindByEmailAsync(string email)
        {
            return await _context.Accounts.SingleOrDefaultAsync(x => x.EmailAddress == email);
        }

        public bool ExistsByEmail(string email)
        {
            return _context.Accounts.Any(x => x.EmailAddress == email);
        }

        public Account FindById(long id)
        {
            return _context.Accounts.Include(p=>p.User).FirstOrDefault(p=>p.Id == id);
        }

        public void Update(Account user)
        {
            _context.Accounts.Update(user);
        }

        public void Remove(Account user)
        {
            _context.Accounts.Remove(user);
        }
    }
}