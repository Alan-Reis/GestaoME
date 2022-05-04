using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClickServ2022.Models
{
    public class Cliente 
    {
        public int ClienteID { get; set; }
        //[Required(ErrorMessage = "Obrigatório preenchimento")]
        public string Nome { get; set; }
        public string CPF { get; set; }
        public Contato Contato { get; set; }
        public Endereco Endereco { get; set; }
        public Equipamento Equipamento { get; set; }

        public IEnumerable<Contato> Contatos;
        public IEnumerable<Endereco> Enderecos;
        public IEnumerable<Equipamento> Equipamentos;

    }
}
