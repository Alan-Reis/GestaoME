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
    public class EquipamentoController : Controller
    {
        private readonly IRepositoryDAL equipamento;

        public EquipamentoController(IRepositoryDAL _equipamento)
        {
            equipamento = _equipamento;
        }
        public IActionResult Details(int? id, string view)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamento equipamento = this.equipamento.GetEquipamento(id, view);

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


        public IActionResult Create(int? id, string tipo, string fabricante)
        {
            
            string view = "Endereco";
            var cliente = this.equipamento.GetEquipamento(id, view);

            Equipamento equipamento = new Equipamento();
            Endereco endereco = new Endereco();
            endereco.EnderecoID = (int)id;
            equipamento.Endereco = endereco;
            equipamento.Cliente = cliente.Cliente;

            if (equipamento == null)
            {
                return NotFound();
            }

            return View(equipamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Equipamento equipamento, string tipo)
        {
            if (ModelState.IsValid)
            {
                this.equipamento.AddEquipamento(equipamento);
                return RedirectToAction("Details", "Endereco", new { id = equipamento.Endereco.EnderecoID });
            }
            return View(equipamento);
        }

        public IActionResult Edit(int? id, string view)
        {
            

            if (id == null)
            {
                return NotFound();
            }

            Equipamento equipamento = this.equipamento.GetEquipamento(id, view);

            ViewBag.Tipo = equipamento.Tipo;
            ViewBag.Fabricante = equipamento.Fabricante;
            ViewBag.Modelo = equipamento.Modelo;


            view = "Endereco";
            int cliente = equipamento.Cliente.ClienteID;
            Endereco endereco = this.equipamento.GetEndereco(cliente, view);
            equipamento.Endereco = endereco;

            if (equipamento == null)
            {
                return NotFound();
            }

            return View(equipamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Equipamento equipamento)
        {
            string view = "Endereco";
            int cliente = equipamento.Cliente.ClienteID;
            Endereco endereco = this.equipamento.GetEndereco(cliente, view);

            if (id != equipamento.EquipamentoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                this.equipamento.UpdateEquipamento(equipamento);
                equipamento.Endereco = endereco;
                return RedirectToAction("Details", "Endereco", new { id = equipamento.Endereco.EnderecoID });
            }
            return View(equipamento);
        }

        public IActionResult Delete(int? id, string view)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamento equipamento = this.equipamento.GetEquipamento(id, view);

            if (equipamento == null)
            {
                return NotFound();
            }

            return View(equipamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string view)
        {
            Equipamento equipamento = this.equipamento.GetEquipamento(id, view);

            this.equipamento.DeleteEquipamento(id);
            return RedirectToAction("Details", "Cliente", new { id = equipamento.Cliente.ClienteID });
        }

    }
}
