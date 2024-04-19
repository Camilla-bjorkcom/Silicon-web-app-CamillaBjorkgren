using Infrastructure.Factories;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API_Camilla.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController(CategoriesService categoriesService) : ControllerBase
{
    private readonly CategoriesService _categoriesService = categoriesService;


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoriesService.GetAllAsync();
        if (categories != null)
        {
            return Ok(CategoryFactory.Create(categories));
        }
        return NotFound();
    }
}
