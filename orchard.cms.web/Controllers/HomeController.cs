using Microsoft.AspNetCore.Mvc;

namespace orchard.cms.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
