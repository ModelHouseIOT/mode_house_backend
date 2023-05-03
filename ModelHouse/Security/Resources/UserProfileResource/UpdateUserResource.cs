namespace ModelHouse.Security.Resources.UserProfileResource
{
    public class UpdateUserResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Image { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
    }
}