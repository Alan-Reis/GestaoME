
using ClickServ2022.Models;
using ClickServ2022.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ2022.Controllers
{
    public class OrdemServicoController : Controller
    {
        private readonly IRepositoryDAL ordemservico;

        public OrdemServicoController(IRepositoryDAL _ordemservico)
        {
            ordemservico = _ordemservico;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ValidarOS(int? os)
        {
            OrdemServico ordemServico = this.ordemservico.GetOrdemServico(os);
            
            //Se tiver a ordem de serviço digitado no campo Ordem de Serviço entra no IF e 
            //retorna para a função enviar() da página Create.
            if(ordemServico.OrdemServicoID == os)
            {
                return Json(1);
            }
            return Json(0);
        }

        public IActionResult Create(int? id, string equip)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Popular um SelectList para os técnico
            ViewBag.Tecnico = this.ordemservico.GetAllColaborador().Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Nome }).ToList();

            OrdemServico ordemservico = new OrdemServico();
            Equipamento equipamento = this.ordemservico.GetEquipamento(id, equip);
            ordemservico.Equipamento = equipamento;
            ordemservico.Data = DateTime.Now.Date;

            if (ordemservico == null)
            {
                return NotFound();
            }

            return View(ordemservico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] OrdemServico ordemservico, int? id, string equip)
        {
            Equipamento equipamento = this.ordemservico.GetEquipamento(id, equip);
            ordemservico.Equipamento = equipamento;

            if (ModelState.IsValid)
            {
                this.ordemservico.AddOrdemServico(ordemservico);
                return RedirectToAction("Index", "Home");
            }

            return View(ordemservico);
        }
    }
}
