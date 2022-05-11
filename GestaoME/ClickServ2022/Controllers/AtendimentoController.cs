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
    public class AtendimentoController : Controller
    {
        private readonly IRepositoryDAL atendimento;

        public AtendimentoController(IRepositoryDAL _atendimento)
        {
            atendimento = _atendimento;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int? id, string equip)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            //Popular um SelectList para os técnico
            ViewBag.Tecnico = this.atendimento.GetAllColaborador().Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Nome }).ToList();

            Atendimento atendimento = new Atendimento();
            Equipamento equipamento = new Equipamento();
            equipamento.EquipamentoID = (int)id;
            atendimento.Equipamento = equipamento;
            atendimento.Data = DateTime.Now;

            if (atendimento == null)
            {
                return NotFound();
            }
            
            return View(atendimento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Atendimento atendimento)
        {
            if (ModelState.IsValid)
            {
                this.atendimento.AddAtendimento(atendimento);
                //Erro ao redirecionar, precida informar  id do Cliente
                return RedirectToAction("Details", "Endereco", new { id = atendimento.Equipamento.EquipamentoID });
            }

            return View(atendimento);
        }
    }
}
