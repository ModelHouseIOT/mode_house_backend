using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Resources;

namespace ModelHouse.Security.Mapping;

public class ModelToResourceProfile : AutoMapper.Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, UserResource>();
        CreateMap<User, UpdateRequest>();


        CreateMap<ProfileUser, CreateProfileUseResource>();
        CreateMap<ProfileUser, UpdateProfileUserResource>();
        CreateMap<ProfileUser, GetProfileUserResource>();
    }
}