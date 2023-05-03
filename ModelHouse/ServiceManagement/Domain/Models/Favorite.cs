using ModelHouse.Security.Domain.Models;

namespace ModelHouse.ServiceManagement.Domain.Models
{
    public class Favorite
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool View { get; set; }
        public long AccountId { get; set; }

        public Account Account { get; set; }
    }
}