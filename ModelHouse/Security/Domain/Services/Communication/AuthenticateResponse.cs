namespace ModelHouse.Security.Domain.Services.Communication;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string EmailAddress { get; set; }
    public string Token { get; set; }
}