using ModelHouse.Security.Domain.Models;

namespace ModelHouse.Security.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task CreateProject(Project project);
        void DeleteProject(Project project);
        void UpdateProject(Project project);
        Task<IEnumerable<Project>> GetAllProject();
        Task<Project> GetProjectById(long id);
        Task<IEnumerable<Project>> GetAllProjectByBusinessProfileId(long id);
    }
}