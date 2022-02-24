using ClickServ2022.Models;
using ClickServ2022.Service;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamento equipamento = this.equipamento.GetEquipamento(id);

            if (equipamento == null)
            {
                return NotFound();
            }
            return View(equipamento);
        }

        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamento equipamento = new Equipamento();
            Cliente cliente = new Cliente();
            cliente.ClienteID = (int)id;
            equipamento.Cliente = cliente;

            if (equipamento == null)
            {
                return NotFound();
            }

            return View(equipamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Equipamento equipamento)
        {
            if (ModelState.IsValid)
            {
                this.equipamento.AddEquipamento(equipamento);
                return RedirectToAction("Details", "Cliente", new { id = equipamento.Cliente.ClienteID });
            }
            return View(equipamento);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamento equipamento = this.equipamento.GetEquipamento(id);

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
            if (id != equipamento.EquipamentoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                this.equipamento.UpdateEquipamento(equipamento);
                return RedirectToAction("Details", "Cliente", new { id = equipamento.Cliente.ClienteID });
            }
            return View(equipamento);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Equipamento equipamento = this.equipamento.GetEquipamento(id);

            if (equipamento == null)
            {
                return NotFound();
            }

            return View(equipamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Equipamento equipamento = this.equipamento.GetEquipamento(id);

            this.equipamento.DeleteEquipamento(id);
            return RedirectToAction("Details", "Cliente", new { id = equipamento.Cliente.ClienteID });
        }

    }
}
