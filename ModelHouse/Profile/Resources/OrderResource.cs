using ModelHouse.Security.Resources;

namespace ModelHouse.Profile.Resources;

public class OrderResource
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public AccountResource User { get; set; }
    public long UserId { get; set; }
    
    public long SendUserId { get; set; } 
    public bool active { get; set; }

    public PostResource Post { get; set; }
    public long PostId { get; set; }

}