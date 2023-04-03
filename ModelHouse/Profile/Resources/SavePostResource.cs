using System.ComponentModel.DataAnnotations;

namespace ModelHouse.Profile.Resources;

public class SavePostResource
{
    [Required]
    public double Price { get; set; }
    [Required]
    [MaxLength(50)]
    public string Title { get; set; }
    [Required]
    [MaxLength(100)]
    public string Category { get; set; }
    [Required]
    [MaxLength(100)]
    public string Location { get; set; }
    [Required]
    [MaxLength(200)]
    public string Description { get; set; }
    [Required]
    public long UserId { get; set; }
    public IFormFile Foto { get; set; }
}