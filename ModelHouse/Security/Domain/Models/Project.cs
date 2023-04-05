namespace ModelHouse.Security.Domain.Models
{
    public class Project
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public long BusinessProfileId { get; set; }

        public BusinessProfile BusinessProfile { get; set; }
    }
}
