using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Resources;

namespace ModelHouse.Profile.Mapping;

public class ModelToResourceProfile: AutoMapper.Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Order, OrderResource>();
        CreateMap<Post, PostResource>();
        CreateMap<Notification, NotificationResource>();
        CreateMap<Contact, ContactResource>();
        CreateMap<Message, MessageResource>();
    }
}