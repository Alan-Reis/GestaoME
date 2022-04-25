using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ2022.Models
{
    public class Equipamento
    {
        public int EquipamentoID { get; set; }
        public string Tipo { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public string NSerie { get; set; }
        public Cliente Cliente { get; set; }
        public Endereco Endereco { get; set; }
    }
}
