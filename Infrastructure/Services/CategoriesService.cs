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
       try
        {
            var categories = await _categoriesRepository.GetAllAsync();
            if (categories != null)
            {
                categories.OrderBy(o => o.CategoryName).ToList();
                return categories;
            }
            return null!;
        }
        catch { return null!; }
       
    }
    public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
    {
        try
        {
            var response = await _http.GetAsync(_configuration["ApiUris:Categories"]);
            if (response.IsSuccessStatusCode)
            {
                var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(await response.Content.ReadAsStringAsync());
                if (categories != null)
                {
                    return categories;
                }
               
            }
            return null!;
        }
        catch { return null!; }
    }
}


