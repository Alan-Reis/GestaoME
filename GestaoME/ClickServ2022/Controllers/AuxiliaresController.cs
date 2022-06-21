using Microsoft.AspNetCore.Mvc;

namespace ClickServ2022.Controllers
{
    public class AuxiliaresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PopupContato()
        {
            return View();
        }
    }
}
