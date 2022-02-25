using ClickServ2022.Models;
using ClickServ2022.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ClickServ2022.Repository
{
    public class RepositoryDAL : IRepositoryDAL
    {
        #region Responsável pela conexão com o banco de dados
        private readonly IConfiguration Configuration;

        public RepositoryDAL(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected string Conexao()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            return connectionString;
        }
        #endregion

        #region Cliente
        public IEnumerable<Cliente> GetAllClientes()
        {
            string connectionString = Conexao();

            List<Cliente> listPessoa = new List<Cliente>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM PESSOA", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.ClienteID = Convert.ToInt32(reader["PessoaID"]);
                    cliente.Nome = reader["Nome"].ToString();
                    listPessoa.Add(cliente);
                }
                con.Close();
            }

            return listPessoa;
        }

        public Cliente GetCliente(int? id)
        { 
            string connectionString = Conexao();
            Cliente cliente = new Cliente();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM PESSOA WHERE PessoaID= {id}";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente.ClienteID = Convert.ToInt32(reader["PessoaID"]);
                    cliente.Nome = reader["Nome"].ToString();
                    
                    cliente.Enderecos = GetAllEnderecos(id);
                    cliente.Equipamentos = GetAllEquipamentos(id);
                    cliente.Contatos = GetAllContatos(id);
                }
                con.Close();
            }
            return cliente;
        }

        public void AddCliente(Cliente cliente)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"INSERT INTO PESSOA (Nome) Values('{cliente.Nome}')";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateCliente(Cliente cliente)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"UPDATE Pessoa SET Nome = '{cliente.Nome}' WHERE PessoaID = {cliente.ClienteID}";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DeleteCliente(int? id)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"DELETE FROM PESSOA WHERE PessoaID = {id}";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        #endregion

        #region Contato
        public IEnumerable<Contato> GetAllContatos(int? id)
        {
            string connectionString = Conexao();

            List<Contato> listContato = new List<Contato>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Contato WHERE PessoaID = " + id, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Contato contato = new Contato();
                    contato.ContatoID = Convert.ToInt32(reader["ContatoID"]);
                    contato.Celular = reader["Celular"].ToString();
                    contato.Telefone = reader["Telefone"].ToString();
                    contato.Email = reader["Email"].ToString();

                    listContato.Add(contato);
                }
                con.Close();
            }

            return listContato;
        }

        public Contato GetContato(int? id)
        {

            string connectionString = Conexao();

            Contato contato = new Contato();

            Cliente cliente = new Cliente();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM Contato WHERE ContatoID = {id}";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente.ClienteID = Convert.ToInt32(reader["PessoaID"]);
                    contato.Cliente = cliente;

                    contato.ContatoID = Convert.ToInt32(reader["ContatoID"]);
                    contato.Celular = reader["Celular"].ToString();
                    contato.Telefone = reader["Telefone"].ToString();
                    contato.Email = reader["Email"].ToString();
                }
                con.Close();
            }

            return contato;
        }

        public void UpdateContato(Contato contato)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string comandoSQL = $"UPDATE Contato SET " +
                                    $"Celular   =   '{contato.Celular}', " +
                                    $"Telefone  =   '{contato.Telefone}', " +
                                    $"Email     =   '{contato.Email}'" +
                                    " WHERE ContatoID = " + contato.ContatoID;

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void AddContato(Contato contato)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"INSERT INTO Contato (PessoaID, Celular, Telefone, Email) " +
                                    $"Values({contato.Cliente.ClienteID}, " +
                                    $"'{contato.Celular}', " +
                                    $"'{contato.Telefone}', " +
                                    $"'{contato.Email}')";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteContato(int? id)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"DELETE FROM Contato WHERE ContatoID = {id}";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        #endregion

        #region Endereco
        public IEnumerable<Endereco> GetAllEnderecos(int? id)
        {
            string connectionString = Conexao();
            List<Endereco> listEndereco = new List<Endereco>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Endereco WHERE PessoaID = {id} ", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Endereco endereco = new Endereco();
                    endereco.EnderecoID = Convert.ToInt32(reader["EnderecoID"]);
                    endereco.Logradouro = reader["Logradouro"].ToString();
                    endereco.Complemento = reader["Complemento"].ToString();
                    endereco.Bairro = reader["Bairro"].ToString();
                    endereco.Cidade = reader["Cidade"].ToString();
                    endereco.Estado = reader["Estado"].ToString();
                    endereco.Observacao = reader["Observacao"].ToString();
                    listEndereco.Add(endereco);
                }
                con.Close();
            }
            return listEndereco;
        }

        public Endereco GetEndereco(int? id)
        {

            string connectionString = Conexao();

            Endereco endereco = new Endereco();
            Cliente cliente = new Cliente();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM Endereco WHERE EnderecoID = {id}";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente.ClienteID = Convert.ToInt32(reader["PessoaID"]);
                    endereco.Cliente = cliente;

                    endereco.EnderecoID = Convert.ToInt32(reader["EnderecoID"]);
                    endereco.Logradouro = reader["Logradouro"].ToString();
                    endereco.Complemento = reader["Complemento"].ToString();
                    endereco.Bairro = reader["Bairro"].ToString();
                    endereco.Cidade = reader["Cidade"].ToString();
                    endereco.Estado = reader["Estado"].ToString();
                    endereco.Observacao = reader["Observacao"].ToString();
                }
                con.Close();
            }

            return endereco;
        }

        public void AddEndereco(Endereco endereco)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"INSERT INTO Endereco " +
                                    $"Values({endereco.Cliente.ClienteID}, " +
                                    $"'{endereco.Logradouro}', " +
                                    $"'{endereco.Complemento}', " +
                                    $"'{endereco.Bairro}', " +
                                    $"'{endereco.Cidade}', " +
                                    $"'{endereco.Estado}', " +
                                    $"'{endereco.Observacao}')";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateEndereco(Endereco endereco)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"UPDATE Endereco SET " +
                                    $"Logradouro = '{endereco.Logradouro}', " +
                                    $"Complemento = '{endereco.Complemento}', " +
                                    $"Bairro = '{endereco.Bairro}', " +
                                    $"Cidade = '{endereco.Cidade}', " +
                                    $"Estado = '{endereco.Estado}', " +
                                    $"Observacao = '{endereco.Observacao}' " +
                                    $"WHERE EnderecoID = '{endereco.EnderecoID}'";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DeleteEndereco(int? id)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"DELETE FROM Endereco WHERE EnderecoID = {id}";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        #endregion

        #region Equipamento
        public IEnumerable<Equipamento> GetAllEquipamentos(int? id)
        {
            string connectionString = Conexao();
            List<Equipamento> listEquipamento = new List<Equipamento>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Equipamento WHERE PessoaID = {id}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Equipamento equipamento = new Equipamento();
                    equipamento.EquipamentoID = Convert.ToInt32(reader["EquipamentoID"]);
                    equipamento.Tipo = reader["Tipo"].ToString();
                    equipamento.Fabricante = reader["Fabricante"].ToString();
                    equipamento.Modelo = reader["Modelo"].ToString();
                    equipamento.NSerie = reader["NSerie"].ToString();
                    listEquipamento.Add(equipamento);
                }
                con.Close();
            }

            return listEquipamento;
        }

        public Equipamento GetEquipamento(int? id)
        {

            string connectionString = Conexao();

            Equipamento equipamento = new Equipamento();
            Cliente cliente = new Cliente();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM Equipamento WHERE EquipamentoID = {id}";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente.ClienteID = Convert.ToInt32(reader["PessoaID"]);
                    equipamento.Cliente = cliente;

                    equipamento.EquipamentoID = Convert.ToInt32(reader["EquipamentoID"]);
                    equipamento.Tipo = reader["Tipo"].ToString();
                    equipamento.Fabricante = reader["Fabricante"].ToString();
                    equipamento.Modelo = reader["Modelo"].ToString();
                    equipamento.NSerie = reader["NSerie"].ToString();
                }
                con.Close();
            }

            return equipamento;
        }

        public void AddEquipamento(Equipamento equipamento)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"INSERT INTO Equipamento " +
                                    $"Values({equipamento.Cliente.ClienteID}, " +
                                    $"'{equipamento.Tipo}', " +
                                    $"'{equipamento.Fabricante}', " +
                                    $"'{equipamento.Modelo}', " +
                                    $"'{equipamento.NSerie}')";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateEquipamento(Equipamento equipamento)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"UPDATE Equipamento SET " +
                                    $"Tipo =       '{equipamento.Tipo}', " +
                                    $"Fabricante = '{equipamento.Fabricante}', " +
                                    $"Modelo =     '{equipamento.Modelo}', " +
                                    $"NSerie =     '{equipamento.NSerie}' " +
                                    $"WHERE EquipamentoID = '{equipamento.EquipamentoID}'";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DeleteEquipamento(int? id)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"DELETE FROM Equipamento WHERE EquipamentoID = {id}";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        #endregion
    }
}
