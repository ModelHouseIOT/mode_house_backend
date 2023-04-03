using System.ComponentModel.DataAnnotations;

namespace ModelHouse.Profile.Resources;

public class SaveContactResource
{
    [Required]
    public long UserId { get; set; }
    [Required]
    public long ContactId { get; set; }
}