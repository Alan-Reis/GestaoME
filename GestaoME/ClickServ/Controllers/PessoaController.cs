using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClickServ.Models;
using ClickServ.Repository;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClickServ.Controllers
{
    public class PessoaController : Controller
    {
        private readonly ClickServContext _context;

        public PessoaController(ClickServContext context)
        {
            _context = context;
        }

        // GET: Pessoa
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pessoas.ToListAsync());
        }

        // GET: Pessoa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .Include(e => e.Enderecos)
                    .Include(c => c.Contatos)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.PessoaID == id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("PessoaID,Nome,Logradouro,Bairro,Complemento,CidadeTelefone,celular,Email")] Pessoa pessoa, Endereco endereco, Contato contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //_context.Add(pessoa);
                    _context.Add(endereco);
                    //_context.Add(contato);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Não foi possível salvar. " +
                                        "Tente novamente, e se o problema persistir " +
                                        "chame o suporte.");
            }
            return View(pessoa);
        }

        public async Task<IActionResult> AdicionarEndereco(
            [Bind("PessoaID,Logradouro,Bairro,Complemento,Cidade")]Endereco endereco)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(endereco);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details));
                }
            }
            catch(DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Não foi possível salvar. " +
                                       "Tente novamente, e se o problema persistir " +
                                       "chame o suporte.");
            }
            return View();
        }


        public async Task<IActionResult> AdicionarContato(
            [Bind("PessoaID,Telefone,celular,Email")] Contato contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(contato);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Não foi possível salvar. " +
                                       "Tente novamente, e se o problema persistir " +
                                       "chame o suporte.");
            }
            return View();
        }

        // GET: Pessoa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PessoaID,Nome")] Pessoa pessoa)
        {
            if (id != pessoa.PessoaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.PessoaID))
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
            return View(pessoa);
        }

        // GET: Pessoa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .FirstOrDefaultAsync(m => m.PessoaID == id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            _context.Pessoas.Remove(pessoa);

            //Caso o cliente tenha Endereço
            if(pessoa.Enderecos != null)
            {
                //var endereco = _context.Enderecos.Where(e => e.PessoaID == id).First();
                //_context.Enderecos.Remove(endereco);
            }

            var endereco = _context.Enderecos.Where(e => e.PessoaID == id).First();
            _context.Enderecos.Remove(endereco);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
             _context.Enderecos.Any(p => p.PessoaID == id);
            return _context.Pessoas.Any(p => p.PessoaID == id);
        }
    }
}
