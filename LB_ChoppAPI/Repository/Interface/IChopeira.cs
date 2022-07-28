using LB_ChoppAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Repository.Interface
{
    public interface IChopeira
    {
        Task<IEnumerable<Chopeira>> GetChopeiraLivreAsync(string Token, 
                                                          string Voltagem,
                                                          string Qt_torneiras);
        Task<IEnumerable<ChopeiraDisponivel>> GetChopeirasDisponiveisAsync(string token,
                                                                           string dt_ini,
                                                                           string dt_fin);
    }
}
