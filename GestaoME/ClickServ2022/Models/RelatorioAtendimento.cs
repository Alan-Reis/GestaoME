namespace ClickServ2022.Models
{
    public class RelatorioAtendimento
    {
        public Cliente Cliente { get; set; }
        public Contato Contato { get; set; }
        public Endereco Endereco { get; set; }
        public Equipamento Equipamento { get; set; }
        public Atendimento Atendimento { get; set; }
    }
}
