using System.ComponentModel.DataAnnotations;

namespace ModelHouse.ServiceManagement.Resource.OrderResource
{
    public class CreateOrderResource
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required] 
        public long UserId { get; set; }
        [Required]
        public long BusinessProfileId { get; set; }


    }
}