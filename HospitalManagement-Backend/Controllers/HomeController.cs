using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement_Backend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
