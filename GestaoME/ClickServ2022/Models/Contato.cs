namespace ClickServ2022.Models
{
    public class Contato
    {
        public int ContatoID { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public Cliente Cliente { get; set; }
    }
}
