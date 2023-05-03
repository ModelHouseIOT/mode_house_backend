using System.ComponentModel.DataAnnotations;

namespace ModelHouse.Security.Resources.BusinessProfileResource
{
    public class CreateBusinessProfileResource
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string WebSite { get; set; }
        [Required]
        public string PhoneBusiness { get; set; }
        [Required]
        public long AccountId { get; set; }
    }
}