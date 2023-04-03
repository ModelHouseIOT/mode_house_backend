namespace ModelHouse.Security.Resources
{
    public class GetProfileUserResource
    {
        public string Name { get; set; }
        public string NameBusiness { get; set; }
        public string LocationBusiness { get; set; }
        public string Image { get; set; }
        public string ImageBusiness { get; set; }
        public string PhoneNumber { get; set; }
        public long UserId { get; set; }
        public UserResource User { get; set; }
    }
}
