using ClickServ2022.Models;
using ClickServ2022.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClickServ2022.Controllers
{
    public class SistemaController : Controller
    {
        private readonly IRepositoryDAL sistema;

        public SistemaController(IRepositoryDAL _sistema)
        {
            sistema = _sistema;
        }

        public IActionResult AddSistema(int? id)
        {
            Sistema sistema = new Sistema();
            Cliente contrato = new Cliente();
            contrato.ClienteID = (int)id;
            sistema.Cliente = contrato;

            if (id == null)
            {
                return NotFound();
            }

            return View(sistema);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSistema(Sistema sistema, int? id)
        {
            Cliente cliente = new Cliente();
            cliente.ClienteID = (int)id;
            sistema.Cliente = cliente;

            if (ModelState.IsValid)
            {
                this.sistema.AddSistema(sistema);
                return RedirectToAction("Details", "Contrato", new { id = cliente.ClienteID });
            }

            return View();
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Pesquisar equipamento por SistemaID
            string sistemaEndereco = "Sistema";
            Sistema sistema = this.sistema.GetSistema(id);
            sistema.Equipamentos = this.sistema.GetEquipamentos(id, sistemaEndereco);

            if (sistema == null)
            {
                return NotFound();
            }
            return View(sistema);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] Sistema sistema)
        {
            if (ModelState.IsValid)
            {
                this.sistema.UpdateSistema(sistema);
                return RedirectToAction("Details", "Sistema", new { id = sistema.SistemaID });
            }
            return View(sistema);
        }
    }
}
