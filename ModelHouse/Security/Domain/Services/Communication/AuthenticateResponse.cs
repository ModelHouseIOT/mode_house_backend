using ModelHouse.Security.Domain.Models;

namespace ModelHouse.Security.Domain.Services.Communication
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public long UserId { get; set; }
        public long BusinessProfileId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime LastLogin { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public BusinessProfile BusinessProfile { get; set; }
    }
}