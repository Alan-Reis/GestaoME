using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ2022.Models
{
    public class Equipamento
    {
        public int EquipamentoID { get; set; }
        //[Required(ErrorMessage = "Obrigatório preenchimento")]
        public string Tipo { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        [Display(Name="Nº série")]
        public string NSerie { get; set; }
        [Display(Name ="Tipo de gás")]
        public string Gas { get; set; }
        public Cliente Cliente { get; set; }
        public Endereco Endereco { get; set; }
        public OrdemServico OrdemServico { get; set; }

        public IEnumerable<OrdemServico> OrdemServicos;
    }
}
