using Microsoft.AspNetCore.Mvc;

namespace projectCleanArch.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
