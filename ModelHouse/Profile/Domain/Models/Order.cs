
using ModelHouse.Security.Domain.Models;

namespace ModelHouse.Profile.Domain.Models;

public class Order
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public Account Account { get; set; }
    public long AccountId { get; set; }
    public long SendUserId { get; set; }
    public bool active { get; set; }
    public Post Post { get; set; }
    public long PostId { get; set; }
}