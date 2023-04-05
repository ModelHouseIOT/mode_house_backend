using ModelHouse.Security.Domain.Models;
using ModelHouse.Shared.Domain.Services.Communication;

namespace ModelHouse.Security.Domain.Services.Communication
{
    public class ProjectResponse : BaseResponse<Project>
    {
        public ProjectResponse(Project resource) : base(resource)
        {
        }

        public ProjectResponse(string message) : base(message)
        {
        }
    }
}
