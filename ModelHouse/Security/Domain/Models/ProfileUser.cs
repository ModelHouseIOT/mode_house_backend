namespace ModelHouse.Security.Domain.Models;
public class ProfileUser
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string NameBusiness { get; set; }
    public string LocationBusiness { get; set; }
    public string Image { get;set; }
    public string ImageBusiness { get; set; }
    public string PhoneNumber { get; set; }
    public long UserId { get; set; }

    public User User { get; set; }
}
