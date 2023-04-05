using ModelHouse.Security.Resources;

namespace ModelHouse.Profile.Resources;

public class NotificationResource
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string active { get; set; }
    public DateTime ShippingTime { get; set; }

    public AccountResource User { get; set; }
    public long UserId { get; set; }
}