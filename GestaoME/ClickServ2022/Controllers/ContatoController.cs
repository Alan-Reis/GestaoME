using ClickServ2022.Models;
using ClickServ2022.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ2022.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IRepositoryDAL contato;

        public ContatoController(IRepositoryDAL _contato)
        {
            contato = _contato;
        }

        public IActionResult Create(int? id)
        {  
            if (id == null)
            {
                return NotFound();
            }

            Contato contato = new Contato();
            Cliente cliente = new Cliente();
            cliente.ClienteID = (int)id;
            contato.Cliente = cliente;

            if (contato == null)
            {
                return NotFound();
            }

            return View(contato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind]Contato contato)
        {
            if(ModelState.IsValid)
            {
                this.contato.AddContato(contato);
                return RedirectToAction("Details", "Cliente", new {id = contato.Cliente.ClienteID});
            }

            return View(contato);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Contato contato = this.contato.GetContato(id);

            if(contato == null)
            {
                return NotFound();
            }
            return View(contato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,[Bind]Contato contato)
        {
            if(id != contato.ContatoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                this.contato.UpdateContato(contato);
                return RedirectToAction("Details", "Cliente", new {id = contato.Cliente.ClienteID});
            }
            return View(contato);        
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contato contato = this.contato.GetContato(id);

            if (contato == null)
            {
                return NotFound();
            }
            return View(contato);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            Contato contato = this.contato.GetContato(id);

            this.contato.DeleteContato(id);
            return RedirectToAction("Details", "Cliente", new { id = contato.Cliente.ClienteID });
        }
    }
}
