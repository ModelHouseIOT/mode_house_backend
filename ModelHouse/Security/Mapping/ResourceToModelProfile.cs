using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Resources;
using ModelHouse.Security.Resources.BusinessProfileResource;
using ModelHouse.Security.Resources.ProjectResource;
using ModelHouse.Security.Resources.UserProfileResource;

namespace ModelHouse.Security.Mapping
{
    public class ResourceToModelProfile : AutoMapper.Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<RegisterRequest, Account>();
            CreateMap<UpdateRequest, Account>().ForAllMembers(options => 
                options.Condition((source, target, property) =>
                    {
                        if (property == null) return false;
                        if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                        return true;
                    }
                
                ));

            CreateMap<CreateUseResource, User>();
            CreateMap<UpdateUserResource, User>();
            CreateMap<GetUserResource, User>();

            CreateMap<CreateProjectResource, Project>();
            CreateMap<UpdateProjectResource, Project>();
            CreateMap<GetProjectResource, Project>();

            CreateMap<CreateBusinessProfileResource, BusinessProfile>();
            CreateMap<UpdateBusinessProfileResource, BusinessProfile>();
            CreateMap<GetBusinessProfileResource, BusinessProfile>();
        }
    }
}