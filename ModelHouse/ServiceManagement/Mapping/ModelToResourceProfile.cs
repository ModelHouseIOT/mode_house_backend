using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Resource.FavoriteResource;
using ModelHouse.ServiceManagement.Resource.NotificationResource;
using ModelHouse.ServiceManagement.Resource.OrderResource;
using ModelHouse.ServiceManagement.Resource.RequestResource;

namespace ModelHouse.ServiceManagement.Mapping
{
    public class ModelToResourceProfile: AutoMapper.Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Request, CreateRequestResource>();
            CreateMap<Request, GetRequestResource>();
            
            CreateMap<Favorite, CreateFavoriteResource>();
            CreateMap<Favorite, GetFavoriteResource>();
            
            CreateMap<Notification, CreateNotificationResource>();
            CreateMap<Notification, GetNotificationResource>();

            CreateMap<Order, CreateOrderResource>();
            CreateMap<Order, GetOrderResource>();
        }
    }
}