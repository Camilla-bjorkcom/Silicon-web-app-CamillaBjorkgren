using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;
public class CategoriesService(CategoriesRepository categoriesRepository, HttpClient http, IConfiguration configuration)
{
    private readonly CategoriesRepository _categoriesRepository = categoriesRepository;
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEnumerable<CategoryEntity>> GetAllAsync()
    {
        var categories = await _categoriesRepository.GetAllAsync();
        categories.OrderBy(o => o.CategoryName).ToList();
        return categories;
    }
    public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
    {
        var response = await _http.GetAsync(_configuration["ApiUris:Categories"]);
        if (response.IsSuccessStatusCode)
        {
            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(await response.Content.ReadAsStringAsync());
            return categories ??= null!;
        }
        return null!;
    }
}


