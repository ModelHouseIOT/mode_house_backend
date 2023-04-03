using System.ComponentModel.DataAnnotations;

namespace ModelHouse.Profile.Resources;
public class SaveOrderResource
{
    [Required]
    [MaxLength(30)]
    public string Title { get; set; }
    
    [Required]
    [MaxLength(120)]
    public string Description { get; set; }
    
    [Required]
    public long UserId { get; set; }
    
    [Required]
    public long SendUserId { get; set; }
    
    [Required]
    public bool active { get; set; }

    [Required]
    public long PostId { get; set; }
}