using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClickServ2022.Models
{
    public class Sistema
    {
        public int SistemaID { get; set; }
        [Display(Name = "Sistema")]
        public string NomeSistema { get; set; }
        public Equipamento Equipamento { get; set; }
        public Cliente Cliente { get; set; }

        public IEnumerable<Equipamento> Equipamentos;
    }
}
