using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ2022.Models
{
    public class OrdemServico
    {
        public int OrdemServicoID { get; set; }
        public DateTime Data { get; set; }
        public string Valor { get; set; }
        public string Defeito { get; set; }
        public string Relatorio { get; set; }
        public Cliente clienteID { get; set; }
        public Equipamento equipamentoID { get; set; }
        public Colaborador colaboradorID { get; set; }
    }
}
