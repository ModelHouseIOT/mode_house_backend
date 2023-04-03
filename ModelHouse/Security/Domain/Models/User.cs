using System.Text.Json.Serialization;
using ModelHouse.Profile.Domain.Models;

namespace ModelHouse.Security.Domain.Models;

public class User
{
    public long Id { get; set; }
    public string EmailAddress { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    public ProfileUser ProfileUser { get; set; }
    public IList<Order> Orders { get; set; } = new List<Order>();
    public IList<Post> Posts { get; set; } = new List<Post>();

    public IList<Notification> Notifications { get; set; } = new List<Notification>();


    public IList<Contact> Contacts { get; set; } = new List<Contact>();

}