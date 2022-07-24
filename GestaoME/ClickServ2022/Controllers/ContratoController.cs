using ClickServ2022.Models;
using ClickServ2022.Service;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClickServ2022.Controllers
{
    public class ContratoController : Controller
    {
        private readonly IRepositoryDAL cliente;
        public ContratoController(IRepositoryDAL _cliente)
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
                //CA = Cliente Contrato
                string tipoCliente = "CC";
                listCliente = this.cliente.GetClientes(colunaNull, nomeNull, tipoCliente).ToList();

                return View(listCliente.ToPagedList(paginaNumero, paginaTamanho));
            }

            return View();

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
            //CC = Cliente Contrato
            string tipoCliente = "CC";
            listCliente = cliente.GetClientes(coluna, nome, tipoCliente).ToList();

            //se não tiver o cliente, vai para adicionar            
            if (listCliente.Count == 0)
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
            string sistema = "sistema";
            cliente.Endereco = this.cliente.GetEndereco(id, sistema);
            cliente.Sistemas = this.cliente.GetSistemas(id);
            cliente.ContatosAuxiliar = this.cliente.GetContatosAuxiliar(id);

            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        public IActionResult Create(Endereco endereco)
        {
            if (endereco.Logradouro != null)
            {
                Cliente cliente = new Cliente();
                cliente.Endereco = endereco;

                return View(cliente);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Cliente cliente, string logradouro, string bairro, string cidade, string uf)
        {
            //Função para setar os valores do CEP
            cliente.Endereco.Logradouro = logradouro;
            cliente.Endereco.Bairro = bairro;
            cliente.Endereco.Cidade = cidade;
            cliente.Endereco.Uf = uf;

            if (ModelState.IsValid)
            {
                cliente.TipoCliente = "CC";
                this.cliente.AddCliente(cliente);

                //Pega o último ClienteID inserido no banco de dados
                cliente.ClienteID = Convert.ToInt32(this.cliente.GetClienteLast());
                this.cliente.AddEndereco(cliente);
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] Cliente cliente, Endereco endereco)
        {

            if (ModelState.IsValid)
            {
                this.cliente.UpdateCliente(cliente);
                int id = cliente.ClienteID;
                return RedirectToAction("Details", "Contrato", new { id });
            }
            return View(cliente);
        }
    }
}
