using ModelHouse.Security.Domain.Models;

namespace ModelHouse.ServiceManagement.Domain.Models
{
    public class Order
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public long UserId { get; set; }
        public long BusinessProfileId { get; set; }

        public User User { get; set; }
    }
}