using ClickServ2022.Models;
using ClickServ2022.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClickServ2022.Controllers
{
    public class EquipamentoController : Controller
    {
        private readonly IRepositoryDAL equipamento;

        public EquipamentoController(IRepositoryDAL _equipamento)
        {
            equipamento = _equipamento;
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamento equipamento = this.equipamento.GetEquipamento(id);
            //Utilizado para pegar criar a lista no Details dos equipamentos 
            string view = null;
            equipamento.OrdemServicos = this.equipamento.GetAllOrdemServico(id, view);

            ViewBag.Tipo = equipamento.Tipo;
            ViewBag.Fabricante = equipamento.Fabricante;
            ViewBag.Modelo = equipamento.Modelo;

            if (equipamento == null)
            {
                return NotFound();
            }
            return View(equipamento);
        }


        public JsonResult Equipamento()
        {
            ViewBag.Equipamento = this.equipamento.GetAllTipoEquipamento();

            return Json(ViewBag.Equipamento);
        }

        [HttpPost]
        public JsonResult Fabricante(string fabri)
        {
            ViewBag.Fabricante = this.equipamento.GetAllFabricante(fabri);

            return Json(ViewBag.Fabricante);
        }

        public JsonResult Modelo(string model)
        {
            ViewBag.Modelo = this.equipamento.GetAllModelo(model);

            return Json(ViewBag.Modelo);
        }


        public IActionResult Create(int? id, string tipoEnderecoSistema, string sistema)
        {
            ViewBag.tipoEnderecoSistema = tipoEnderecoSistema;

            var cliente = this.equipamento.GetEndereco(id, sistema);

            Equipamento equipamento = new Equipamento();
            equipamento.Endereco = cliente;
            equipamento.Cliente = cliente.Cliente;

            return View(equipamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Cliente cliente, int? id, string tipoEnderecoSistema)
        {
           
            if(tipoEnderecoSistema == "sistema")
            {
                Sistema sistema = new Sistema();
                sistema.SistemaID = (int)id;

                Endereco endereco = new Endereco();
                endereco.EnderecoID = 0;

                cliente.Endereco = endereco;
                cliente.Sistema = sistema;

                if (ModelState.IsValid)
                {
                    this.equipamento.AddEquipamento(cliente);
                    return RedirectToAction("Details", "Sistema", new { id = cliente.Sistema.SistemaID });
                }
            }
            else
            {
                Endereco endereco = new Endereco();
                endereco.EnderecoID = (int)id;

                Sistema sistema = new Sistema();
                sistema.SistemaID = 0;

                cliente.Sistema = sistema;
                cliente.Endereco = endereco;

                if (ModelState.IsValid)
                {
                    this.equipamento.AddEquipamento(cliente);
                    return RedirectToAction("Details", "Endereco", new { id = cliente.Endereco.EnderecoID });
                }
            }
           
            return View(equipamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] Equipamento equipamento, string sistema)
        {
            int cliente = equipamento.Cliente.ClienteID;
            Endereco endereco = this.equipamento.GetEndereco(cliente, sistema);

            if (ModelState.IsValid)
            {
                this.equipamento.UpdateEquipamento(equipamento);
                equipamento.Endereco = endereco;
                return RedirectToAction("Details", "Equipamento", new { id = equipamento.EquipamentoID });
            }
            return View(equipamento);
        }
    }
}
