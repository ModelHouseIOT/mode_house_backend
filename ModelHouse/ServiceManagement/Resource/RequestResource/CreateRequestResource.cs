using System.ComponentModel.DataAnnotations;

namespace ModelHouse.ServiceManagement.Resource.RequestResource
{
    public class CreateRequestResource
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Precio { get; set; }
        [Required]
        public int EstimatedTime { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long BusinessProfileId { get; set; }
    }
}