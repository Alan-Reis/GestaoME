using ClickServ2022.Models;
using ClickServ2022.Service;
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

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Pesquisar equipamento por EnderecoID
            string sistemaEndereco = "Endereco";
            string sistema = "endereco";
            Endereco endereco = this.endereco.GetEndereco(id, sistema);
            endereco.Equipamentos = this.endereco.GetEquipamentos(id, sistemaEndereco);

            if (endereco == null)
            {
                return NotFound();
            }
            return View(endereco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] Endereco endereco, string redirecionar)
        {
            if (ModelState.IsValid)
            {
                this.endereco.UpdateEndereco(endereco);
                if(redirecionar == "sistema")
                {
                    return RedirectToAction("Details", "Contrato", new { id = endereco.Cliente.ClienteID });
                }
                return RedirectToAction("Details", "Endereco", new { id = endereco.EnderecoID });
            }
            return View(endereco);
        }
    }
}
