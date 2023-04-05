using ModelHouse.Security.Domain.Models;

namespace ModelHouse.Profile.Domain.Models;

public class Contact
{
    public long Id {get; set; }
    public long ContactId { get; set; }
    public Account Account { get; set; }
    public long UserId { get; set; }
    public IList<Message> Messages { get; set; } = new List<Message>();

}