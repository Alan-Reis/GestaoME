using ClickServ2022.Controllers;
using Microsoft.AspNetCore.Mvc;
using SistemaLogin.Models;
using SistemaLogin.Service;

namespace SistemaLogin.Controllers
{
    public class LoginController : Controller
    {
        string nome;

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
        public IActionResult Index(string Usuario, string Senha)
        {
            Login login = this.login.GetLogin(Usuario, Senha);

            if (login == null)
            {
                return NotFound();
            }

            if(Usuario != login.Usuario && Senha != login.Senha)
            {
                ViewBag.Falha = "Usuário ou senha incorreta";
                return View();

            }

            return RedirectToAction("Home", "Login", login);
        }

        public IActionResult Home(Login login)
         {
            ViewBag.Nome = login.Nome;
            return View();
        }
    }
}
