﻿using System.Collections.Generic;

namespace ClickServ2022.Models
{
    public class Contrato
    {
        public int ClienteID { get; set; }
        public string TipoCliente { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public Contato Contato { get; set; }
        public ContatoAuxiliar ContatoAuxiliar { get; set; }
        public Endereco Endereco { get; set; }
        public Equipamento Equipamento { get; set; }
        public Sistema Sistema { get; set; }
        public Preventiva Preventiva { get; set; }

        public IEnumerable<Contato> Contatos;
        public IEnumerable<ContatoAuxiliar> ContatosAuxiliar;
        public IEnumerable<Endereco> Enderecos;
        public IEnumerable<Equipamento> Equipamentos;
        public IEnumerable<Sistema> Sistemas;
        public IEnumerable<Preventiva> Preventivas;
    }
}