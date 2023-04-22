using ModelHouse.Security.Resources.AccountResource;

namespace ModelHouse.ServiceManagement.Resource.FavoriteResource
{
    public class GetFavoriteResource
    {
    
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool View { get; set; }
        public long AccountId { get; set; }

        public AccountResource Account { get; set; }
    }
}