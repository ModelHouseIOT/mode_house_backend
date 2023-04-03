using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelHouse.Profile.Domain.Models;
using ModelHouse.Profile.Domain.Repositories;
using ModelHouse.Profile.Domain.Services;
using ModelHouse.Profile.Resources;
using ModelHouse.Shared.Extensions;

namespace ModelHouse.Profile.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ContactController: ControllerBase
{
    private readonly IContactService _contactService;
    private readonly IMapper _mapper;

    public ContactController(IContactService contactService, IMapper mapper)
    {
        _contactService = contactService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ContactResource>> GetAllAsync()
    {
        var posts = await _contactService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactResource>>(posts);
        return resources;
    }
    
    [HttpGet("" +
             "user/{id}")]
    public async Task<IEnumerable<ContactResource>> GetAllByUserId(long id)
    {
        var contacts = await _contactService.ListByUserId(id);
        var resources = _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactResource>>(contacts);
        return resources;
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _contactService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var postResource = _mapper.Map<Contact, ContactResource>(result.Resource);

        return Ok(postResource);
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveContactResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var post = _mapper.Map<SaveContactResource, Contact>(resource);

        var result = await _contactService.CreateAsync(post);
        if (!result.Success)
            return BadRequest(result.Message);

        var postResource = _mapper.Map<Contact, ContactResource>(result.Resource);
        return Ok(postResource);
    }


    //[HttpGet("/api/v1/users")]
    //public async Task<string> Guardar()
    //{
    //    return "hola";
    //}
    //[HttpPost("GuardarImagen")]
    //public async Task<string> GuardarImagen([FromForm] SubirImagenAPI fichero)
    //{
    //    var ruta = string.Empty;
    //    if (fichero.Archivo.Length > 0)
    //    {
    //        var nombreArchivo = Guid.NewGuid().ToString() + ".jpg";
    //        ruta = $"Imagenes/{nombreArchivo}";
    //        using (var stream = new FileStream(ruta, FileMode.Create))
    //        {
    //            await fichero.Archivo.CopyToAsync(stream);
    //        }
    //    }
    //    return ruta;
    //}
}