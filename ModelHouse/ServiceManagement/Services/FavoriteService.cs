using ModelHouse.Security.Domain.Repositories;
using ModelHouse.ServiceManagement.Domain.Models;
using ModelHouse.ServiceManagement.Domain.Repositories;
using ModelHouse.ServiceManagement.Domain.Services;
using ModelHouse.ServiceManagement.Domain.Services.Comunication;
using ModelHouse.Shared.Domain.Repositories;

namespace ModelHouse.ServiceManagement.Services
{
    public class FavoriteService: IFavoriteService
    {
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;


        public FavoriteService(IFavoritesRepository favoritesRepository, IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _favoritesRepository = favoritesRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Favorite>> ListAsync()
        {
            return await _favoritesRepository.ListAsync();
        }

        public async Task<IEnumerable<Favorite>> ListByUserId(long id)
        {
            return await _favoritesRepository.ListByAccountId(id);
        }

        public async Task<FavoriteResponse> CreateAsync(Favorite favorite)
        {
            var user = await _accountRepository.FindByIdAsync(favorite.AccountId);
            if (user == null)
                return new FavoriteResponse("Account is not exist");
            favorite.CreationDate = DateTime.Now;
            try
            {
                await _favoritesRepository.CreateFavorite(favorite);
                await _unitOfWork.CompleteAsync();
                return new FavoriteResponse(favorite);
            }
            catch  (Exception e)
            {
                return new FavoriteResponse($"Failed to register a Favorite: {e.Message}");
            }
        }

        public async Task<FavoriteResponse> DeleteAsync(long id)
        {
            var favorite = await _favoritesRepository.GetFavoriteById(id);
            if (favorite == null)
                return new FavoriteResponse("Favorite is not exist");
        
            try
            {
                _favoritesRepository.DeleteFavorite(favorite);
                await _unitOfWork.CompleteAsync();
                return new FavoriteResponse(favorite);
            }
            catch (Exception e)
            {
                return new FavoriteResponse($"An error occurred while deleting the Favorite: {e.Message}");
            }
        }

        public Task<FavoriteResponse> UpdateAsync(long id, Order favorite)
        {
            throw new NotImplementedException();
        }
    }
}