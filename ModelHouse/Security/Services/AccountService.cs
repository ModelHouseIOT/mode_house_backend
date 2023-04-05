using AutoMapper;
using ModelHouse.Security.Authorization.Handlers.Interfaces;
using ModelHouse.Security.Domain.Models;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Security.Domain.Services;
using ModelHouse.Security.Domain.Services.Communication;
using ModelHouse.Security.Exceptions;
using ModelHouse.Shared.Domain.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;
namespace ModelHouse.Security.Services;

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

    public async Task<Account> GetByIdAsync(int id)
    {
        var account = await _accountRepository.FindByIdAsync(id);
        Console.WriteLine(account.User);
        Console.WriteLine("Hola hola hola hola");
        if (account == null) throw new KeyNotFoundException("User not found");
        return account;
    }

    public async Task<Account> GetByEmailAsync(string email)
    {
        return await _accountRepository.FindByEmailAsync(email);
    }


    public async Task RegisterAsync(RegisterRequest request)
    {
        // Validate if Username is already taken
        if (_accountRepository.ExistsByEmail(request.EmailAddress))
            throw new AppException($"Email is already taken");
        
        // Map Request to User Object
        var account = _mapper.Map<Account>(request);

        // Hash password
        account.PasswordHash = BCryptNet.HashPassword(request.Password);
        account.Role = "user";
        // Save User
        try
        {
            await _accountRepository.AddAsync(account);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An Error occurred while saving the user: {account.EmailAddress}");
        }
    }

    public async Task<Account> UpdateAsync(int id, UpdateRequest request, byte[] file, string contentType,string extension, string container)
    {
        var account = GetById(id);
        if(account == null)
            return null;
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

            //user.Image = dbUrl;

            _accountRepository.Update(account);
            await _unitOfWork.CompleteAsync();
            return account;
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
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
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }
    
    // Helper Methods
    private Account GetById(int id)
    {
        var account = _accountRepository.FindById(id);
        if (account == null) throw new KeyNotFoundException("User not found");
        return account;
    }
}