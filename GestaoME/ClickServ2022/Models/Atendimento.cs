using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ2022.Models
{
    public class Atendimento
    {
        public int AtendimentoID { get; set; }
        public string Tipo { get; set; } //Novo - Retorno - Garantia
        public string  Defeito { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        public string Periodo { get; set; }
        public string Status { get; set; } //Ativo - Cancelado - Aguardando Aprovação - Aguardando Peça - Concluído
        [Display(Name ="Observação")]
        public string Observacao { get; set; }
        public Equipamento Equipamento { get; set; }
        public Colaborador Colaborador { get; set; }

    }
}
