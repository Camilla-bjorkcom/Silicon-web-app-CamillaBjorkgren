using Microsoft.AspNetCore.Mvc;
using Web_app_Camilla.ViewModels;

namespace Web_app_Camilla.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactForm(ContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return View(viewModel);
            }
            return View(viewModel);
        }
    }
}
