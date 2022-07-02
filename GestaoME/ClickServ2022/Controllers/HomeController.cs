using ClickServ2022.Models;
using ClickServ2022.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ2022.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryDAL home;

        public HomeController(IRepositoryDAL _home)
        {
            home = _home;
        }

        public IActionResult Index()
        {
            List<Evento> listEvento = new List<Evento>();
            listEvento = this.home.GetAllEventos().ToList();

            return View(listEvento);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
