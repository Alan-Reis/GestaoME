using ClickServ2022.Models;
using ClickServ2022.Repository;
using ClickServ2022.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace ClickServ2022.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IRepositoryDAL cliente;
        public ClienteController(IRepositoryDAL _cliente)
        {
            cliente = _cliente;
        }
        public IActionResult Index()
        {
            List<Pessoa> listCliente = new List<Pessoa>();
            listCliente = cliente.GetAllClientes().ToList();
            return View(listCliente);
        }
    }
}
