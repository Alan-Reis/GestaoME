using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ.Models
{
    public class OrdemServico
    {
        //Esse atributo permite inserir a PK para o curso em vez de fazer com que o banco de dados o gere.
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrdemServicoID { get; set; }
        public int EquipamentoID { get; set; }
        public int ColaboradorID { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Defeito { get; set; }
        public string Relatorio { get; set; }

        public Equipamento Equipamento { get; set; }
        public Colaborador Colaborador { get; set; }
    }
}
