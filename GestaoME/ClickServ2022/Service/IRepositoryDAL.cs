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
        //Cliente GetCliente(int? id, string view);
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
        void AddEquipamento(Equipamento equipamento);
        void UpdateEquipamento(Equipamento equipamento);
        Equipamento GetEquipamento(int? id, string view);
        void DeleteEquipamento(int? id);
        #endregion

        #region Atendimento
        //IEnumerable<Atendimento>GetAllAtedimentos(string data);
        void AddAtendimento(Atendimento atendimento);
        //void UpdateAtendimento(Atendimento atendimento);
        //Atendimento GetAtendimento(int? id);
        #endregion

        #region Colaborador
        IEnumerable<Colaborador> GetAllColaborador();
        #endregion

        #region Ordem de Serviço
        void AddOrdemServico(OrdemServico ordemservico);
        IEnumerable<OrdemServico> GetAllOrdemServico(int? id);
        OrdemServico GetOrdemServico(int? os);

        #endregion

        #region Auxiliares
        List<TipoEquipamento> GetAllTipoEquipamento();
        List<Fabricante> GetAllFabricante(string equipamento);
        List<Modelo> GetAllModelo(string model);
        IEnumerable<Evento> GetAllEventos();
        #endregion

        #region Relatórios
        IEnumerable<RelatorioAtendimento> RelatorioAtendimento(string data);
        #endregion
    }
}
