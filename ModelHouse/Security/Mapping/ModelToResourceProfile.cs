using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Resources;

namespace ModelHouse.Security.Mapping;

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

        CreateMap<BusinessProfile, CreateBusinessProfileResource>();
        CreateMap<BusinessProfile, UpdateBusinessProfileResource>();
        CreateMap<BusinessProfile, GetBusinessProfileResource>();
    }
}