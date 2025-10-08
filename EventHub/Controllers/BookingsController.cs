using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class BookingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
