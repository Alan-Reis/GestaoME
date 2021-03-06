using ClickServ2022.Models;
using System.Collections.Generic;

namespace ClickServ2022.Service
{
    public interface IRepositoryDAL
    {
        #region GestaoME
    
        #endregion

        #region Cliente
        IEnumerable<Cliente> GetAllClientes(string coluna, string nome);
        void AddCliente(Cliente cliente);
        void AddDados(Cliente cliente);
        void UpdateCliente(Cliente cliente);
        Cliente GetCliente(int? id);
        void DeleteCliente(int? id);
        #endregion

        #region Contato
        IEnumerable<Contato> GetAllContatos(int? id);
        void AddContato(Contato contato);
        void UpdateContato(Contato contato);
        Contato GetContato(int? id);
        void DeleteContato(int? id);
        #endregion

        #region Endereço
        IEnumerable<Endereco> GetAllEnderecos(int? id);
        void AddEndereco(Endereco endereco);
        void UpdateEndereco(Endereco endereco);
        Endereco GetEndereco(int? id, string view);
        void DeleteEndereco(int? id);
        #endregion

        #region Equipamento
        IEnumerable<Equipamento> GetAllEquipamentos(int? id, string view);
        IEnumerable<Equipamento> GetAllEquipamentosCliente(int? id);
        void AddEquipamento(Equipamento equipamento);
        void UpdateEquipamento(Equipamento equipamento);
        Equipamento GetEquipamento(int? id, string view);
        void DeleteEquipamento(int? id);
        #endregion

        #region Atendimento
        //IEnumerable<Atendimento>GetAllAtedimentos(string data);
        void AddAtendimento(Atendimento atendimento);
        void UpdateAtendimento(Atendimento atendimento);
        Atendimento GetAtendimento(int? id);
        void DeleteAtendimento(int? id);
        #endregion

        #region Colaborador
        IEnumerable<Colaborador> GetAllColaborador();
        #endregion

        #region Ordem de Serviço
        void AddOrdemServico(OrdemServico ordemservico);
        void AddOrdemServicoDuplicado(OrdemServico ordemServico);
        IEnumerable<OrdemServico> GetAllOrdemServico(int? id, string view);
        OrdemServico GetOrdemServico(int? os);
        void UpdateOrdemServico(OrdemServico ordemServico);
        void DeleteOrdemServico(int? id);
        #endregion

        #region Auxiliares
        List<TipoEquipamento> GetAllTipoEquipamento();
        List<Fabricante> GetAllFabricante(string equipamento);
        List<Modelo> GetAllModelo(string model);
        List<OrdemServico> GetAllCategoria();
        List<Atendimento> GetPeriodo();
        IEnumerable<Evento> GetAllEventos();
        #endregion

        #region Relatórios
        IEnumerable<RelatorioAtendimento> RelatorioAtendimento(string data);
        #endregion
    }
}
