using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Resources;

namespace ModelHouse.Profile.Mapping;

public class ResourceToModelProfile:AutoMapper.Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveOrderResource, Order>();
        CreateMap<SavePostResource, Post>();
 //           .ForMember(m=>m.Foto, options=>options.Ignore());
        CreateMap<SaveNotificationResource, Notification>();
        CreateMap<SaveContactResource, Contact>();
        CreateMap<SaveMessageResource, Message>();
    }
}