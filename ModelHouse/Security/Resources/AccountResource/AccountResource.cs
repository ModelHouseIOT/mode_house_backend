namespace ModelHouse.Security.Resources.AccountResource
{
    public class AccountResource
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public long UserId { get; set; }
        public long BusinessProfileId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime LastLogin { get; set; }
        public string Role { get; set; }
    }
}