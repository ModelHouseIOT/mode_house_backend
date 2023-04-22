using System.ComponentModel.DataAnnotations;

namespace ModelHouse.Security.Resources.UserProfileResource
{
    public class CreateUseResource
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public long AccountId { get; set; }
    }
}