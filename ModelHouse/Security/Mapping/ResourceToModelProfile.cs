using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Resources;

namespace ModelHouse.Security.Mapping;

public class ResourceToModelProfile : AutoMapper.Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<RegisterRequest, User>();
        CreateMap<UpdateRequest, User>().ForAllMembers(options => 
            options.Condition((source, target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                    return true;
                }
                
            ));
        CreateMap<CreateProfileUseResource, ProfileUser>();
        CreateMap<UpdateProfileUserResource, ProfileUser>();
        CreateMap<GetProfileUserResource, ProfileUser>();
    }
}