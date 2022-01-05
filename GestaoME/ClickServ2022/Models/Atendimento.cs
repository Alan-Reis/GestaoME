using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ2022.Models
{
    public class Atendimento
    {
        public int AtendimentoID { get; set; }
        public string  Defeito { get; set; }
        public DateTime Data { get; set; }
        public string Periodo { get; set; }
        public Enum Observacao { get; set; }
        public Enum Status { get; set; }
        public Equipamento Equipamento { get; set; }
        public Colaborador Colaborador { get; set; }

    }
}
