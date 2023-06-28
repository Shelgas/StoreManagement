using Microsoft.AspNetCore.Mvc;

namespace SM.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
