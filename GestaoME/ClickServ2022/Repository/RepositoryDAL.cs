using ClickServ2022.Models;
using ClickServ2022.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

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

        protected string ConexaoGestaoME()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnectionGestaoME");
            return connectionString;
        }
        #endregion

        #region GestaoME

        #endregion

        #region Cliente
        public IEnumerable<Cliente> GetAllClientes(string coluna, string nome)
        {

            string connectionString = Conexao();

            List<Cliente> listPessoa = new List<Cliente>();

            string stringQuery;
            //IF realizado para consulta por nome ou condomínio
            if (nome != null)
            {
                stringQuery = $"SELECT * FROM tbl_Cliente C " +
                              $"INNER JOIN tbl_Endereco E " +
                              $"ON C.ClienteID = E.ClienteID " +
                              $"WHERE {coluna} LIKE '%{nome}%' ORDER BY Nome DESC";

            }
            else
            {
                stringQuery = $"SELECT TOP(12) * FROM tbl_Cliente C " +
                              $"INNER JOIN tbl_Endereco E " +
                              $"ON C.ClienteID = E.ClienteID" +
                              $" ORDER BY C.ClienteID DESC";

            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(stringQuery, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    Endereco endereco = new Endereco();

                    cliente.ClienteID = Convert.ToInt32(reader["ClienteID"]);
                    cliente.Nome = reader["Nome"].ToString();

                    endereco.Logradouro = reader["Logradouro"].ToString();
                    endereco.Complemento = reader["Complemento"].ToString();

                    cliente.Endereco = endereco;

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
                string sqlQuery = $"SELECT * FROM tbl_Cliente WHERE ClienteID= {id}";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente.ClienteID = Convert.ToInt32(reader["ClienteID"]);
                    cliente.Nome = reader["Nome"].ToString();
                    cliente.CPF = reader["CPF"].ToString();

                    cliente.Enderecos = GetAllEnderecos(id);
                    string view = null;
                    cliente.Equipamentos = GetAllEquipamentos(id, view);
                    cliente.Contatos = GetAllContatos(id);
                }
                con.Close();
            }
            return cliente;
        }

        public Cliente GetClienteOrdemServico(int? id)
        {
            string connectionString = Conexao();
            Cliente cliente = new Cliente();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"SELECT ClienteID FROM tbl_OrdemServico OS" +
                                $" INNER JOIN tbl_Equipamento E" +
                                $" ON OS.EquipamentoID = E.EquipamentoID" +
                                $" WHERE OS.OrdemServicoID = {id}";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente.ClienteID = Convert.ToInt32(reader["ClienteID"]);
                    
                }
                con.Close();
            }
            return cliente;
        }

        public int GetClienteLast()
        {
            string connectionString = Conexao();
            int endereco = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = $" SELECT TOP 1 ClienteID FROM tbl_Cliente ORDER BY ClienteID DESC ";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                endereco = Convert.ToInt32(reader["ClienteID"]);
                con.Close();
            }
            return endereco;
        }

        public void AddCliente(Cliente cliente)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"INSERT INTO tbl_Cliente (Nome, CPF) Values('{cliente.Nome}','{cliente.CPF}')";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();

                //Pega o último ClienteID inserido no banco de dados
                cliente.ClienteID = GetClienteLast();
                AddDados(cliente);

                con.Close();
            }
        }

        public void AddDados(Cliente cliente)
        {

            string connectionString = Conexao();

            //Para cadastro completo de cliente. Quando o cadastro é feito para adicionar um novo
            //endereço e equipamento o Contato vem nulo e daria erro se não estivesse esse if.
            if (cliente.Contato != null)
            {
                cliente.Contato.Cliente = cliente;
                AddContato(cliente.Contato);
            }

            cliente.Endereco.Cliente = cliente;
            AddEndereco(cliente.Endereco);

            //Pega endereçoID para salvar na FK da tbl Equipamento
            cliente.Endereco.EnderecoID = GetEnderecoLast();

            cliente.Equipamento.Cliente = cliente;
            AddEquipamento(cliente.Equipamento);

        }

        public void UpdateCliente(Cliente cliente)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"UPDATE tbl_Cliente SET Nome = '{cliente.Nome}', CPF = '{cliente.CPF}' WHERE ClienteID = {cliente.ClienteID}";

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
                string delContato = $"DELETE FROM tbl_Contato WHERE ClienteID = {id}";
                string delEndereco = $"DELETE FROM tbl_Endereco WHERE ClienteID = {id}";
                string delEquipamento = $"DELETE FROM tbl_Equipamento WHERE ClienteID = {id}";
                string comandoSQL = $"DELETE FROM tbl_Cliente WHERE ClienteID = {id}";
                SqlCommand cmd = new SqlCommand(delContato + delEndereco + delEquipamento + comandoSQL, con);
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_Contato WHERE ClienteID = " + id, con);
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
                string sqlQuery = $"SELECT * FROM tbl_Contato WHERE ContatoID = {id}";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente.ClienteID = Convert.ToInt32(reader["ClienteID"]);
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

                string comandoSQL = $"UPDATE tbl_Contato SET " +
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
                string comandoSQL = $"INSERT INTO tbl_Contato (ClienteID, Celular, Telefone, Email) " +
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
                string comandoSQL = $"DELETE FROM tbl_Contato WHERE ContatoID = {id}";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        #endregion

        #region Contato Auxiliar

        public void AddContatoAuxiliar(ContatoAuxiliar contato)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"INSERT INTO tbl_Contato (ContatoAuxID, ClienteID, Nome, Celular, Telefone, Atribuicao) " +
                                    $"Values({contato.ContatoAuxID}, " +
                                    $"'{contato.Cliente.ClienteID}', " +
                                    $"'{contato.Nome}', " +
                                    $"'{contato.Celular}', " +
                                    $"'{contato.Telefone}', " +
                                    $"'{contato.Atribuicao}')";

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
                SqlCommand cmd = new SqlCommand($" SELECT * FROM tbl_Endereco E WHERE E.ClienteID = {id} ", con);

                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Endereco endereco = new Endereco();
                    Equipamento equipamento = new Equipamento();

                    endereco.EnderecoID = Convert.ToInt32(reader["EnderecoID"]);
                    endereco.Logradouro = reader["Logradouro"].ToString();
                    endereco.Complemento = reader["Complemento"].ToString();
                    endereco.Bairro = reader["Bairro"].ToString();
                    endereco.Cidade = reader["Cidade"].ToString();
                    endereco.Uf = reader["Uf"].ToString();
                    endereco.Observacao = reader["Observacao"].ToString();

                    listEndereco.Add(endereco);
                }


                con.Close();
            }
            return listEndereco;
        }

        public Endereco GetEndereco(int? id, string view)
        {

            string connectionString = Conexao();

            Endereco endereco = new Endereco();
            Cliente cliente = new Cliente();

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string sqlQuery;

                if (view == "Endereco")
                {
                    sqlQuery = $"SELECT * FROM tbl_Endereco WHERE ClienteID = {id}";
                }
                else
                {
                    sqlQuery = $"SELECT * FROM tbl_Endereco WHERE EnderecoID = {id}";
                }

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente.ClienteID = Convert.ToInt32(reader["ClienteID"]);
                    endereco.Cliente = cliente;

                    endereco.EnderecoID = Convert.ToInt32(reader["EnderecoID"]);
                    endereco.Logradouro = reader["Logradouro"].ToString();
                    endereco.Complemento = reader["Complemento"].ToString();
                    endereco.Bairro = reader["Bairro"].ToString();
                    endereco.Cidade = reader["Cidade"].ToString();
                    endereco.Uf = reader["Uf"].ToString();
                    endereco.Observacao = reader["Observacao"].ToString();

                    view = "Endereco";
                    endereco.Equipamentos = GetAllEquipamentos(id, view);
                }
                con.Close();
            }

            return endereco;
        }

        public int GetEnderecoLast()
        {
            string connectionString = Conexao();
            int endereco = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = $" SELECT TOP 1 EnderecoID FROM tbl_Endereco ORDER BY EnderecoID DESC ";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                endereco = Convert.ToInt32(reader["EnderecoID"]);
                con.Close();
            }
            return endereco;
        }

        public void AddEndereco(Endereco endereco)
        {
            string connectionString = Conexao();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"INSERT INTO tbl_Endereco " +
                                    $"Values({endereco.Cliente.ClienteID}, " +
                                    $"'{endereco.Logradouro}', " +
                                    $"'{endereco.Complemento}', " +
                                    $"'{endereco.Bairro}', " +
                                    $"'{endereco.Cidade}', " +
                                    $"'{endereco.Uf}', " +
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
                string comandoSQL = $"UPDATE tbl_Endereco SET " +
                                    $"Logradouro = '{endereco.Logradouro}', " +
                                    $"Complemento = '{endereco.Complemento}', " +
                                    $"Bairro = '{endereco.Bairro}', " +
                                    $"Cidade = '{endereco.Cidade}', " +
                                    $"Uf = '{endereco.Uf}', " +
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

            string view = null;
            Endereco endereco = GetEndereco(id, view);
            var total = endereco.Equipamentos;

            //if(total == null)
            //{
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"DELETE FROM tbl_Endereco WHERE EnderecoID = {id}";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            //} 

        }
        #endregion

        #region Equipamento
        public IEnumerable<Equipamento> GetAllEquipamentos(int? id, string view)
        {
            string connectionString = Conexao();
            List<Equipamento> listEquipamento = new List<Equipamento>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = null;

                if (view == "Endereco")
                {
                    query = $"SELECT * FROM tbl_Equipamento WHERE EnderecoID = {id}";
                }
                else
                {
                    query = $"SELECT * FROM tbl_Equipamento WHERE ClienteID = {id}";
                }


                SqlCommand cmd = new SqlCommand(query, con);
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

                    //Criado para adicionar novo equipamento para um endereço específico 
                    Endereco endereco = new Endereco();
                    endereco.EnderecoID = Convert.ToInt32(reader["EnderecoID"]);
                    equipamento.Endereco = endereco;

                    Cliente cliente = new Cliente();
                    cliente.ClienteID = Convert.ToInt32(reader["ClienteID"]);
                    equipamento.Cliente = cliente;
                    //Fim

                    listEquipamento.Add(equipamento);
                }
                con.Close();
            }

            return listEquipamento;
        }

        public IEnumerable<Equipamento> GetAllEquipamentosCliente(int? id)
        {
            Cliente ClienteID = GetClienteOrdemServico(id);
            int idcliente = ClienteID.ClienteID;

            string connectionString = Conexao();
            List<Equipamento> listEquipamento = new List<Equipamento>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
             
                string query = $" SELECT * FROM tbl_Equipamento WHERE ClienteID = {idcliente}";              

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Equipamento equipamento = new Equipamento();
                    equipamento.EquipamentoID = Convert.ToInt32(reader["EquipamentoID"]);

                    string Tipo = reader["Tipo"].ToString();
                    string Fabricante = reader["Fabricante"].ToString();
                    string Modelo = reader["Modelo"].ToString();
                    string NSerie = reader["NSerie"].ToString();
                    
                    equipamento.Tipo = Tipo + ' ' + Fabricante + ' ' + Modelo + ' ' + NSerie;
                    

                    listEquipamento.Add(equipamento);
                }
                con.Close();
            }

            return listEquipamento;
        }

        public Equipamento GetEquipamento(int? id, string view)
        {

            string connectionString = Conexao();

            Equipamento equipamento = new Equipamento();
            Cliente cliente = new Cliente();

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string sqlQuery = null;

                if (view == "Endereco")
                {
                    sqlQuery = $"SELECT * FROM tbl_Equipamento WHERE EnderecoID = {id}";
                }
                else
                {
                    sqlQuery = $"SELECT * FROM tbl_Equipamento WHERE EquipamentoID = {id} ";
                }

                //string sqlQuery = $"SELECT * FROM tbl_Equipamento WHERE EquipamentoID = {id}";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente.ClienteID = Convert.ToInt32(reader["ClienteID"]);
                    equipamento.Cliente = cliente;

                    equipamento.EquipamentoID = Convert.ToInt32(reader["EquipamentoID"]);
                    equipamento.Tipo = reader["Tipo"].ToString();
                    equipamento.Fabricante = reader["Fabricante"].ToString();
                    equipamento.Modelo = reader["Modelo"].ToString();
                    equipamento.NSerie = reader["NSerie"].ToString();

                    //Utilizado para pegar criar a lista no Details dos equipamentos 
                    equipamento.OrdemServicos = GetAllOrdemServico(id, view);

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
                //Para cadastro vindo do Cliente/Details
                string sqlQuery;
                if (equipamento.Endereco != null)
                {
                    sqlQuery = $"INSERT INTO tbl_Equipamento " +
                                   $"Values({equipamento.Endereco.EnderecoID}, " +
                                   $"'{equipamento.Cliente.ClienteID}', " +
                                   $"'{equipamento.Tipo}', " +
                                   $"'{equipamento.Fabricante}', " +
                                   $"'{equipamento.Modelo}', " +
                                   $"'{equipamento.NSerie}')";

                }
                else
                {
                    sqlQuery = $"INSERT INTO tbl_Equipamento " +
                                       $"Values({equipamento.Cliente.Endereco.EnderecoID}, " +
                                       $"'{equipamento.Cliente.ClienteID}', " +
                                       $"'{equipamento.Tipo}', " +
                                       $"'{equipamento.Fabricante}', " +
                                       $"'{equipamento.Modelo}', " +
                                       $"'{equipamento.NSerie}')";
                }

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
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
                string comandoSQL = $"UPDATE tbl_Equipamento SET " +
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
                string comandoSQL = $"DELETE FROM tbl_Equipamento WHERE EquipamentoID = {id}";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        #endregion

        #region Atendimento

        public IEnumerable<Atendimento> GetAllAtedimentos(string data)
        {
            string connectionString = Conexao();

            List<Atendimento> listAtendimento = new List<Atendimento>();

            Equipamento equipamento = new Equipamento();
            Colaborador colaborador = new Colaborador();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string selectQuery;

                if (data == null)
                {
                    selectQuery = "SELECT C.Nome, Cont.Celular, Cont.Telefone, E.Logradouro, E.Bairro, E.Cidade," +
                                  " E.Uf, E.Complemento, A.Defeito, A.Data, A.Periodo, A.Observacao, A.Categoria, Eq.Tipo " +
                                  " FROM tbl_Atendimento A " +
                                  " INNER JOIN tbl_Equipamento Eq ON A.EquipamentoID = Eq.EquipamentoID" +
                                  " INNER JOIN tbl_Endereco E ON Eq.EnderecoID = E.EnderecoID" +
                                  " INNER JOIN tbl_Cliente C ON Eq.ClienteID = C.ClienteID" +
                                  " INNER JOIN tbl_Contato Cont ON C.ClienteID = Cont.ClienteID" +
                                  $" WHERE A.Data = '{ data }'";

                }
                else
                {
                    selectQuery = "SELECT C.Nome, Cont.Celular, Cont.Telefone, E.Logradouro, E.Bairro, E.Cidade," +
                                " E.Uf, E.Complemento, A.Defeito, A.Data, A.Periodo, A.Observacao, A.Categoria, Eq.Tipo " +
                                " FROM tbl_Atendimento A " +
                                " INNER JOIN tbl_Equipamento Eq ON A.EquipamentoID = Eq.EquipamentoID" +
                                " INNER JOIN tbl_Endereco E ON Eq.EnderecoID = E.EnderecoID" +
                                " INNER JOIN tbl_Cliente C ON Eq.ClienteID = C.ClienteID" +
                                " INNER JOIN tbl_Contato Cont ON C.ClienteID = Cont.ClienteID";

                }

                SqlCommand cmd = new SqlCommand(selectQuery, con);

                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Atendimento atendimento = new Atendimento();
                    atendimento.AtendimentoID = Convert.ToInt32(reader["AtendimentoID"]);

                    equipamento.EquipamentoID = Convert.ToInt32(reader["EquipamentoID"]);
                    equipamento.Tipo = reader["Tipo"].ToString();
                    atendimento.Equipamento = equipamento;
                    atendimento.Defeito = reader["Defeito"].ToString();
                    atendimento.Data = Convert.ToDateTime(reader["Data"].ToString());
                    atendimento.Periodo = reader["Periodo"].ToString();
                    atendimento.Observacao = reader["Observacao"].ToString();

                    listAtendimento.Add(atendimento);
                }
                con.Close();
            }

            return listAtendimento;
        }
        public void AddAtendimento(Atendimento atendimento)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                var Data = atendimento.Data.ToString("yyyy/MM/dd");

                string comandoSQL = $"INSERT INTO tbl_Atendimento (EquipamentoID, Colaborador, Defeito, Data, Periodo, Observacao) " +
                                    $"Values({atendimento.Equipamento.EquipamentoID}, " +
                                    $"'{atendimento.Colaborador}', " +
                                    $"'{atendimento.Defeito}', " +
                                    $"'{Data}', " +
                                    $"'{atendimento.Periodo}', " +
                                    $"'{atendimento.Observacao}')";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Atendimento GetAtendimento(int? id)
        {
            string connectionString = Conexao();
            Atendimento atendimento = new Atendimento();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM tbl_Atendimento WHERE AtendimentoID = {id}";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    atendimento.AtendimentoID = Convert.ToInt32(reader["AtendimentoID"]);
                    atendimento.Defeito = reader["Defeito"].ToString();
                    atendimento.Data = Convert.ToDateTime(reader["Data"].ToString());
                    atendimento.Periodo = reader["Periodo"].ToString();
                    atendimento.Observacao = reader["Observacao"].ToString();
                    atendimento.Colaborador = reader["Colaborador"].ToString();
                }
                con.Close();
            }
            return atendimento;
        }

        public void UpdateAtendimento(Atendimento atendimento)
        {
            string connectionString = Conexao();

            var Data = atendimento.Data.ToString("yyyy/MM/dd");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"UPDATE tbl_Atendimento " +
                                    $"SET Defeito = '{atendimento.Defeito}', " +
                                    $"Data = '{Data}', " +
                                    $"Periodo = '{atendimento.Periodo}'," +
                                    $"Observacao = '{atendimento.Observacao}', " +
                                    $"Colaborador = '{atendimento.Colaborador}' " +
                                    $"WHERE AtendimentoID = {atendimento.AtendimentoID}";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DeleteAtendimento(int? id)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"DELETE FROM tbl_Atendimento WHERE AtendimentoID = {id}";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        #endregion

        #region Ordem de Serviço
        public OrdemServico GetOrdemServico(int? os)
        {
            string connectionString = Conexao();
            OrdemServico ordemServico = new OrdemServico();
            Equipamento equipamento = new Equipamento();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM tbl_OrdemServico WHERE OrdemServicoID = {os}";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    /*/Criado pela necessidade de ter ordem duplicada no sistema
                    string Duplicado = reader["Duplicado"].ToString();
                    if (Duplicado == "D")
                    {
                        int OrdemServicoID = Convert.ToInt32(reader["OrdemServicoID"]);
                        ordemServico.OrdemServicoID = OrdemServicoID + Duplicado;
                    }
                    else
                    {
                        ordemServico.OrdemServicoID = reader["OrdemServicoID"].ToString();
                    }
                    */

                    ordemServico.OrdemServicoID = Convert.ToInt32(reader["OrdemServicoID"]);
                    equipamento.EquipamentoID = Convert.ToInt32(reader["EquipamentoID"]);
                    ordemServico.Equipamento = equipamento;
                    ordemServico.Data = Convert.ToDateTime(reader["Data"].ToString());
                    ordemServico.Valor = reader["Valor"].ToString();
                    ordemServico.Categoria = reader["Categoria"].ToString();
                    ordemServico.Defeito = reader["Defeito"].ToString();
                    ordemServico.Relatorio = reader["Relatorio"].ToString();
                    ordemServico.Colaborador = reader["Colaborador"].ToString();
                    ordemServico.Observacao = reader["Observacao"].ToString();
                }
                con.Close();
            }
            return ordemServico;
        }

        public IEnumerable<OrdemServico> GetAllOrdemServico(int? id, string view)
        {
            string connectionString = Conexao();

            List<OrdemServico> listOrdemServico = new List<OrdemServico>();
            Equipamento equipamento = new Equipamento();
            string stringQuery;

            if (view == "OS")
            {
                if (id != 0000)
                {
                    stringQuery = $"SELECT * FROM tbl_OrdemServico WHERE OrdemServicoID = {id}";
                }
                else
                {
                    stringQuery = $"SELECT TOP(12)* FROM tbl_OrdemServico ORDER BY Data DESC";
                }
            }
            else
            {
                stringQuery = $"SELECT * FROM tbl_OrdemServico WHERE EquipamentoID = {id} ";
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(stringQuery, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OrdemServico ordemServico = new OrdemServico();
                   
               
                    ordemServico.OrdemServicoID = Convert.ToInt32(reader["OrdemServicoID"]);
                    equipamento.EquipamentoID = Convert.ToInt32(reader["EquipamentoID"]);
                    ordemServico.Equipamento = equipamento;

                    ordemServico.Data = Convert.ToDateTime(reader["Data"].ToString());
                    ordemServico.Valor = reader["Valor"].ToString();
                    ordemServico.Categoria = reader["Categoria"].ToString();
                    ordemServico.Observacao = reader["Observacao"].ToString();
                    ordemServico.Defeito = reader["Defeito"].ToString();
                    ordemServico.Relatorio = reader["Relatorio"].ToString();
                    ordemServico.Colaborador = reader["Colaborador"].ToString();

                    listOrdemServico.Add(ordemServico);
                }
                con.Close();
            }

            return listOrdemServico;
        }
        public void AddOrdemServico(OrdemServico ordemservico)
        {
            string valor = "";

            if (ordemservico.Valor != null)
            {
                //Converter a virgula em ponto, o SQL dar erro em caso de virgula.
                valor = ordemservico.Valor.Replace(',', '.');
            }
            else
            {
                valor = "0";
            }

            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var Data = ordemservico.Data.ToString("yyyy/MM/dd");

                string comandoSQL = $"INSERT INTO tbl_OrdemServico (OrdemServicoID,Duplicado, EquipamentoID, Data, Valor,Categoria, Defeito, Relatorio,Observacao, Colaborador) " +
                                    $"Values({ordemservico.OrdemServicoID}, " +
                                    $"'N', " +
                                    $"{ordemservico.Equipamento.EquipamentoID}, " +
                                    $"'{Data}', " +
                                    $"{valor}, " +
                                    $"'{ordemservico.Categoria}', " +
                                    $"'{ordemservico.Defeito}', " +
                                    $"'{ordemservico.Relatorio}', " +
                                    $"'{ordemservico.Observacao}', " +
                                    $"'{ordemservico.Colaborador}')";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void AddOrdemServicoDuplicado(OrdemServico ordemservico)
        {
            string valor = "";

            if (ordemservico.Valor != null)
            {
                //Converter a virgula em ponto, o SQL dar erro em caso de virgula.
                valor = ordemservico.Valor.Replace(',', '.');
            }
            else
            {
                valor = "0";
            }

            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var Data = ordemservico.Data.ToString("yyyy/MM/dd");

                string comandoSQL = $"INSERT INTO tbl_OrdemServico (OrdemServicoID, Duplicado, EquipamentoID, Data, Valor,Categoria, Defeito, Relatorio,Observacao, Colaborador) " +
                                    $"Values({ordemservico.OrdemServicoID}, " +
                                    $"'D', " +
                                    $"{ordemservico.Equipamento.Tipo}, " +
                                    $"'{Data}', " +
                                    $"{valor}, " +
                                    $"'{ordemservico.Categoria}', " +
                                    $"'{ordemservico.Defeito}', " +
                                    $"'{ordemservico.Relatorio}', " +
                                    $"'{ordemservico.Observacao}', " +
                                    $"'{ordemservico.Colaborador}')";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateOrdemServico(OrdemServico ordemServico)
        {
            string connectionString = Conexao();

            string valor = "";

            if (ordemServico.Valor != null)
            {
            //Converter a virgula em ponto, o SQL dar erro em caso de virgula.
             valor = ordemServico.Valor.Replace(',', '.');
            }
            else
            {
                valor = "0";
            }


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"UPDATE tbl_OrdemServico " +
                                    $"SET Data = '{ordemServico.Data}', " +
                                    $"Valor = {valor}, " +
                                    $"Categoria = '{ordemServico.Categoria}', " +
                                    $"Defeito = '{ordemServico.Defeito}', " +
                                    $"Relatorio = '{ordemServico.Relatorio}'," +
                                    $"Observacao = '{ordemServico.Observacao}', " +
                                    $" Colaborador = '{ordemServico.Colaborador}' " +
                                    $"WHERE OrdemServicoID = {ordemServico.OrdemServicoID}";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DeleteOrdemServico(int? id)
        {
            string connectionString = Conexao();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = $"DELETE FROM tbl_OrdemServico WHERE OrdemServicoID = {id}";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        #endregion

        #region Colaborador
        public IEnumerable<Colaborador> GetAllColaborador()
        {
            string connectionString = Conexao();

            List<Colaborador> listColaborador = new List<Colaborador>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_Colaborador Where Funcao = 'Técnico em manutenção'", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Colaborador colaborador = new Colaborador();
                    colaborador.Nome = reader["Nome"].ToString();

                    listColaborador.Add(colaborador);
                }
                con.Close();
            }

            return listColaborador;
        }
        #endregion

        #region Tipo de Equipamento
        public List<TipoEquipamento> GetAllTipoEquipamento()
        {
            string connectionString = Conexao();

            List<TipoEquipamento> listTipoEquipamento = new List<TipoEquipamento>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_TipoEquipamento", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TipoEquipamento tipoEquipamento = new TipoEquipamento();

                    tipoEquipamento.TipoID = Convert.ToInt32(reader["TipoID"].ToString());
                    tipoEquipamento.Equipamento = reader["Equipamento"].ToString();

                    listTipoEquipamento.Add(tipoEquipamento);
                }
                con.Close();
            }

            return listTipoEquipamento;
        }
        #endregion

        #region Fabricante
        public List<Fabricante> GetAllFabricante(string equipamento)
        {
            string connectionString = Conexao();

            List<Fabricante> listFabricante = new List<Fabricante>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM tbl_TipoEquipamento te" +
                                                $" INNER JOIN tbl_Fabricante f" +
                                                $" ON te.TipoID = f.TipoID" +
                                                $" WHERE te.Equipamento = '{equipamento}'", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Fabricante fabricante = new Fabricante();
                    fabricante.TipoID = Convert.ToInt32(reader["TipoID"].ToString());
                    fabricante.FabricanteID = Convert.ToInt32(reader["FabricanteID"].ToString());
                    fabricante.NomeFabricante = reader["NomeFabricante"].ToString();

                    listFabricante.Add(fabricante);
                }
                con.Close();
            }

            return listFabricante;
        }
        #endregion

        #region Modelos
        public List<Modelo> GetAllModelo(string model)
        {
            string connectionString = Conexao();

            List<Modelo> listModelo = new List<Modelo>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($" SELECT F.NomeFabricante, M.NomeModelo FROM tbl_Fabricante F" +
                                                $" INNER JOIN tbl_Modelo M" +
                                                $" ON F.FabricanteID = M.FabricanteID" +
                                                $" WHERE F.NomeFabricante = '{ model }'", con);

                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Modelo modelo = new Modelo();
                    modelo.NomeModelo = reader["NomeModelo"].ToString();

                    listModelo.Add(modelo);
                }
                con.Close();
            }

            return listModelo;
        }
        #endregion

        #region Período
        public List<Atendimento> GetPeriodo()
        {
            string connectionString = Conexao();

            List<Atendimento> listPeriodo = new List<Atendimento>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($" SELECT * FROM Tbl_Periodo", con);

                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Atendimento atendimento = new Atendimento();
                    atendimento.Periodo = reader["Periodo"].ToString();

                    listPeriodo.Add(atendimento);
                }
                con.Close();
            }

            return listPeriodo;
        }
        #endregion

        #region Categoria de Ordem de Serviço
        public List<OrdemServico> GetAllCategoria()
        {
            string connectionString = Conexao();

            List<OrdemServico> listCategoria = new List<OrdemServico>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($" SELECT * FROM tbl_CategoriaOrdemServico", con);

                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    OrdemServico ordemServico = new OrdemServico();
                    ordemServico.Categoria = reader["Categoria"].ToString();

                    listCategoria.Add(ordemServico);
                }
                con.Close();
            }

            return listCategoria;
        }
        #endregion

        #region Evento
        public IEnumerable<Evento> GetAllEventos()
        {
            string connectionString = Conexao();

            List<Evento> listEvento = new List<Evento>();
            Equipamento equipamento = new Equipamento();
            Atendimento atendimento = new Atendimento();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string dataAtual = DateTime.Now.ToString("yyyy-MM-dd");

                SqlCommand cmd = new SqlCommand($"SELECT A.AtendimentoID, C.Nome, E.Tipo, A.Data FROM tbl_Atendimento A " +
                                                $"INNER JOIN tbl_Equipamento E " +
                                                $"ON A.EquipamentoID = E.EquipamentoID " +
                                                $"INNER JOIN tbl_Cliente C " +
                                                $"ON E.ClienteID = C.ClienteID " +
                                                $"WHERE A.Data >= '{ dataAtual }'" +
                                                $" ORDER BY A.Data ", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Evento evento = new Evento();
                    Cliente cliente = new Cliente();

                    atendimento.AtendimentoID = Convert.ToInt32(reader["AtendimentoID"]);
                    equipamento.Tipo = reader["Tipo"].ToString();
                    cliente.Nome = reader["Nome"].ToString();
                    atendimento.Data = Convert.ToDateTime(reader["Data"].ToString());

                    evento.Id = atendimento.AtendimentoID;
                    evento.Nome = cliente.Nome;
                    evento.Title = equipamento.Tipo;
                    evento.StartDate = atendimento.Data.ToString("yyyy-MM-dd");

                    listEvento.Add(evento);
                }
                con.Close();
            }

            return listEvento;
        }
        #endregion

        #region Relatório Antedimento

        public IEnumerable<RelatorioAtendimento> RelatorioAtendimento(string data)
        {
            string connectionString = Conexao();

            List<RelatorioAtendimento> relatorioAtendimento = new List<RelatorioAtendimento>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string selectQuery = "SELECT C.Nome, Cont.Celular, Cont.Telefone, E.Logradouro, E.Bairro, E.Cidade, A.AtendimentoID," +
                                  " E.Uf, E.Complemento, A.Defeito, A.Data, A.Periodo, A.Observacao, A.Colaborador, Eq.Tipo, Eq.Fabricante " +
                                  " FROM tbl_Atendimento A " +
                                  " INNER JOIN tbl_Equipamento Eq ON A.EquipamentoID = Eq.EquipamentoID" +
                                  " INNER JOIN tbl_Endereco E ON Eq.EnderecoID = E.EnderecoID" +
                                  " INNER JOIN tbl_Cliente C ON Eq.ClienteID = C.ClienteID" +
                                  " INNER JOIN tbl_Contato Cont ON C.ClienteID = Cont.ClienteID" +
                                  $" WHERE A.Data = '{ data }'" +
                                  $" ORDER BY A.Data DESC";


                SqlCommand cmd = new SqlCommand(selectQuery, con);

                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    RelatorioAtendimento relatorio = new RelatorioAtendimento();

                    Cliente cliente = new Cliente();
                    Contato contato = new Contato();
                    Endereco endereco = new Endereco();
                    Equipamento equipamento = new Equipamento();
                    Atendimento atendimento = new Atendimento();
                    Colaborador colaborador = new Colaborador();

                    cliente.Nome = reader["Nome"].ToString();

                    contato.Celular = reader["Celular"].ToString();
                    contato.Telefone = reader["Telefone"].ToString();

                    endereco.Logradouro = reader["Logradouro"].ToString();
                    endereco.Bairro = reader["Bairro"].ToString();
                    endereco.Cidade = reader["Cidade"].ToString();
                    endereco.Uf = reader["Uf"].ToString();
                    endereco.Complemento = reader["Complemento"].ToString();

                    atendimento.AtendimentoID = Convert.ToInt32(reader["AtendimentoID"].ToString());
                    atendimento.Defeito = reader["Defeito"].ToString();
                    atendimento.Data = Convert.ToDateTime(reader["Data"].ToString());
                    atendimento.Periodo = reader["Periodo"].ToString();
                    atendimento.Observacao = reader["Observacao"].ToString();
                    atendimento.Colaborador = reader["Colaborador"].ToString();

                    equipamento.Tipo = reader["Tipo"].ToString();
                    equipamento.Fabricante = reader["Fabricante"].ToString();

                    relatorio.Cliente = cliente;
                    relatorio.Contato = contato;
                    relatorio.Endereco = endereco;
                    relatorio.Atendimento = atendimento;
                    relatorio.Equipamento = equipamento;

                    relatorioAtendimento.Add(relatorio);
                }
                con.Close();
            }

            return relatorioAtendimento;
        }

        #endregion
    }
}
