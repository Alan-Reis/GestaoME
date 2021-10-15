using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServClassLibrary
{
    public class Colaborador
    {
        public int ColaboradorID { get; set; }
        public string Nome { get; set; }
        public string Funcao { get; set; }

        public ICollection<Atendimento> Atendimentos { get; set; }
        public ICollection<OrdemServico> OrdemServicos { get; set; }

    }
}
