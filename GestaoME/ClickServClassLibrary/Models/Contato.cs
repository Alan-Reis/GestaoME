using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServClassLibrary
{
    public class Contato
    {
        public int ContatoID { get; set; }
        public int PessoaID { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
