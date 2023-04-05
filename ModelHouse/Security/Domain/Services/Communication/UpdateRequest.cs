namespace ModelHouse.Security.Domain.Services.Communication;

public class UpdateRequest
{
    public string EmailAddress { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateCreate { get; set; }
    public DateTime LastLogin { get; set; }
    public string Role { get; set; }
}