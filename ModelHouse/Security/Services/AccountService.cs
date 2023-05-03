using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ModelHouse.Security.Authorization.Handlers.Interfaces;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Security.Domain.Services;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Exceptions;
using ModelHouse.Security.Resources.AccountResource;
using ModelHouse.Security.Resources.UserProfileResource;
using ModelHouse.Shared.Domain.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;
namespace ModelHouse.Security.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var account = await _accountRepository.FindByEmailAsync(request.EmailAddress);
        
            // Validate 
            if (account == null || !BCryptNet.Verify(request.Password, account.PasswordHash))
            {
                Console.WriteLine("Authentication Error");
                throw new AppException("Username or password is incorrect");
            }
        
            Console.WriteLine("Authentication successful. About to generate token");
            // Authentication successful
            var response = _mapper.Map<AuthenticateResponse>(account);
            Console.WriteLine($"Response: {response.Id}, {response.EmailAddress}");
            response.Token = _jwtHandler.GenerateToken(account);
            Console.WriteLine($"Generated token is {response.Token}");
            return response;
        }

        public async Task<IEnumerable<Account>> ListAsync()
        {
            return await _accountRepository.ListAsync();
        }

        public async Task<IEnumerable<Account>> ListBusinessAsync()
        {
            return await _accountRepository.ListBusinessAsync();
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            var account = await _accountRepository.FindByIdAsync(id);
            Console.WriteLine(account.User);
            Console.WriteLine("Hola hola hola hola");
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }

        public async Task<Account> GetByEmailAsync(string email)
        {
            return await _accountRepository.FindByEmailAsync(email);
        }


        public async Task RegisterAsync(RegisterRequest request)
        {
            if (_accountRepository.ExistsByEmail(request.EmailAddress))
                throw new AppException($"Email is already taken");
        
            var account = _mapper.Map<Account>(request);

            account.PasswordHash = BCryptNet.HashPassword(request.Password);
            account.IsActive = true;
            account.Role = "User";
            account.DateCreate = DateTime.Now;
            account.LastLogin = DateTime.Now;
            account.BusinessProfileId = 0;
            account.UserId = 0;

            CreateUseResource user = null;
            try
            {
                await _accountRepository.AddAsync(account);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                throw new AppException($"An Error occurred while saving the Account: {account.EmailAddress}");
            }
        }

        public async Task<Account> UpdateAsync(int id, UpdateRequest request)
        {
            var account = GetById(id);
            if(account == null)
                return null;
            account.EmailAddress = request.EmailAddress;
            account.LastLogin = DateTime.Now;
            try
            {
                _accountRepository.Update(account);
                await _unitOfWork.CompleteAsync();
                return account;
            }
            catch (Exception e)
            {
                throw new AppException($"An error occurred while updating the Account: {e.Message}");
            }
        }

        public async Task<Account> UpdateRoleAsync(long id, ChangeRole request)
        {
            var account = GetById(id);
            if(account == null)
                return null;
            account.Role = request.Role;
            account.LastLogin = DateTime.Now;
            try
            {
                _accountRepository.Update(account);
                await _unitOfWork.CompleteAsync();
                return account;
            }
            catch (Exception e)
            {
                throw new AppException($"An error occurred while updating the Account: {e.Message}");
            }
        }

        public async Task<Account> UpdateIsActiveAsync(long id, ChangeIsActive request)
        {
            var account = GetById(id);
            if(account == null)
                return null;
            account.IsActive = request.IsActive;
            account.LastLogin = DateTime.Now;
            try
            {
                _accountRepository.Update(account);
                await _unitOfWork.CompleteAsync();
                return account;
            }
            catch (Exception e)
            {
                throw new AppException($"An error occurred while updating the Account: {e.Message}");
            }
        }

        public async Task<Account> UpdateUserProfileIdAsync(long id, long UserId)
        {
            var account = GetById(id);
            if(account == null)
                return null;
            account.UserId = UserId;
            account.LastLogin = DateTime.Now;
            try
            {
                _accountRepository.Update(account);
                await _unitOfWork.CompleteAsync();
                return account;
            }
            catch (Exception e)
            {
                throw new AppException($"An error occurred while updating the Account: {e.Message}");
            }
        }

        public async Task<Account> UpdateBusinessProfileIdAsync(long id, long BusinessProfileId)
        {
            var account = GetById(id);
            if(account == null)
                return null;
            account.BusinessProfileId = BusinessProfileId;
            account.LastLogin = DateTime.Now;
            try
            {
                _accountRepository.Update(account);
                await _unitOfWork.CompleteAsync();
                return account;
            }
            catch (Exception e)
            {
                throw new AppException($"An error occurred while updating the Account: {e.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var account = GetById(id);
            try
            {
                _accountRepository.Remove(account);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                throw new AppException($"An error occurred while deleting the Account: {e.Message}");
            }
        }
    
        // Helper Methods
        private Account GetById(long id)
        {
            var account = _accountRepository.FindById(id);
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }
    }
}