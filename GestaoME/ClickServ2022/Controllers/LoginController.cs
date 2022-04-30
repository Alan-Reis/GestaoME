using ClickServ2022.Service;
using Microsoft.AspNetCore.Mvc;
using ClickServ2022.Models;

namespace ClickServ2022.Controllers
{
    public class LoginController : Controller
    {
        private readonly IRepositoryDAL login;
        public LoginController(IRepositoryDAL _login)
        {
            login = _login;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string usuario, string senha)
        {
            Login login = this.login.GetLogin(usuario, senha);

            if (login.Usuario == null)
            {
                return NotFound();
            }

            if (login.Usuario == null)
            {
                ViewBag.Falha = "Usuário ou senha incorreta";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
