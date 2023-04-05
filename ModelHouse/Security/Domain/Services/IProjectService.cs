using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Resources;

namespace ModelHouse.Security.Domain.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProject();
        Task<IEnumerable<Project>> GetAllProjectByBusinessProfileId(long id);
        Task<Project> GetProjectById(long id);
        Task<ProjectResponse> CreateProject(Project profile);
        Task<ProjectResponse> UpdateProject(long id, UpdateProjectResource profile, byte[] file, string contentType, string extension, string container);

    }
}
