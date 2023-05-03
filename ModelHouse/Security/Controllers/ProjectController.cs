using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services;
using ModelHouse.Security.Resources;
using ModelHouse.Shared.Extensions;
using System.Security.Principal;
using ModelHouse.Security.Resources.ProjectResource;

namespace ModelHouse.Security.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ProjectController: ControllerBase
    {
        private readonly IProjectService projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            this.projectService = projectService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<GetProjectResource>> GetAllProjects()
        {
            var account = await projectService.GetAllProject();
            var resources = _mapper.Map<IEnumerable<Project>, IEnumerable<GetProjectResource>>(account);
            return resources;
        }
        [HttpGet("" + "businessProfile/{id}")]
        public async Task<IEnumerable<GetProjectResource>> GetAllProjectByBusinessProfileId(long id)
        {
            var projects = await projectService.GetAllProjectByBusinessProfileId(id);

            var resources = _mapper.Map<IEnumerable<Project>, IEnumerable<GetProjectResource>>(projects);
            return resources;
        }
        [HttpGet("{id}")]
        public async Task<GetProjectResource> GetProjectById(long id)
        {
            var projects = await projectService.GetProjectById(id);

            var resources = _mapper.Map<Project, GetProjectResource>(projects); 

            return resources;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBusinessProfile([FromBody] CreateProjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var project = _mapper.Map<CreateProjectResource, Project>(resource);

            var result = await projectService.CreateProject(project);

            if (!result.Success)
                return BadRequest(result.Message);

            var projectResource = _mapper.Map<Project, GetProjectResource>(result.Resource);
            return Ok(projectResource);
            return Ok(projectResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBusinessProfile(long id, [FromForm] UpdateProjectResource input)
        {
            using var stream = new MemoryStream();
            IFormFile foto = input.Image;
            await foto.CopyToAsync(stream);
            var fileBytes = stream.ToArray();

            var response = await projectService.UpdateProject(id, input, fileBytes, foto.ContentType, Path.GetExtension(foto.FileName), "ImageBusinessProfile");
            var project = _mapper.Map<Project, GetProjectResource>(response.Resource);

            return Ok(project);
        }
    }
}