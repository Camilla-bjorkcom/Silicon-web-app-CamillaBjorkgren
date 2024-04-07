using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API_Camilla.Filters;

namespace Web_API_Camilla.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ContactController(ContactMessageService contactMessageService) : ControllerBase
{
    private readonly ContactMessageService _contactMessageService = contactMessageService;

    [UseApiKey]
    [HttpPost]
    public async Task<IActionResult> Create(ContactFormModel model)
    {
        if (ModelState.IsValid)
        {
                ContactMessageEntity contactEntity = model;
                var result = await _contactMessageService.CreateAsync(contactEntity);
                if (result)
                {
                    return Created("", null);
                }
               
            }
        return BadRequest();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (ModelState.IsValid)
        {
            var result = await _contactMessageService.GetAllAsync();
            if (result != null)
            {
                return Ok(result);
            }
        }
        return NotFound();
    }


    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        if (ModelState.IsValid)
        {
            var result = await _contactMessageService.GetOneAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
        }
        return NotFound();
    }

}
