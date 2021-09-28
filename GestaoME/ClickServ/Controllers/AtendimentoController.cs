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
    public class AtendimentoController : Controller
    {
        private readonly ClickServContext _context;

        public AtendimentoController(ClickServContext context)
        {
            _context = context;
        }

        // GET: Atendimento
        public async Task<IActionResult> Index()
        {
            var clickServContext = _context.Atendimentos.Include(a => a.Colaborador).Include(a => a.Equipamento);
            return View(await clickServContext.ToListAsync());
        }

        // GET: Atendimento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atendimento = await _context.Atendimentos
                .Include(a => a.Colaborador)
                .Include(a => a.Equipamento)
                .FirstOrDefaultAsync(m => m.AtendimentoID == id);
            if (atendimento == null)
            {
                return NotFound();
            }

            return View(atendimento);
        }

        // GET: Atendimento/Create
        public IActionResult Create()
        {
            ViewData["ColaboradorID"] = new SelectList(_context.Colaboradors, "ColaboradorID", "ColaboradorID");
            ViewData["EquipamentoID"] = new SelectList(_context.Equipamentos, "EquipamentoID", "EquipamentoID");
            return View();
        }

        // POST: Atendimento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AtendimentoID,EquipamentoID,ColaboradorID,Defeito,Data,Periodo,Observacao,Status")] Atendimento atendimento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(atendimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColaboradorID"] = new SelectList(_context.Colaboradors, "ColaboradorID", "ColaboradorID", atendimento.ColaboradorID);
            ViewData["EquipamentoID"] = new SelectList(_context.Equipamentos, "EquipamentoID", "EquipamentoID", atendimento.EquipamentoID);
            return View(atendimento);
        }

        // GET: Atendimento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atendimento = await _context.Atendimentos.FindAsync(id);
            if (atendimento == null)
            {
                return NotFound();
            }
            ViewData["ColaboradorID"] = new SelectList(_context.Colaboradors, "ColaboradorID", "ColaboradorID", atendimento.ColaboradorID);
            ViewData["EquipamentoID"] = new SelectList(_context.Equipamentos, "EquipamentoID", "EquipamentoID", atendimento.EquipamentoID);
            return View(atendimento);
        }

        // POST: Atendimento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AtendimentoID,EquipamentoID,ColaboradorID,Defeito,Data,Periodo,Observacao,Status")] Atendimento atendimento)
        {
            if (id != atendimento.AtendimentoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atendimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtendimentoExists(atendimento.AtendimentoID))
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
            ViewData["ColaboradorID"] = new SelectList(_context.Colaboradors, "ColaboradorID", "ColaboradorID", atendimento.ColaboradorID);
            ViewData["EquipamentoID"] = new SelectList(_context.Equipamentos, "EquipamentoID", "EquipamentoID", atendimento.EquipamentoID);
            return View(atendimento);
        }

        // GET: Atendimento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atendimento = await _context.Atendimentos
                .Include(a => a.Colaborador)
                .Include(a => a.Equipamento)
                .FirstOrDefaultAsync(m => m.AtendimentoID == id);
            if (atendimento == null)
            {
                return NotFound();
            }

            return View(atendimento);
        }

        // POST: Atendimento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var atendimento = await _context.Atendimentos.FindAsync(id);
            _context.Atendimentos.Remove(atendimento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtendimentoExists(int id)
        {
            return _context.Atendimentos.Any(e => e.AtendimentoID == id);
        }
    }
}
