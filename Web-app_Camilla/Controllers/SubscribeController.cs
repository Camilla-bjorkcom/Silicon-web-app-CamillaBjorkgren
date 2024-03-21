using Microsoft.AspNetCore.Mvc;
using Web_app_Camilla.ViewModels;

namespace Web_app_Camilla.Controllers;

public class SubscribeController : Controller
{
    public IActionResult Subscribe()
    {
        
        return View(new SubscriberViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(SubscriberViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            using var http = new HttpClient();

            var url = $"https://localhost:7138/api/subscribers?email={viewModel.Email}";
            var request = new HttpRequestMessage(HttpMethod.Post, url);



            var response = await http.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                viewModel.IsSubscribed = true;
            }
        }
        return View(viewModel);
    }
}
