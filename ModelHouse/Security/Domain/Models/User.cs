using ModelHouse.ServiceManagement.Domain.Models;

namespace ModelHouse.Security.Domain.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Image { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLogin { get; set; }
        public bool AccountStatus { get; set; }
        public long AccountId { get; set; }

        public Account Account { get; set; }

        public IList<Order> Orders { get; set; } = new List<Order>();

    }
}
