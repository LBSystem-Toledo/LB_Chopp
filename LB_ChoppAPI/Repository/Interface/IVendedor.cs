using LB_ChoppAPI.Models;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Repository.Interface
{
    public interface IVendedor
    {
        Task<TokenVendedor> validarAsync(string Token, string Login, string Senha, string Cnpj);
    }
}
