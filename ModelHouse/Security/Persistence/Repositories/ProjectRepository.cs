using Microsoft.EntityFrameworkCore;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Shared.Persistence.Contexts;
using ModelHouse.Shared.Persistence.Repositories;

namespace ModelHouse.Security.Persistence.Repositories
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateProject(Project project)
        {
            await _context.Projects.AddAsync(project);
        }

        public void DeleteProject(Project project)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Project>> GetAllProjectByBusinessProfileId(long id)
        {
            return await _context.Projects.Include(p => p.BusinessProfile)
                .Where(p => p.BusinessProfileId == id).ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetAllProject()
        {
            return await _context.Projects.Include(p => p.BusinessProfile).ToListAsync();
        }

        public async Task<Project> GetProjectById(long id)
        {
            return await _context.Projects.Include(p => p.BusinessProfile)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void UpdateProject(Project project)
        {
            _context.Projects.Update(project);
        }
    }
}