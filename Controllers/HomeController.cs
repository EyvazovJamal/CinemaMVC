using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cinema.Models;

namespace Cinema.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    
}
