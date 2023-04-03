using ModelHouse.Security.Domain.Models;

namespace ModelHouse.Profile.Domain.Models;

public class Post
{
    public long Id { get; set; }
    public double Price { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public string Foto { get; set; }
    
    public User User { get; set; }
    public long UserId { get; set; }
    public IList<Order> Orders { get; set; } = new List<Order>();

}