using LB_ChoppAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Repository.Interface
{
    public interface ICliente
    {
        Task<IEnumerable<Cliente>> GetAsync(string Token, string Cd_clifor, string Nome);
    }
}
