﻿using ClickServ2022.Models;
using ClickServ2022.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using PagedList;

namespace ClickServ2022.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IRepositoryDAL cliente;
        public ClienteController(IRepositoryDAL _cliente)
        {
            cliente = _cliente;

        }

        public IActionResult Index(int? pagina, Cliente cliente)
        {
            List<Cliente> listCliente = new List<Cliente>();

            //paginação
            int paginaTamanho = 4;
            int paginaNumero = (pagina ?? 1);
            //fim

            if (cliente.Nome == null)
            {
                //string criada para que se possa obter todos os clientes sem que possa passar um nome como parametro
                string nomeNull = null;
                string colunaNull = null;
                listCliente = this.cliente.GetAllClientes(colunaNull, nomeNull).ToList();

                return View(listCliente.ToPagedList(paginaNumero, paginaTamanho));
            }

            return View();

            //return View(listCliente.ToPagedList(paginaNumero, paginaTamanho));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int? pagina, string coluna, string nome)
        {
            if (coluna == "Condomínio")
            {
                coluna = "Complemento";
            }

            List<Cliente> listCliente = new List<Cliente>();
            listCliente = cliente.GetAllClientes(coluna, nome).ToList();

            //se não tiver o cliente, vai para adicionar            
            if(listCliente.Count == 0)
            {
                ViewBag.Erro = "Cliente inexistente, deseja criar? ";
            }
            

            //paginação
            int paginaTamanho = 4;
            int paginaNumero = (pagina ?? 1);
            //fim

            //Condicional criada para trazer todos os resultado em uma única página
            if (listCliente.Count > 4)
            {
                //paginação
                paginaTamanho = listCliente.Count;
                paginaNumero = (pagina ?? 1);
                //fim
            }

            return View(listCliente.ToPagedList(paginaNumero, paginaTamanho));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente cliente = this.cliente.GetCliente(id);

            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Cliente cliente)
        {

            if (ModelState.IsValid)
            {
                this.cliente.AddCliente(cliente);
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Cliente cliente = this.cliente.GetCliente(id);

            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Cliente cliente)
        {
            if (id != cliente.ClienteID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                this.cliente.UpdateCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente cliente = this.cliente.GetCliente(id);

            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            cliente.DeleteCliente(id);
            return RedirectToAction("Index");
        }

        public IActionResult AddDados(int? id)
        {
            Cliente cliente = this.cliente.GetCliente(id);

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDados([Bind] Cliente cliente)
        {

            if (ModelState.IsValid)
            {
                this.cliente.AddDados(cliente);
                return RedirectToAction("Index");
            }

            return View(cliente);
        }
    }
}
