using System.Text.Json.Serialization;
using ModelHouse.ServiceManagement.Domain.Models;

namespace ModelHouse.Security.Domain.Models
{
    public class Account
    {
        public long Id { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public long UserId { get; set; }
        public long BusinessProfileId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime LastLogin { get; set; }
        public string Role { get; set; }


        [JsonIgnore]
        public string PasswordHash { get; set; }

        public User User { get; set; }
        public BusinessProfile BusinessProfile { get; set; }

        public IList<Notification> Notifications { get; set; } = new List<Notification>();
        public IList<Favorite> Favorites { get; set; } = new List<Favorite>();

    }
}