using ClickServ2022.Models;
using System.Collections.Generic;

namespace ClickServ2022.Service
{
    public interface IRepositoryDAL
    {
        #region Cliente
        IEnumerable<Cliente> GetClientes(string coluna, string nome, string tipoCliente);
        void AddCliente(Cliente cliente);
        void UpdateCliente(Cliente cliente);
        Cliente GetCliente(int? id);
        int GetClienteLast();
        #endregion

        #region Contato
        IEnumerable<Contato> GetContatos(int? id);
        void AddContato(Cliente cliente);
        void UpdateContato(Contato contato);
        Contato GetContato(int? id);
        void DeleteContato(int? id);
        #endregion

        #region ContatoAuxiliar
        IEnumerable<ContatoAuxiliar> GetContatosAuxiliar(int? id);
        void AddContatoAuxiliar(ContatoAuxiliar contato);
        void UpdateContatoAuxiliar(ContatoAuxiliar contato);
        ContatoAuxiliar GetContatoAuxiliar(int? id);
        void DeleteContatoAuxiliar(int? id);
        #endregion

        #region Endereço
        IEnumerable<Endereco> GetEnderecos(int? id);
        Endereco GetEndereco(int? id, string sistema);
        void AddEndereco(Cliente cliente);
        void UpdateEndereco(Endereco endereco);
        int GetEnderecoLast();
        #endregion

        #region Equipamento
        IEnumerable<Equipamento> GetEquipamentos(int? id, string sistemaEndereco);
        Equipamento GetEquipamento(int? id);
        IEnumerable<Equipamento> GetAllEquipamentosCliente(int? id);
        void AddEquipamento(Cliente cliente);
        void UpdateEquipamento(Equipamento equipamento);
        #endregion

        #region Atendimento
        void AddAtendimento(Atendimento atendimento);
        void UpdateAtendimento(Atendimento atendimento);
        Atendimento GetAtendimento(int? id);
        void DeleteAtendimento(int? id);
        #endregion

        #region Colaborador
        IEnumerable<Colaborador> GetAllColaborador();
        #endregion

        #region Ordem de Serviço
        OrdemServico GetOrdemServico(int? os);
        IEnumerable<OrdemServico> GetAllOrdemServico(int? id, string view);
        void AddOrdemServico(OrdemServico ordemservico, string duplicado);
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

        #region Sistema
        IEnumerable<Sistema> GetSistemas(int? id);
        Sistema GetSistema(int? id);
        void AddSistema(Sistema sistema);
        void UpdateSistema(Sistema sistema);
        #endregion
    }
}
