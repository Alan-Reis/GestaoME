using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ2022.Models
{
    public class Contato
    {
        public int ContatoID { get; set; }
        public string Telefone { get; set; }
        //[Required(ErrorMessage = "Obrigatório preenchimento")]
        public string Celular { get; set; }
        public string Email { get; set; }
        public Cliente Cliente { get; set; }
    }
}
