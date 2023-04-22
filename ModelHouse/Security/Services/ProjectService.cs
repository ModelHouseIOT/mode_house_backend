using ModelHouse.Security.Authorization.Handlers.Interfaces;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Security.Domain.Services;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Persistence.Repositories;
using ModelHouse.Security.Resources;
using ModelHouse.Security.Resources.ProjectResource;
using ModelHouse.Shared.Domain.Repositories;
using Org.BouncyCastle.Ocsp;

namespace ModelHouse.Security.Services
{
    public class ProjectService : IProjectService
    {

        private readonly IBusinessProfileRepository _businessProfileRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtHandler _jwtHandler;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectService(IBusinessProfileRepository businessProfileRepository, 
            IProjectRepository projectRepository, 
            IUnitOfWork unitOfWork, 
            IJwtHandler jwtHandler, 
            IWebHostEnvironment webHostEnvironment, 
            IHttpContextAccessor httpContextAccessor)
        {
            _businessProfileRepository = businessProfileRepository;
            this._projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
            _jwtHandler = jwtHandler;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProjectResponse> CreateProject(Project project)
        {
            var existingBusinessProfile = await _businessProfileRepository.GetBusinessProfileById(project.BusinessProfileId);
            if (existingBusinessProfile == null)
                return new ProjectResponse("Invalid Account");
            try
            {
                await _projectRepository.CreateProject(project);
                await _unitOfWork.CompleteAsync();

                return new ProjectResponse(project);
            }
            catch (Exception e)
            {
                return new ProjectResponse($"An error occurred while saving the Project: {e.Message}");
            }
        }

        public async Task<IEnumerable<Project>> GetAllProject()
        {
            return await _projectRepository.GetAllProject();
        }

        public async Task<IEnumerable<Project>> GetAllProjectByBusinessProfileId(long id)
        {
            return await _projectRepository.GetAllProjectByBusinessProfileId(id);
        }

        public async Task<Project> GetProjectById(long id)
        {
            return await _projectRepository.GetProjectById(id);
        }

        public async Task<ProjectResponse> UpdateProject(long id, UpdateProjectResource profile, byte[] file, string contentType, string extension, string container)
        {
            var project = await _projectRepository.GetProjectById(id);
            if (project == null)
                return new ProjectResponse("Project not found");
            project.Title = profile.Title;
            project.Description = profile.Description;
            try
            {
                string wwwrootPath = _webHostEnvironment.WebRootPath;
                if (string.IsNullOrEmpty(wwwrootPath))
                    throw new Exception();
                string carpetaArchivo = Path.Combine(wwwrootPath, container);
                if (!Directory.Exists(carpetaArchivo))
                    Directory.CreateDirectory(carpetaArchivo);
                string nombreFinal = $"{Guid.NewGuid()}{extension}";
                //System.Console.WriteLine(file);
                string rutaFinal = Path.Combine(carpetaArchivo, nombreFinal);
                System.Console.WriteLine(rutaFinal);
                File.WriteAllBytesAsync(rutaFinal, file);
                string urlActual =
                    $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
                string dbUrl = Path.Combine(urlActual, container, nombreFinal).Replace("\\", "/");

                project.Image = dbUrl;

                _projectRepository.UpdateProject(project);
                await _unitOfWork.CompleteAsync();
                return new ProjectResponse(project);
            }
            catch (Exception e)
            {
                return new ProjectResponse($"An error occurred while saving the area: {e.Message}");
            }
        }
    }
}