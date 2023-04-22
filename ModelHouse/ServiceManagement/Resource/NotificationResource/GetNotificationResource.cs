using ModelHouse.Security.Resources;
using ModelHouse.Security.Resources.AccountResource;

namespace ModelHouse.ServiceManagement.Resource.NotificationResource
{
    public class GetNotificationResource
    {
        public long Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public DateTime StartDate { get; set; }
        public long AccountId { get; set; }
        
        public AccountResource Account { get; set; }
    }
}