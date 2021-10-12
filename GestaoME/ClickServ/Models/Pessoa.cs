using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ.Models
{
    public class Pessoa
    {
        public int PessoaID { get; set; }
        public string Nome { get; set; }
       
        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<Contato> Contatos { get; set; }
        public ICollection <Equipamento> Equipamentos { get; set; }
    }
}
