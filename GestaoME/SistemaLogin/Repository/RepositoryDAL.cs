
using Microsoft.Extensions.Configuration;
using SistemaLogin.Models;
using SistemaLogin.Service;
using System.Data.SqlClient;

namespace SistemaLogin.Repository
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

        public Login GetLogin(string usuario, string senha)
        {
            string connectionString = Conexao();
            Login login = new Login();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = $"SELECT * FROM tbl_login WHERE Usuario = '{usuario}' AND Senha = '{senha}'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    login.Nome = reader["Nome"].ToString();
                    //login.Usuario = reader["Usuario"].ToString();
                    //login.Senha = reader["Senha"].ToString();

                }
                con.Close();
            }
            return login;
        }

    }
}
