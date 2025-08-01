using Microsoft.AspNetCore.Mvc;

namespace FurionTest.Web;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}