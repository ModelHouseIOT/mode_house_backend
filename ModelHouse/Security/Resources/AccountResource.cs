namespace ModelHouse.Security.Resources;

public class AccountResource
{
    public int Id { get; set; }
    public string EmailAddress { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateCreate { get; set; }
    public DateTime LastLogin { get; set; }
    public string Role { get; set; }
}