using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ModelHouse.Profile.Domain.Models;

namespace ModelHouse.Security.Domain.Models;

public class Account
{
    public long Id { get; set; }
    public string EmailAddress { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateCreate { get; set; }
    public DateTime LastLogin { get; set; }
    public string Role { get; set; }


    [JsonIgnore]
    public string PasswordHash { get; set; }

    public User User { get; set; }
    public BusinessProfile BusinessProfile { get; set; }

    public IList<Order> Orders { get; set; } = new List<Order>();
    public IList<Post> Posts { get; set; } = new List<Post>();
    public IList<Notification> Notifications { get; set; } = new List<Notification>();
    public IList<Contact> Contacts { get; set; } = new List<Contact>();

}