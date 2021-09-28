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
    public class EquipamentoController : Controller
    {
        private readonly ClickServContext _context;

        public EquipamentoController(ClickServContext context)
        {
            _context = context;
        }

        // GET: Equipamento
        public async Task<IActionResult> Index()
        {
            var clickServContext = _context.Equipamentos.Include(e => e.Pessoa);
            return View(await clickServContext.ToListAsync());
        }

        // GET: Equipamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamentos
                .Include(e => e.Pessoa)
                .FirstOrDefaultAsync(m => m.EquipamentoID == id);
            if (equipamento == null)
            {
                return NotFound();
            }

            return View(equipamento);
        }

        // GET: Equipamento/Create
        public IActionResult Create()
        {
            ViewData["PessoaID"] = new SelectList(_context.Pessoas, "PessoaID", "PessoaID");
            return View();
        }

        // POST: Equipamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipamentoID,PessoaID,Tipo,Fabricante,Modelo,NSerie")] Equipamento equipamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PessoaID"] = new SelectList(_context.Pessoas, "PessoaID", "PessoaID", equipamento.PessoaID);
            return View(equipamento);
        }

        // GET: Equipamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamentos.FindAsync(id);
            if (equipamento == null)
            {
                return NotFound();
            }
            ViewData["PessoaID"] = new SelectList(_context.Pessoas, "PessoaID", "PessoaID", equipamento.PessoaID);
            return View(equipamento);
        }

        // POST: Equipamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipamentoID,PessoaID,Tipo,Fabricante,Modelo,NSerie")] Equipamento equipamento)
        {
            if (id != equipamento.EquipamentoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipamentoExists(equipamento.EquipamentoID))
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
            ViewData["PessoaID"] = new SelectList(_context.Pessoas, "PessoaID", "PessoaID", equipamento.PessoaID);
            return View(equipamento);
        }

        // GET: Equipamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamentos
                .Include(e => e.Pessoa)
                .FirstOrDefaultAsync(m => m.EquipamentoID == id);
            if (equipamento == null)
            {
                return NotFound();
            }

            return View(equipamento);
        }

        // POST: Equipamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipamento = await _context.Equipamentos.FindAsync(id);
            _context.Equipamentos.Remove(equipamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipamentoExists(int id)
        {
            return _context.Equipamentos.Any(e => e.EquipamentoID == id);
        }
    }
}
