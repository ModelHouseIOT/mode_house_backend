using ModelHouse.Security.Domain.Models;

namespace ModelHouse.ServiceManagement.Domain.Models
{
    public class Notification
    {
        public long Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public DateTime StartDate { get; set; }
        public long AccountId { get; set; }
        
        public Account Account { get; set; }
    }
}