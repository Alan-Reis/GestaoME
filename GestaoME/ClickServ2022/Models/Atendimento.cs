using System;
using System.ComponentModel.DataAnnotations;

namespace ClickServ2022.Models
{
    public class Atendimento
    {
        public int AtendimentoID { get; set; }
        public string Defeito { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public string Periodo { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        public Equipamento Equipamento { get; set; }
        public string Colaborador { get; set; }

    }
}
