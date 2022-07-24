using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClickServ2022.Models
{
    public class Endereco
    {
        public int EnderecoID { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        [Display(Name = "UF")]
        public string Uf { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        public Cliente Cliente { get; set; }
        public Equipamento Equipamento { get; set; }

        public IEnumerable<Equipamento> Equipamentos;

        public Cliente Contrato { get; set; }
    }
}
