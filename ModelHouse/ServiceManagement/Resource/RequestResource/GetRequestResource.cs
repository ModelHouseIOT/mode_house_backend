namespace ModelHouse.ServiceManagement.Resource.RequestResource
{
    public class GetRequestResource
    {
        public string Description { get; set; }
        public string Category { get; set; }
        public string Precio { get; set; }
        public int EstimatedTime { get; set; }
        public DateTime RequestDate { get; set; }
        public string IsResponse { get; set; }
        public string ResponseDate { get; set; }
        public long BusinessProfileId { get; set; }
        public long UserId { get; set; }
    }
}