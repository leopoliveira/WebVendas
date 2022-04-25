using Microsoft.AspNetCore.Mvc;

namespace WebVendas.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
