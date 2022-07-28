using LB_Chopp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LB_Chopp.Interface
{
    public interface IDataService
    {
        Task<TokenVendedor> LoginAsync(string Cnpj, string Login, string Senha);
        Task<bool> ValidarTerminalAsync(TerminalMobile terminal);
        Task<IEnumerable<Cliente>> GetClienteAsync(string Cd_cliente, string Nome);
        Task<IEnumerable<ReservaChopp>> GetReservaExpedirAsync(int Id_reserva,
                                                               string Cd_clifor,
                                                               string Dt_ini,
                                                               string Dt_fin);
        Task<IEnumerable<ReservaChopeira>> GetChopeirasExpedirAsync(string Cd_empresa, int Id_reserva);
        Task<IEnumerable<ReservaBarril>> GetBarrisExpedirAsync(string Cd_empresa, int Id_reserva);
        Task<IEnumerable<ReservaCilindro>> GetCilindrosExpedirAsync(string Cd_empresa, int Id_reserva);
        Task<IEnumerable<ReservaChopp>> GetReservaColetarAsync(int Id_reserva,
                                                               string Cd_clifor,
                                                               string Dt_ini,
                                                               string Dt_fin);
        Task<IEnumerable<ReservaChopeira>> GetChopeirasColetarAsync(string Cd_empresa, int Id_reserva);
        Task<IEnumerable<ReservaBarril>> GetBarrisColetarAsync(string Cd_empresa, int Id_reserva);
        Task<IEnumerable<ReservaCilindro>> GetCilindrosColetarAsync(string Cd_empresa, int Id_reserva);
        Task<IEnumerable<Chopeira>> GetChopeiraLivreAsync(string Voltagem, string Qt_torneiras);
        Task<IEnumerable<ChopeiraDisponivel>> GetChopeirasDisponiveisAsync(string dt_ini, string dt_fin);
        Task<IEnumerable<Barril>> GetBarrilLivreAsync(string Cd_produto, int Volume);
        Task<IEnumerable<BarrisTipo>> GetBarrisTiposAsync(string Cd_produto);
        Task<IEnumerable<Cilindro>> GetCilindroLivreAsync();
        Task<IEnumerable<Chopp>> GetChoppAsync();
        Task<bool> GravarExpedicaoAsync(ReservaChopp reserva);
        Task<bool> ProrrogarColetaAsync(ReservaChopp reserva);
        Task<bool> ColetarChopeiraAsync(ReservaChopeira reservaChopeira);
        Task<bool> ColetarBarrilAsync(ReservaBarril reservaBarril);
        Task<bool> ColetarCilindroAsync(ReservaCilindro reservaCilindro);
        Task<string> GravarFotoAsync(ReservaFoto foto);
        Task<bool> GravarReservaAsync(ReservaChopp reserva);
    }
}
