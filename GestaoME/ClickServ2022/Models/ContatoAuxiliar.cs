using System.ComponentModel.DataAnnotations;

namespace ClickServ2022.Models
{
    public class ContatoAuxiliar : Contato
    {
        public string Nome { get; set; }
        [Display(Name ="Atribuição")]
        public string Atribuicao { get; set; }
    }
}
