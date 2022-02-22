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
            List<Cliente> listCliente = new List<Cliente>();
            listCliente = cliente.GetAllClientes().ToList();
            return View(listCliente);
        }

        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Cliente cliente = this.cliente.GetCliente(id);

            if(cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                this.cliente.AddCliente(cliente);
                return RedirectToAction("Index");
            }
           
            return View(cliente);
        }


        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Cliente cliente = this.cliente.GetCliente(id);

            if(cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Cliente cliente)
        {
            if(id != cliente.ClienteID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                this.cliente.UpdateCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Cliente cliente = this.cliente.GetCliente(id);

            if(cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            cliente.DeleteCliente(id);
            return RedirectToAction("Index");
        }
    }
}
