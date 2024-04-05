using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Web_API_Camilla.Filters;
using static System.Net.WebRequestMethods;

namespace Web_API_Camilla.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscribersController(SubscriberService subscriberService) : ControllerBase
{
    private readonly SubscriberService _subscriberService = subscriberService;

    [HttpPost]
    [UseApiKey]
    public async Task<IActionResult> CreateSubscriberAsync(SubscribeDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (!await _subscriberService.ExistsSubscriberAsync(dto.Email))
                {
                    SubscriberEntity entity = dto;
                    var result = await _subscriberService.AddSubscriberAsync(entity);
                    if (result)
                    {
                        return Created("", null);
                    }
                    return Problem("Unable to create subscription.");
                }
                return Conflict("Your email address is already subscribed");
            }
            return BadRequest();
        }
        catch
        {
            return BadRequest();
        }
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            if (ModelState.IsValid)
            {   
                    var subscribers = await _subscriberService.GetAllAsync();
                    if (subscribers != null)
                    {
                        return Ok(subscribers);
                    }
                    return NotFound();              
            }
            return BadRequest();
        }
        catch { return BadRequest(); }
    }

    [Authorize]
    [HttpGet("{email}")]
    public async Task<IActionResult> GetOneAsync(string email)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var subscriber = await _subscriberService.GetOneAsyncEmail(email);
                if (subscriber != null)
                {
                    return Ok(subscriber);
                }
                return NotFound();
            }
            return BadRequest();
        }
        catch { return BadRequest(); }
    }

    [UseApiKey]
    [HttpDelete("{email}")]
    public async Task<IActionResult> DeleteAsync(string email)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var subscriber = await _subscriberService.GetOneAsyncEmail(email);
                if (subscriber != null)
                {
                    var result = await _subscriberService.DeleteSubscriberAsync(subscriber);
                    if (result)
                        return Ok();
                    else return BadRequest();
                }
                return NotFound();
            }
            return BadRequest();
        }
        catch { return BadRequest(); }
    }
}
