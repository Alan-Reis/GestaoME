using ClickServ2022.Models;
using ClickServ2022.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClickServ2022.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly IRepositoryDAL endereco;

        public EnderecoController(IRepositoryDAL _endereco)
        {
            endereco = _endereco;
        }

        public IActionResult Details(int? id, string view)
        {
            if(id == null)
            {
                return NotFound();
            }

            Endereco endereco = this.endereco.GetEndereco(id, view);

            if(endereco == null)
            {
                return NotFound();
            }
            return View(endereco);
        }

        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Endereco endereco = new Endereco();
            Cliente cliente = new Cliente();
            cliente.ClienteID = (int)id;
            endereco.Cliente = cliente;
                
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind]Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                this.endereco.AddEndereco(endereco);
                return RedirectToAction("Details", "Cliente", new { id = endereco.Cliente.ClienteID });
            }
            return View(endereco);
        }     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind]Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                this.endereco.UpdateEndereco(endereco);
                return RedirectToAction("Details", "Endereco", new { id = endereco.EnderecoID });
            }
            return View(endereco);
        }

        public IActionResult Delete(int? id, string view)
        {
            if(id == null)
            {
                return NotFound();
            }

            Endereco endereco = this.endereco.GetEndereco(id, view);

            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string view)
        {
            Endereco endereco = this.endereco.GetEndereco(id, view);

            this.endereco.DeleteEndereco(id);
            return RedirectToAction("Details", "Cliente", new { id = endereco.Cliente.ClienteID });
        }
    }
}
