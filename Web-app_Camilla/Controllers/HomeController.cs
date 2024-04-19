using Azure.Core;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Text;
using Web_app_Camilla.ViewModels;
using static System.Net.WebRequestMethods;

namespace Web_app_Camilla.Controllers;

public class HomeController : Controller
{
   
    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }



    [Route("/error")]
    public IActionResult Error404(int statusCode) => View();

    [Route("/denied")]
    public IActionResult AccessDenied(int statusCode) => View();

    public IActionResult TermsPolicy () => View();
}
