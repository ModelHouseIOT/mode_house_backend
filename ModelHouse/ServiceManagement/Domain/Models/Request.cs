using ModelHouse.Security.Domain.Models;

namespace ModelHouse.ServiceManagement.Domain.Models
{
    public class Request
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Precio { get; set; }
        public int EstimatedTime { get; set; }
        public DateTime RequestDate { get; set; }
        public long BusinessProfileId { get; set; }
        public long UserId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }

    }
}