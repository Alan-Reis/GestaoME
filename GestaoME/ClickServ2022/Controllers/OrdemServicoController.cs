using ClickServ2022.Models;
using ClickServ2022.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClickServ2022.Controllers
{
    public class OrdemServicoController : Controller
    {
        private readonly IRepositoryDAL ordemservico;

        public OrdemServicoController(IRepositoryDAL _ordemservico)
        {
            ordemservico = _ordemservico;
        }

        public IActionResult Index(int? pagina)
        {
            string view = "OS";
            int os = 0;
            List<OrdemServico> ordemServicos = new List<OrdemServico>();
            ordemServicos = this.ordemservico.GetAllOrdemServico(os, view).ToList();

            //paginação
            int paginaTamanho = 4;
            int paginaNumero = (pagina ?? 1);
            //fim

            return View(ordemServicos.ToPagedList(paginaNumero, paginaTamanho));
        }

        [HttpPost]
        public IActionResult Index(int? pagina, int? os)
        {

            string view = "OS";
            List<OrdemServico> ordemServicos = new List<OrdemServico>();
            ordemServicos = this.ordemservico.GetAllOrdemServico(os, view).ToList();

            //paginação
            int paginaTamanho = 4;
            int paginaNumero = (pagina ?? 1);
            //fim

            return View(ordemServicos.ToPagedList(paginaNumero, paginaTamanho));
        }

        [HttpPost]
        public JsonResult ValidarOS(int? os)
        {
            OrdemServico ordemServico = this.ordemservico.GetOrdemServico(os);

            //Se tiver a ordem de serviço digitado no campo Ordem de Serviço entra no IF e 
            //retorna para a função enviar() da página Create.
            if (ordemServico.OrdemServicoID == os)
            {
                return Json(1);
            }
            return Json(0);
        }

        public IActionResult Create(int? id, string redirecionar)
        {
            ViewBag.Redirecionar = redirecionar;
           
            if (id == null)
            {
                return NotFound();
            }

            Equipamento equipamento = this.ordemservico.GetEquipamento(id);

            //Popular um SelectList para os técnico
            ViewBag.Tecnico = this.ordemservico.GetAllColaborador().Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Nome }).ToList();

            //Popular um SelectList para os categoria
            ViewBag.Categoria = this.ordemservico.GetAllCategoria().Select(c => new SelectListItem()
            { Text = c.Categoria, Value = c.Categoria }).ToList();

            OrdemServico ordemservico = new OrdemServico();
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
        public IActionResult Create([Bind] OrdemServico ordemservico, int? id, string redirecionar)
        {
            Equipamento equipamento = this.ordemservico.GetEquipamento(id);
            ordemservico.Equipamento = equipamento;

            if (ModelState.IsValid)
            {
                string duplicado = "N";
                this.ordemservico.AddOrdemServico(ordemservico, duplicado);
                if(redirecionar == "sistema")
                {
                    return RedirectToAction("Details", "Sistema", new { id = ordemservico.Equipamento.Sistema.SistemaID });
                }
                return RedirectToAction("Details", "Endereco", new { id = ordemservico.Equipamento.Endereco.EnderecoID });
            }

            return View(ordemservico);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Popular um SelectList para os técnico
            ViewBag.Tecnico = this.ordemservico.GetAllColaborador().Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Nome }).ToList();

            //Popular um SelectList para os categoria
            ViewBag.Categoria = this.ordemservico.GetAllCategoria().Select(c => new SelectListItem()
            { Text = c.Categoria, Value = c.Categoria }).ToList();

            OrdemServico ordemServico = this.ordemservico.GetOrdemServico(id);

            if (ordemservico == null)
            {
                return NotFound();
            }
            return View(ordemServico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] OrdemServico ordemServico)
        {
            if (id != ordemServico.OrdemServicoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                this.ordemservico.UpdateOrdemServico(ordemServico);
                return RedirectToAction("Details", "Equipamento", new { id = ordemServico.Equipamento.EquipamentoID });
            }
            return View(ordemServico);
        }

        public IActionResult InsertDuplicado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.EquipamentoCompleto = this.ordemservico.GetAllEquipamentosCliente(id).Select(c => new SelectListItem()
            { Text = c.Tipo, Value = c.EquipamentoID.ToString() }).ToList();

            OrdemServico ordemServico = this.ordemservico.GetOrdemServico(id);

            if (ordemservico == null)
            {
                return NotFound();
            }
            return View(ordemServico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertDuplicado(int id, [Bind] OrdemServico ordemServico)
        {
            if (ModelState.IsValid)
            {
                string duplicado = "D";
                this.ordemservico.AddOrdemServico(ordemServico, duplicado);
                return RedirectToAction("Details", "Equipamento", new { id = ordemServico.Equipamento.EquipamentoID });
            }
            return View(ordemServico);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrdemServico ordemServico = this.ordemservico.GetOrdemServico(id);

            if (ordemServico == null)
            {
                return NotFound();
            }
            return View(ordemServico);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            OrdemServico ordemServico = this.ordemservico.GetOrdemServico(id);

            this.ordemservico.DeleteOrdemServico(id);
            return RedirectToAction("Details", "Equipamento", new { id = ordemServico.Equipamento.EquipamentoID });
        }

    }
}
