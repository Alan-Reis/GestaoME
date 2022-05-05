using ClickServ2022.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClickServ2022.Controllers
{
    public class CEPController : Controller
    {
        public IActionResult Index(string view, int id)
        {
         
            if(view == "Create")
            {
                ViewBag.View = view;
                return View();
            }
            else
            {
                ViewBag.View = view;
                ViewBag.ClienteID = id;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Index(Endereco endereco, int id)
        {
            if (id != 0)
            {
                endereco.EnderecoID = id;
                return RedirectToAction("AddDados", "Cliente", endereco);
            }
            return RedirectToAction("Create", "Cliente", endereco);
        }
    }
}
