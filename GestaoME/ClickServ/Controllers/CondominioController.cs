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
    public class CondominioController : Controller
    {
        private readonly ClickServContext _context;

        public CondominioController(ClickServContext context)
        {
            _context = context;
        }

        // GET: Condominio
        public async Task<IActionResult> Index()
        {
            var clickServContext = _context.Condominios.Include(c => c.Endereco);
            return View(await clickServContext.ToListAsync());
        }

        // GET: Condominio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condominio = await _context.Condominios
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(m => m.CondominioID == id);
            if (condominio == null)
            {
                return NotFound();
            }

            return View(condominio);
        }

        // GET: Condominio/Create
        public IActionResult Create()
        {
            ViewData["EnderecoID"] = new SelectList(_context.Enderecos, "EnderecoID", "EnderecoID");
            return View();
        }

        // POST: Condominio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CondominioID,EnderecoID,Nome,Torre,Apartamento")] Condominio condominio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(condominio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoID"] = new SelectList(_context.Enderecos, "EnderecoID", "EnderecoID", condominio.EnderecoID);
            return View(condominio);
        }

        // GET: Condominio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condominio = await _context.Condominios.FindAsync(id);
            if (condominio == null)
            {
                return NotFound();
            }
            ViewData["EnderecoID"] = new SelectList(_context.Enderecos, "EnderecoID", "EnderecoID", condominio.EnderecoID);
            return View(condominio);
        }

        // POST: Condominio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CondominioID,EnderecoID,Nome,Torre,Apartamento")] Condominio condominio)
        {
            if (id != condominio.CondominioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(condominio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CondominioExists(condominio.CondominioID))
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
            ViewData["EnderecoID"] = new SelectList(_context.Enderecos, "EnderecoID", "EnderecoID", condominio.EnderecoID);
            return View(condominio);
        }

        // GET: Condominio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condominio = await _context.Condominios
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(m => m.CondominioID == id);
            if (condominio == null)
            {
                return NotFound();
            }

            return View(condominio);
        }

        // POST: Condominio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var condominio = await _context.Condominios.FindAsync(id);
            _context.Condominios.Remove(condominio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CondominioExists(int id)
        {
            return _context.Condominios.Any(e => e.CondominioID == id);
        }
    }
}
