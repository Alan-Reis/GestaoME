using ClickServ2022.Models;
using ClickServ2022.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClickServ2022.Controllers
{
    public class ContatoAuxiliarController : Controller
    {
        private readonly IRepositoryDAL contato;

        public ContatoAuxiliarController(IRepositoryDAL _contato)
        {
            contato = _contato;
        }

        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContatoAuxiliar contato = new ContatoAuxiliar();
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
        public IActionResult Create([Bind] ContatoAuxiliar contato)
        {
            if (ModelState.IsValid)
            {
                this.contato.AddContatoAuxiliar(contato);
                return RedirectToAction("Details", "Contrato", new { id = contato.Cliente.ClienteID });
            }

            return View(contato);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContatoAuxiliar contato = this.contato.GetContatoAuxiliar(id);

            if (contato == null)
            {
                return NotFound();
            }
            return View(contato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] ContatoAuxiliar contato)
        {
            if (id != contato.ContatoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                this.contato.UpdateContatoAuxiliar(contato);
                return RedirectToAction("Details", "Contrato", new { id = contato.Cliente.ClienteID });
            }
            return View(contato);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContatoAuxiliar contato = this.contato.GetContatoAuxiliar(id);

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
            ContatoAuxiliar contato = this.contato.GetContatoAuxiliar(id);

            this.contato.DeleteContato(id);
            return RedirectToAction("Details", "Contrato", new { id = contato.Cliente.ClienteID });
        }
    }
}
