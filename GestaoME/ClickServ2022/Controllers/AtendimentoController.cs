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

        public IActionResult Create(int? id, string view)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamento equipamento = this.atendimento.GetEquipamento(id, view);

            //Popular um SelectList para os técnico
            ViewBag.Tecnico = this.atendimento.GetAllColaborador().Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Nome }).ToList();

            //Popular um SelectList para período
            ViewBag.Periodo = this.atendimento.GetPeriodo().Select(c => new SelectListItem()
            { Text = c.Periodo, Value = c.Periodo }).ToList();

            Atendimento atendimento = new Atendimento();
            atendimento.Equipamento = equipamento;
            atendimento.Data = DateTime.Now.Date;

            if (atendimento == null)
            {
                return NotFound();
            }

            return View(atendimento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Atendimento atendimento, int? id, string view)
        {

            Equipamento equipamento = this.atendimento.GetEquipamento(id, view);
            atendimento.Equipamento = equipamento;

            if (ModelState.IsValid)
            {
                this.atendimento.AddAtendimento(atendimento);
                return RedirectToAction("Details", "Endereco", new { id = atendimento.Equipamento.Endereco.EnderecoID });
            }
            return View(atendimento);
        }

        [HttpPost]
        public IActionResult RelatorioAtendimento(string data)
        {            
            List<RelatorioAtendimento> atendimentoList = new List<RelatorioAtendimento>();
            atendimentoList = atendimento.RelatorioAtendimento(data).ToList();

            return View(atendimentoList);
        }

        public IActionResult RelatorioAtendimento()
        {
            string data = DateTime.Now.ToString("yyyy-MM-dd");
            List<RelatorioAtendimento> atendimentoList = new List<RelatorioAtendimento>();
            atendimentoList = atendimento.RelatorioAtendimento(data).ToList();

            return View(atendimentoList);
        }

        public JsonResult Atendimentos()
        {
            List<Evento> listEvento = new List<Evento>();
            listEvento = this.atendimento.GetAllEventos().ToList();

            ViewData["events"] = listEvento;

            return Json(listEvento);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Popular um SelectList para os técnico
            ViewBag.Tecnico = this.atendimento.GetAllColaborador().Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Nome }).ToList();

            //Popular um SelectList para período
            ViewBag.Periodo = this.atendimento.GetPeriodo().Select(c => new SelectListItem()
            { Text = c.Periodo, Value = c.Periodo }).ToList();

            Atendimento atendimento = this.atendimento.GetAtendimento(id);

            if (atendimento == null)
            {
                return NotFound();
            }
            return View(atendimento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Atendimento atendimento)
        {
            if (id != atendimento.AtendimentoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                this.atendimento.UpdateAtendimento(atendimento);
                return RedirectToAction("Index", "Home");
            }
            return View(atendimento);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Atendimento atendimento = this.atendimento.GetAtendimento(id);

            if (atendimento == null)
            {
                return NotFound();
            }
            return View(atendimento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            Atendimento atendimento = this.atendimento.GetAtendimento(id);

            this.atendimento.DeleteAtendimento(id);
            return RedirectToAction("Index", "Home");
        }

    }
}

