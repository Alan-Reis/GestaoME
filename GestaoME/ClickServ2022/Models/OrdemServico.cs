using System;
using System.ComponentModel.DataAnnotations;

namespace ClickServ2022.Models
{
    public class OrdemServico
    {
        [Display(Name = "O.S")]
        public int OrdemServicoID { get; set; }
        public string Duplicado { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public string Valor { get; set; }
        public string Categoria { get; set; }
        public string Defeito { get; set; }
        [Display(Name = "Relatório")]
        public string Relatorio { get; set; }
        public Equipamento Equipamento { get; set; }
        public string Colaborador { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }
}
