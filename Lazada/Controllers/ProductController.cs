using Microsoft.AspNetCore.Mvc;

namespace Lazada.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
