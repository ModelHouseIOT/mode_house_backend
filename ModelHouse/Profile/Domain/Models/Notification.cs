using ModelHouse.Security.Domain.Models;

namespace ModelHouse.Profile.Domain.Models;

public class Notification
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool active { get; set; }
    public DateTime ShippingTime { get; set; }
    
    public Account Account { get; set; }
    public long AccountId { get; set; }
}