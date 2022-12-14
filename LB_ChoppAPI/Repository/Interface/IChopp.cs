using LB_ChoppAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Repository.Interface
{
    public interface IChopp
    {
        Task<IEnumerable<Chopp>> GetChoppAsync(string Token);
    }
}
