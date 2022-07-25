using System;
using System.ComponentModel.DataAnnotations;

namespace ClickServ2022.Models
{
    public class Preventiva
    {
        public int PreventivaID { get; set; }
        [Display(Name = "Mês")]
        public string Mes { get; set; }
        public string Ano { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        [Display(Name = "Relatório")]
        public string Relatorio { get; set; }
        [Display(Name = "Técnico")]
        public string Tecnico { get; set; }
        public Cliente Cliente { get; set; }
    }
}
