namespace ModelHouse.Security.Domain.Models
{
    public class BusinessProfile
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string WebSite { get; set; }
        public string PhoneBusiness { get; set; }
        public DateTime FoundationDate { get; set; }
        public long AccountId { get; set; }
        public Account Account { get; set; }

        public List<Project> Projects { get; set; }
    }
}
