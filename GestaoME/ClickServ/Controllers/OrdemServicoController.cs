using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClickServ.Models;
using ClickServ.Repository;

namespace ClickServ.Controllers
{
    public class OrdemServicoController : Controller
    {
        private readonly ClickServContext _context;

        public OrdemServicoController(ClickServContext context)
        {
            _context = context;
        }

        // GET: OrdemServico
        public async Task<IActionResult> Index()
        {
            var clickServContext = _context.OrdemServicos.Include(o => o.Colaborador).Include(o => o.Equipamento);
            return View(await clickServContext.ToListAsync());
        }

        // GET: OrdemServico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordemServico = await _context.OrdemServicos
                .Include(o => o.Colaborador)
                .Include(o => o.Equipamento)
                .FirstOrDefaultAsync(m => m.OrdemServicoID == id);
            if (ordemServico == null)
            {
                return NotFound();
            }

            return View(ordemServico);
        }

        // GET: OrdemServico/Create
        public IActionResult Create()
        {
            ViewData["ColaboradorID"] = new SelectList(_context.Colaboradors, "Nome", "Nome");
            ViewData["EquipamentoID"] = new SelectList(_context.Equipamentos, "EquipamentoID", "EquipamentoID");
            return View();
        }

        // POST: OrdemServico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdemServicoID,EquipamentoID,ColaboradorID,Data,Valor,Defeito,Relatorio")] OrdemServico ordemServico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordemServico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColaboradorID"] = new SelectList(_context.Colaboradors, "Nome", "Nome", ordemServico.ColaboradorID);
            ViewData["EquipamentoID"] = new SelectList(_context.Equipamentos, "EquipamentoID", "EquipamentoID", ordemServico.EquipamentoID);
            return View(ordemServico);
        }

        // GET: OrdemServico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordemServico = await _context.OrdemServicos.FindAsync(id);
            if (ordemServico == null)
            {
                return NotFound();
            }
            ViewData["ColaboradorID"] = new SelectList(_context.Colaboradors, "ColaboradorID", "ColaboradorID", ordemServico.ColaboradorID);
            ViewData["EquipamentoID"] = new SelectList(_context.Equipamentos, "EquipamentoID", "EquipamentoID", ordemServico.EquipamentoID);
            return View(ordemServico);
        }

        // POST: OrdemServico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrdemServicoID,EquipamentoID,ColaboradorID,Data,Valor,Defeito,Relatorio")] OrdemServico ordemServico)
        {
            if (id != ordemServico.OrdemServicoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordemServico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdemServicoExists(ordemServico.OrdemServicoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColaboradorID"] = new SelectList(_context.Colaboradors, "ColaboradorID", "ColaboradorID", ordemServico.ColaboradorID);
            ViewData["EquipamentoID"] = new SelectList(_context.Equipamentos, "EquipamentoID", "EquipamentoID", ordemServico.EquipamentoID);
            return View(ordemServico);
        }

        // GET: OrdemServico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordemServico = await _context.OrdemServicos
                .Include(o => o.Colaborador)
                .Include(o => o.Equipamento)
                .FirstOrDefaultAsync(m => m.OrdemServicoID == id);
            if (ordemServico == null)
            {
                return NotFound();
            }

            return View(ordemServico);
        }

        // POST: OrdemServico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordemServico = await _context.OrdemServicos.FindAsync(id);
            _context.OrdemServicos.Remove(ordemServico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdemServicoExists(int id)
        {
            return _context.OrdemServicos.Any(e => e.OrdemServicoID == id);
        }
    }
}
