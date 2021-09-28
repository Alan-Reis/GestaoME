using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ.Models
{
    public class Condominio
    {
        public int CondominioID { get; set; }
        public int EnderecoID { get; set; }
        public string Nome { get; set; }
        public string Torre { get; set; }
        public string Apartamento { get; set; }

        public Endereco Endereco { get; set; }
    }
}
