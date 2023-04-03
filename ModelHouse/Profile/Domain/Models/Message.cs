namespace ModelHouse.Profile.Domain.Models;

public class Message
{
    public long Id { get; set; }
    public string Content { get; set; }
    public DateTime ShippingTime { get; set; }
    public bool isMe { get; set; }
    
    public Contact Contact { get; set; }
    public long UserId { get; set; }
    public long ContactId { get; set; }

}