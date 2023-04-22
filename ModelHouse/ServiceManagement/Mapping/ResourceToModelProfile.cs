using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Resource.FavoriteResource;
using ModelHouse.ServiceManagement.Resource.NotificationResource;
using ModelHouse.ServiceManagement.Resource.OrderResource;
using ModelHouse.ServiceManagement.Resource.RequestResource;

namespace ModelHouse.ServiceManagement.Mapping
{
    public class ResourceToModelProfile : AutoMapper.Profile
    {
        public ResourceToModelProfile() {
            CreateMap<CreateRequestResource, Request>();
            CreateMap<GetRequestResource, Request>();
            
            CreateMap<CreateFavoriteResource, Favorite>();
            CreateMap<GetFavoriteResource, Favorite>();
            
            CreateMap<CreateNotificationResource, Notification>();
            CreateMap<GetNotificationResource, Notification>();

            CreateMap<CreateOrderResource, Order>();
            CreateMap<GetOrderResource, Order>();
        }
    }
}