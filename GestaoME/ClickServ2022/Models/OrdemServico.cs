using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ2022.Models
{
    public class OrdemServico
    {
        [Display(Name ="O.S")]
        public int OrdemServicoID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public string Valor { get; set; }
        public string Defeito { get; set; }
        [Display(Name ="Relatório")]
        public string Relatorio { get; set; }
        public Equipamento Equipamento { get; set; }
        public string Colaborador { get; set; }
    }
}
