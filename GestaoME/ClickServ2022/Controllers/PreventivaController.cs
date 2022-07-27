using ClickServ2022.Models;
using ClickServ2022.Service;
using GemBox.Document;
using GemBox.Document.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ClickServ2022.Controllers
{
    public class PreventivaController : Controller
    {
        private readonly IRepositoryDAL preventiva;

        public PreventivaController(IRepositoryDAL _preventiva)
        {
            preventiva = _preventiva;
        }

        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Preventiva preventiva = new Preventiva();

            preventiva.Mes = DateTime.Now.ToString("MMMM");
            preventiva.Ano = DateTime.Now.ToString("yyyy");
            preventiva.Data = DateTime.Now.Date;

            //Popular um SelectList para os técnico
            ViewBag.Tecnico = this.preventiva.GetAllColaborador().Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Nome }).ToList();
 
            Cliente cliente = new Cliente();
            cliente.ClienteID = (int)id;
            preventiva.Cliente = cliente;

            if (preventiva == null)
            {
                return NotFound();
            }

            return View(preventiva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Preventiva preventiva)
        {
            if (ModelState.IsValid)
            {
                this.preventiva.AddPreventiva(preventiva);
                return RedirectToAction("Details", "Contrato", new { id = preventiva.Cliente.ClienteID });
            }

            return View(preventiva);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Popular um SelectList para os técnico
            ViewBag.Tecnico = this.preventiva.GetAllColaborador().Select(c => new SelectListItem()
            { Text = c.Nome, Value = c.Nome }).ToList();

            Preventiva preventiva = this.preventiva.GetPreventiva(id);

            if (preventiva == null)
            {
                return NotFound();
            }
            return View(preventiva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Preventiva preventiva)
        {
            if (id != preventiva.PreventivaID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                this.preventiva.UpdatePreventiva(preventiva);
                return RedirectToAction("Details", "Preventiva", new { id = preventiva.PreventivaID });
            }
            return View(preventiva);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Cliente cliente = this.preventiva.GetCliente(id);
            Preventiva preventiva = new Preventiva();
            //preventiva.Cliente = cliente;
            preventiva = this.preventiva.GetPreventiva(id);

            if (preventiva == null)
            {
                return NotFound();
            }
            return View(preventiva);
        }

        public IActionResult RelatorioPreventiva()
        {
            string coluna = null;
            string nome = null;
            string tipoCliente = "CC";

            ViewBag.Mes = DateTime.Now.ToString("MMMM").ToUpper();
            ViewBag.Ano = DateTime.Now.ToString("yyyy");

            List<Cliente> listCliente = new List<Cliente>();
            listCliente = this.preventiva.GetClientes(coluna, nome, tipoCliente).ToList();
            

            return View(listCliente);
        }

        public IActionResult GerarRelatorio()
        {
            string coluna = null;
            string nome = null;
            string tipoCliente = "CC";
            List<Cliente> listCliente = new List<Cliente>();
            listCliente = this.preventiva.GetClientes(coluna, nome, tipoCliente).ToList();
                
            foreach (var cliente in listCliente)
            {
               string contrato = cliente.Nome;
               string cnpj = cliente.CPF;
               string logradouro = cliente.Endereco.Logradouro;
               string bairro = cliente.Endereco.Bairro;
               string cidade = cliente.Endereco.Cidade;

                var data = new
                {
                    mes = DateTime.Now.ToString("MMMM").ToUpper(),
                    ano = DateTime.Now.ToString("yyyy"),
                    nome = contrato,
                    cnpj = cnpj,
                    logradouro = logradouro,
                    bairro = bairro,
                    cidade = cidade
                };

                string arquivo = "preventiva.docx";
                Arquivo ArquivoCaminho = this.preventiva.GetCaminhoArquivo(arquivo);

                ComponentInfo.SetLicense("FREE-LIMITED-KEY");

                string caminho = ArquivoCaminho.Caminho.Replace("\"","");
                
                string procurar = caminho + arquivo;

                var document = DocumentModel.Load(procurar);
                
                document.MailMerge.Execute(data);

                arquivo = contrato + ".pdf";

                document.Save(caminho + arquivo);
            }
            return RedirectToAction("RelatorioPreventiva");
        }    

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Preventiva preventiva = this.preventiva.GetPreventiva(id);

            if (preventiva == null)
            {
                return NotFound();
            }
            return View(preventiva);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            Preventiva preventiva = this.preventiva.GetPreventiva(id);

            this.preventiva.DeletePreventiva(id);
            return RedirectToAction("Details", "Contrato", new { id = preventiva.Cliente.ClienteID });
        }
    }
}
