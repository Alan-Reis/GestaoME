using System.Collections.Generic;

namespace ClickServ2022.Models
{
    public class Cliente 
    {
        public int ClienteID { get; set; }
        public string Nome { get; set; }

        public IEnumerable<Contato> Contatos;
        public IEnumerable<Endereco> Enderecos;
        public IEnumerable<Equipamento> Equipamentos;

    }
}
