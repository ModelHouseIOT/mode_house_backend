using System.ComponentModel.DataAnnotations;

namespace ModelHouse.Profile.Resources;

public class SaveMessageResource
{
    [Required]
    public string Content { get; set; }
    
    [Required]
    public DateTime ShippingTime { get; set; }
    
    [Required]
    public bool isMe { get; set; }
    
    [Required]
    public long UserId { get; set; }
    
    [Required]
    public long ContactId { get; set; }
}