using System.ComponentModel.DataAnnotations;

namespace ModelHouse.Security.Resources
{
    public class CreateUseResource
    {
        [Required]
        public string FirstName { get; set; }
        public string Image { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string RegistrationDate { get; set; }
        public string LastLogin { get; set; }
        public long AccountStatus { get; set; }
        [Required]
        public long AccountId { get; set; }
    }
}
