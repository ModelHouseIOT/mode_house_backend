namespace ModelHouse.Security.Resources
{
    public class UpdateUserResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Image { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string RegistrationDate { get; set; }
        public string LastLogin { get; set; }
        public long AccountStatus { get; set; }
    }
}
