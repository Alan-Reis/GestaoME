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
        public string Observacao { get; set; }
        public Enum Status { get; set; } //Ativo - Cancelado - Aguardando Aprovação - Agardando Peça
        public Enum Tipo { get; set; } //Novo - Retorno - Garantia
        public Equipamento equipamentoID { get; set; }
        public Colaborador colaboradorID { get; set; }

    }
}
