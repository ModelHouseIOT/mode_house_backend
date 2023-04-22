namespace ModelHouse.ServiceManagement.Resource.OrderResource
{
    public class GetOrderResource
    {
        public string Description { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public DateTime OrderDate { get; set; }
        public long UserId { get; set; }
        public long BusinessProfileId { get; set; }
    }
}