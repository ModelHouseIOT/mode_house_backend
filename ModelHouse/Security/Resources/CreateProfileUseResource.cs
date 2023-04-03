using System.ComponentModel.DataAnnotations;

namespace ModelHouse.Security.Resources
{
    public class CreateProfileUseResource
    {
        [Required]
        public string Name { get; set; }
        public string NameBusiness { get; set; }
        public string LocationBusiness { get; set; }
        public string Image { get; set; }
        public string ImageBusiness { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public long UserId { get; set; }
    }
}
