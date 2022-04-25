using SistemaLogin.Models;

namespace SistemaLogin.Service
{
    public interface IRepositoryDAL
    {
        Login GetLogin(string usuario, string senha);

    }
}
