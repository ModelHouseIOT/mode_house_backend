using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Repositories;
using ModelHouse.Profile.Domain.Services;
using ModelHouse.Profile.Domain.Services.Communication;
using ModelHouse.Security.Domain.Repositories;
using ModelHouse.Shared.Domain.Repositories;

namespace ModelHouse.Profile.Services;

public class ContactService: IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ContactService(IContactRepository contactRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _contactRepository = contactRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<Contact>> ListAsync()
    {
        return _contactRepository.ListAsync();
    }

    public Task<IEnumerable<Contact>> ListByUserId(long id)
    {
        return _contactRepository.ListByUserId(id);
    }

    public async Task<ContactResponse> CreateAsync(Contact contact)
    {
        var user_exist = await _userRepository.FindByIdAsync(contact.UserId);
        if (user_exist == null)
            return new ContactResponse("The User is not exist");
        var user_contact_exist = await _userRepository.FindByIdAsync(contact.ContactId);
        if (user_contact_exist == null)
            return new ContactResponse("The User Contact is not exist");
        if (contact.UserId == contact.ContactId)
            return new ContactResponse("The action is not correct");
        var contact_exist = await _contactRepository.ListByUserId(contact.UserId);
        foreach (var c in contact_exist)
        {
            if (c.ContactId == contact.ContactId)
                return new ContactResponse("The Contact is exist");
        }
        try
        {
            await _contactRepository.AddAsync(contact);
            await _unitOfWork.CompleteAsync();
            return new ContactResponse(contact);
        }
        catch (Exception e)
        {
            return new ContactResponse($"Failed to register a Project: {e.Message}");
        }
    }

    public async Task<ContactResponse> DeleteAsync(long id)
    {
        var contact_exist = await _contactRepository.FindByIdAsync(id);
        if (contact_exist == null)
            return new ContactResponse("the Contact is not existing");
        try
        {
            _contactRepository.DeleteAsync(contact_exist);
            await _unitOfWork.CompleteAsync();
            return new ContactResponse(contact_exist);
        }
        catch (Exception e)
        {
            return new ContactResponse($"An error occurred while deleting the Contact: {e.Message}");
        }
    }

    public async Task<ContactResponse> UpdateAsync(long id, Contact order)
    {
        var contact_exist = await _contactRepository.FindByIdAsync(id);
        if (contact_exist == null)
            return new ContactResponse("the Contact is not existing");
        try
        {
            _contactRepository.UpdateAsync(contact_exist);
            await _unitOfWork.CompleteAsync();
            return new ContactResponse(contact_exist);
        }
        catch (Exception e)
        {
            return new ContactResponse($"An error occurred while deleting the Contact: {e.Message}");
        }
    }

    public async Task<ContactResponse> GetOrderById(long id)
    {
        try
        {
            var account = await _contactRepository.FindByIdAsync(id);
            return new ContactResponse(account);
        }
        catch (Exception e)
        {
            return new ContactResponse($"Failed to find a current user Contact: {e.Message}");
        }
    }
}