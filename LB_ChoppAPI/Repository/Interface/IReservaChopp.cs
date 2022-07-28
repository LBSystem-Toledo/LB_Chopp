using System.Threading.Tasks;
using System.Collections.Generic;
using LB_ChoppAPI.Models;

namespace LB_ChoppAPI.Repository.Interface
{
    public interface IReservaChopp
    {
        Task<IEnumerable<ReservaChopp>> GetReservaExpedirAsync(string token, 
                                                               int Id_reserva, 
                                                               string Cd_clifor,
                                                               string Dt_ini,
                                                               string Dt_fin);
        Task<IEnumerable<ReservaChopeira>> GetChopeirasExpedirAsync(string token,
                                                                    string Cd_empresa,
                                                                    int Id_reserva);
        Task<IEnumerable<ReservaBarril>> GetBarrisExpedirAsync(string token,
                                                               string Cd_empresa,
                                                               int Id_reserva);
        Task<IEnumerable<ReservaCilindro>> GetCilindrosExpedirAsync(string token,
                                                                    string Cd_empresa,
                                                                    int Id_reserva);
        Task<IEnumerable<ReservaChopp>> GetReservaColetarAsync(string token,
                                                               int Id_reserva,
                                                               string Cd_clifor,
                                                               string Dt_ini,
                                                               string Dt_fin);
        Task<IEnumerable<ReservaChopeira>> GetChopeirasColetarAsync(string token,
                                                                    string Cd_empresa,
                                                                    int Id_reserva);
        Task<IEnumerable<ReservaBarril>> GetBarrisColetarAsync(string token,
                                                               string Cd_empresa,
                                                               int Id_reserva);
        Task<IEnumerable<ReservaCilindro>> GetCilindrosColetarAsync(string token,
                                                                    string Cd_empresa,
                                                                    int Id_reserva);
        Task<bool> GravarExpedicaoAsync(string token, ReservaChopp reserva);
        Task<bool> ProrrogarColetaAsync(string token, ReservaChopp reserva);
        Task<bool> ColetarChopeiraAsync(string token, ReservaChopeira reservaChopeira);
        Task<bool> ColetarBarrilAsync(string token, ReservaBarril reservaBarril);
        Task<bool> ColetarCilindroAsync(string token, ReservaCilindro reservaCilindro);
        Task<string> GravarFotoAsync(string token, ReservaFoto foto);
        Task<bool> GravarReservaAsync(string token, ReservaChopp reserva);
    }
}
