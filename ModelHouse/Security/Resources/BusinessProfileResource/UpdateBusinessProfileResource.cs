namespace ModelHouse.Security.Resources.BusinessProfileResource
{
    public class UpdateBusinessProfileResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string Address { get; set; }
        public string WebSite { get; set; }
        public string PhoneBusiness { get; set; }
        public long AccountId { get; set; }
    }
}