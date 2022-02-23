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

            using(SqlConnection con = new SqlConnection(connectionString))
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

        public Contato GetClienteID(int? id)
        {

            string connectionString = Conexao();

            Contato contato = new Contato();

            Cliente cliente = new Cliente();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM Pessoa WHERE PessoaID = {id}";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente.ClienteID = Convert.ToInt32(reader["PessoaID"]);

                    contato.Cliente = cliente;

                }
                con.Close();
            }

            return contato;
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

            using(SqlConnection con = new SqlConnection(connectionString))
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
                    endereco.Bairro = reader["Bairro"].ToString();
                    endereco.Complemento = reader["Complemento"].ToString();
                    endereco.Cidade = reader["Cidade"].ToString();
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

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM PESSOA WHERE PessoaID= {id}";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    //cliente.ClienteID = Convert.ToInt32(reader["PessoaID"]);
                   // cliente.Nome = reader["Nome"].ToString();

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
                string comandoSQL = $"INSERT INTO PESSOA (Nome) Values()";
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
                string comandoSQL = "UPDATE Pessoa SET Nome = @Nome WHERE PessoaID = @Id";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                //cmd.Parameters.AddWithValue("@Id", cliente.ClienteID);
                //cmd.Parameters.AddWithValue("@Nome", cliente.Nome);

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
                string comandoSQL = "DELETE FROM PESSOA WHERE PessoaID = @Id";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", id);

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
                SqlCommand cmd = new SqlCommand("SELECT * FROM Equipamento WHERE PessoaID = " + id, con);
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
        #endregion
    }
}
