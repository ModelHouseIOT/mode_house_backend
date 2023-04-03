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

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var user = await _userRepository.FindByEmailAsync(request.EmailAddress);
        Console.WriteLine($"Request: {request.EmailAddress}, {request.Password}");
        Console.WriteLine($"User: {user.Id},{user.EmailAddress}, {user.PasswordHash}");
        
        // Validate 
        if (user == null || !BCryptNet.Verify(request.Password, user.PasswordHash))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Username or password is incorrect");
        }
        
        Console.WriteLine("Authentication successful. About to generate token");
        // Authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        Console.WriteLine($"Response: {response.Id}, {response.EmailAddress}");
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"Generated token is {response.Token}");
        return response;
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        Console.WriteLine(user.ProfileUser);
        Console.WriteLine("Hola hola hola hola");
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _userRepository.FindByEmailAsync(email);
    }


    public async Task RegisterAsync(RegisterRequest request)
    {
        // Validate if Username is already taken
        if (_userRepository.ExistsByEmail(request.EmailAddress))
            throw new AppException($"Email is already taken");
        
        // Map Request to User Object
        var user = _mapper.Map<User>(request);
        
        // Hash password
        user.PasswordHash = BCryptNet.HashPassword(request.Password);
        
        // Save User
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An Error occurred while saving the user: {e.Message}");
        }
    }

    public async Task<User> UpdateAsync(int id, UpdateRequest request, byte[] file, string contentType,string extension, string container)
    {
        var user = GetById(id);
        if(user == null)
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
            
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
            return user;
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }
    }
    
    public async Task DeleteAsync(int id)
    {
        var user = GetById(id);
        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }
    
    // Helper Methods
    private User GetById(int id)
    {
        var user = _userRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}