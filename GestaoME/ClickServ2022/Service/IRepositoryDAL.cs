using ClickServ2022.Models;
using System.Collections.Generic;

namespace ClickServ2022.Service
{
    public interface IRepositoryDAL
    {
        IEnumerable<Pessoa> GetAllClientes();
        //void AddCliente(Pessoa cliente);
        //void UpdateCliente(Pessoa cliente);
        //Pessoa GetCliente(int? id);
        //void DeleteCliente(int? id);

    }
}
