using Microsoft.AspNetCore.Mvc;

namespace SistemaLotes.Controllers
{
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
