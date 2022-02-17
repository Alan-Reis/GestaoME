using ClickServ2022.Models;
using ClickServ2022.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ClickServ2022.Repository
{
    public class RepositoryDAL : IRepositoryDAL
    {
        string connectionString = @"Data Source=DESKTOP-7KH1TOI\SQLEXPRESS;Initial Catalog=DBClickServ; Integrated Security=True;";

        public IEnumerable<Pessoa> GetAllClientes()
        {
            List<Pessoa> listPessoa = new List<Pessoa>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM PESSOA", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pessoa pessoa = new Pessoa();

                    pessoa.PessoaID = Convert.ToInt32(reader["PessoaID"]);
                    pessoa.Nome = reader["Nome"].ToString();

                    listPessoa.Add(pessoa);
                }
                con.Close();
            }

            return listPessoa;
        }
    }
}
