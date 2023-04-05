using ModelHouse.Security.Authorization.Handlers.Interfaces;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Security.Domain.Services;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Persistence.Repositories;
using ModelHouse.Security.Resources;
using ModelHouse.Shared.Domain.Repositories;

namespace ModelHouse.Security.Services
{
    public class BusinessProfileService : IBusinessProfileService
    {
        private readonly IBusinessProfileRepository _businessProfileRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtHandler _jwtHandler;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BusinessProfileService(IBusinessProfileRepository businessProfileRepository, 
            IAccountRepository accountRepository, 
            IUnitOfWork unitOfWork, 
            IJwtHandler jwtHandler, 
            IWebHostEnvironment webHostEnvironment, 
            IHttpContextAccessor httpContextAccessor)
        {
            _businessProfileRepository = businessProfileRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _jwtHandler = jwtHandler;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BusinessProfileResponse> CreateBusinessProfile(BusinessProfile profile)
        {
            var existingAccount = await _accountRepository.FindByIdAsync(profile.AccountId);
            if (existingAccount == null)
                return new BusinessProfileResponse("Invalid Account");
            if(existingAccount.Role != "Business") { 
                return new BusinessProfileResponse("You do not have access to the company profile.");
            }
            if (existingAccount.BusinessProfile != null)
                return new BusinessProfileResponse("The user already has a profile");
            try
            {
                await _businessProfileRepository.CreateBusinessProfile(profile);
                await _unitOfWork.CompleteAsync();

                return new BusinessProfileResponse(profile);
            }
            catch (Exception e)
            {
                return new BusinessProfileResponse($"An error occurred while saving the area: {e.Message}");
            }
        }

        public async Task<IEnumerable<BusinessProfile>> GetAllBusinessProfile()
        {
            return await _businessProfileRepository.GetAllBusinessProfile();
        }

        public async Task<BusinessProfileResponse> GetBusinessProfileByAccountId(long id)
        {
            var businessProfile = await _accountRepository.FindByIdAsync(id);
            if (businessProfile == null)
                return new BusinessProfileResponse("Invalid user");

            return new(await _businessProfileRepository.GetBusinessProfileByAccountId(id));
        }

        public async Task<BusinessProfileResponse> UpdateBusinessProfile(long id, UpdateBusinessProfileResource profile, byte[] file, string contentType, string extension, string container)
        {
            var businesProfile = await _businessProfileRepository.GetBusinessProfileById(id);
            if (businesProfile == null)
                return new BusinessProfileResponse("User not found");
            businesProfile.Name = profile.Name;
            businesProfile.Description = profile.Description;
            businesProfile.Address = profile.Address;
            businesProfile.WebSite = profile.WebSite;
            businesProfile.PhoneBusiness = profile.PhoneBusiness;
            businesProfile.FoundationDate = profile.FoundationDate;
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

                businesProfile.Image = dbUrl;

                _businessProfileRepository.UpdateUser(businesProfile);
                await _unitOfWork.CompleteAsync();
                return new BusinessProfileResponse(businesProfile);
            }
            catch (Exception e)
            {
                return new BusinessProfileResponse($"An error occurred while saving the area: {e.Message}");
            }
        }
    }
}
