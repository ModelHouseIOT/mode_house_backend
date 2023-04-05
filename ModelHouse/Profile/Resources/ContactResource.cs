using ModelHouse.Security.Resources;

namespace ModelHouse.Profile.Resources;

public class ContactResource
{
    public long Id {get; set; }
    public AccountResource User { get; set; }
    public long UserId { get; set; }
    //public UserResource UserContact { get; set; }
    public long ContactId { get; set; }
}