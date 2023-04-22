using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Resources;
using ModelHouse.Security.Resources.AccountResource;
using ModelHouse.Security.Resources.BusinessProfileResource;
using ModelHouse.Security.Resources.ProjectResource;
using ModelHouse.Security.Resources.UserProfileResource;

namespace ModelHouse.Security.Mapping
{
    public class ModelToResourceProfile : AutoMapper.Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Account, AuthenticateResponse>();
            CreateMap<Account, AccountResource>();
            CreateMap<Account, UpdateRequest>();

            CreateMap<User, CreateUseResource>();
            CreateMap<User, UpdateUserResource>();
            CreateMap<User, GetUserResource>();

            CreateMap<Project, CreateProjectResource>();
            CreateMap<Project, UpdateProjectResource>();
            CreateMap<Project, GetProjectResource>();

            CreateMap<BusinessProfile, CreateBusinessProfileResource>();
            CreateMap<BusinessProfile, UpdateBusinessProfileResource>();
            CreateMap<BusinessProfile, GetBusinessProfileResource>();
        }
    }
}