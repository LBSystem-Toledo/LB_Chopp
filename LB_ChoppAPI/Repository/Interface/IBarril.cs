using LB_ChoppAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Repository.Interface
{
    public interface IBarril
    {
        Task<IEnumerable<Barril>> GetBarrilLivreAsync(string Token,
                                                      string Cd_produto,
                                                      int Volume);
        Task<IEnumerable<BarrisTipo>> GetBarrisTiposAsync(string token,
                                                          string cd_produto);
    }
}
