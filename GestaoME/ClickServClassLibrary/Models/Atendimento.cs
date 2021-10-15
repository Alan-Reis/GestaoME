using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServClassLibrary
{
    public enum Status
    {
        Concluido, Cancelado, Orçamento
    }

    public enum Periodo
    {
        Manhã, Tarde
    }
    public class Atendimento
    {
        public int AtendimentoID { get; set; }
        public int EquipamentoID { get; set; }
        public int ColaboradorID { get; set; }
        public string Defeito { get; set; }
        public DateTime Data { get; set; }
        public Periodo? Periodo { get; set; }
        public string Observacao { get; set; }
        public Status? Status { get; set; }
        
        //public bool Retorno { get; set; }
        public Equipamento Equipamento { get; set; }
        public Colaborador Colaborador { get; set; }
    }
}
