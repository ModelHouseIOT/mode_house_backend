using AutoMapper;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Security.Domain.Services;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Persistence.Repositories;
using ModelHouse.Shared.Domain.Repositories;

namespace ModelHouse.Security.Services
{
    public class ProfileUserService : IProfileUserService
    {
        private readonly IProfileUserRepository _profileUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileUserService(IUserRepository _userRepository, IUnitOfWork _unitOfWork, IProfileUserRepository profileUserRepository)
        {
            this._profileUserRepository = profileUserRepository;
            this._unitOfWork = _unitOfWork;
            this._userRepository = _userRepository;
        }

        public async Task<ProfileResponse> CreateProfile(ProfileUser profile)
        {
            var existingUser = await _userRepository.FindByIdAsync(profile.UserId);
            if (existingUser == null)
                return new ProfileResponse("Invalid user");
            if (existingUser.ProfileUser != null)
                return new ProfileResponse("The user already has a profile");
            try
            {
                await _profileUserRepository.CreateProfile(profile);
                await _unitOfWork.CompleteAsync();

                return new ProfileResponse(profile);
            }
            catch (Exception e)
            {
                return new ProfileResponse($"An error occurred while saving the area: {e.Message}");
            }
        }

        public async Task<IEnumerable<ProfileUser>> GetAllProfile()
        {
            return await _profileUserRepository.GetAllProfile();
        } 

        public async Task<ProfileResponse> GetProfileByUserId(long id)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);
            if (existingUser == null)
                return new ProfileResponse("Invalid user");

            return new ProfileResponse(await _profileUserRepository.GetProfileById(id));
        }

        public async Task<ProfileResponse> UpdateProfile(ProfileUser profile)
        {
            var existingArea = await _profileUserRepository.GetProfileById(profile.Id);
            if (existingArea == null)
                return new ProfileResponse("Area not found");

            try
            {
                _profileUserRepository.UpdateProfile(existingArea);
                await _unitOfWork.CompleteAsync();

                return new ProfileResponse(existingArea);
            }
            catch (Exception e)
            {
                return new ProfileResponse($"An error occurred while saving the area: {e.Message}");
            }
        }
    }
}
