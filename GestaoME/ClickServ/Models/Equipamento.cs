using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ.Models
{
    public enum Tipo
    {
        AquecedorGás, AquecedorElétrico, TrocadorDeCalor, Bomba, Reservatório 
    }
    public class Equipamento
    {
        public int EquipamentoID { get; set; }
        public int PessoaID { get; set; }
        public Tipo? Tipo { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public string NSerie { get; set; }
        public Pessoa Pessoa { get; set; }
        public ICollection<Atendimento> Atendimentos { get; set; }
        public ICollection<OrdemServico> OrdemServicos { get; set; }


    }
}
