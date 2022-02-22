using ClickServ2022.Models;
using System.Collections.Generic;

namespace ClickServ2022.Service
{
    public interface IRepositoryDAL
    {
        #region Cliente
        IEnumerable<Cliente> GetAllClientes();
        void AddCliente(Cliente cliente);
        void UpdateCliente(Cliente cliente);
        Cliente GetCliente(int? id);
        void DeleteCliente(int? id);
        #endregion

        #region Contato
        IEnumerable<Contato> GetAllContatos(int? id);
        #endregion

        #region Endereço
        IEnumerable<Endereco> GetAllEnderecos(int? id);
        void AddEndereco(Endereco endereco);
        void UpdateEndereco(Endereco endereco);
        Endereco GetEndereco(int? id);
        void DeleteEndereco(int? id);
        #endregion

        #region Equipamento
        IEnumerable<Equipamento> GetAllEquipamentos(int? id);
        #endregion

        
    }
}
