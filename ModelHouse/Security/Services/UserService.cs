using AutoMapper;
using Google.Protobuf;
using Microsoft.AspNetCore.Hosting;
using ModelHouse.Security.Authorization.Handlers.Interfaces;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Security.Domain.Services;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Persistence.Repositories;
using ModelHouse.Security.Resources;
using ModelHouse.Shared.Domain.Repositories;
using System.ComponentModel;

namespace ModelHouse.Security.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtHandler _jwtHandler;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment webHostEnvironment,
            IJwtHandler _jwtHandler,
            IAccountRepository accountRepository, 
            IUnitOfWork _unitOfWork, IUserRepository userRepository)
        {
            this._userRepository = userRepository;
            this._unitOfWork = _unitOfWork;
            this._accountRepository = accountRepository;
            this._jwtHandler = _jwtHandler;
            this._webHostEnvironment = webHostEnvironment;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserResponse> CreateUser(User profile)
        {
            var existingAccount = await _accountRepository.FindByIdAsync(profile.AccountId);
            if (existingAccount == null)
                return new UserResponse("Invalid user");
            if (existingAccount.User != null)
                return new UserResponse("The user already has a profile");
            try
            {
                await _userRepository.CreateUser(profile);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(profile);
            }
            catch (Exception e)
            {
                return new UserResponse($"An error occurred while saving the area: {e.Message}");
            }
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _userRepository.GetAllUser();
        } 

        public async Task<UserResponse> GetUserByUserId(long id)
        {
            var existingUser = await _accountRepository.FindByIdAsync(id);
            if (existingUser == null)
                return new UserResponse("Invalid user");

            return new (await _userRepository.GetUserById(id));
        }

        public async Task<UserResponse> UpdateUser(long id, UpdateUserResource user, byte[] file, string contentType, string extension, string container)
        {
            var existingUser = await _userRepository.GetUserById(id);
            if (existingUser == null)
                return new UserResponse("User not found");
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.RegistrationDate = user.RegistrationDate;
            existingUser.LastLogin = user.LastLogin;
            existingUser.Gender = user.Gender;
            existingUser.AccountStatus = user.AccountStatus;
            try
            {
                string wwwrootPath = _webHostEnvironment.WebRootPath;
                if (string.IsNullOrEmpty(wwwrootPath))
                    throw new Exception();
                string carpetaArchivo = Path.Combine(wwwrootPath, container);
                if (!Directory.Exists(carpetaArchivo))
                    Directory.CreateDirectory(carpetaArchivo);
                string nombreFinal = $"{Guid.NewGuid()}{extension}";
                //System.Console.WriteLine(file);
                string rutaFinal = Path.Combine(carpetaArchivo, nombreFinal);
                System.Console.WriteLine(rutaFinal);
                File.WriteAllBytesAsync(rutaFinal, file);
                string urlActual =
                    $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
                string dbUrl = Path.Combine(urlActual, container, nombreFinal).Replace("\\", "/");

                existingUser.Image = dbUrl;

                _userRepository.UpdateUser(existingUser);
                await _unitOfWork.CompleteAsync();
                return new UserResponse(existingUser);
            }
            catch (Exception e)
            {
                return new UserResponse($"An error occurred while saving the area: {e.Message}");
            }
        }
    }
}
