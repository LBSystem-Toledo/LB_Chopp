using LB_Chopp.Interface;
using LB_Chopp.Models;
using LB_Chopp.Utils;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Collections.Generic;

namespace LB_Chopp.Service
{
    public class DataService: IDataService
    {
        public async Task<TokenVendedor> LoginAsync(string Cnpj,
                                                    string Login,
                                                    string Senha)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/Login/LoginAsync", Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(Cnpj.SoNumero())))
                        .AddHeader("cnpj", Cnpj)
                        .AddHeader("login", Login)
                        .AddHeader("senha", Senha);
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<TokenVendedor>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
                
        public async Task<bool> ValidarTerminalAsync(TerminalMobile terminal)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/Terminal/ValidarDeviceAsync", Method.Post);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())))
                        .AddJsonBody<TerminalMobile>(terminal);
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<bool>(response.Content);
                    else return false;
                }
            }
            catch { return false; }
        }

        public async Task<IEnumerable<Cliente>> GetClienteAsync(string Cd_cliente = "", string Nome = "")
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/Cliente/GetAsync?Cd_cliente=" + Cd_cliente + "&Nome=" + Nome, Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<Cliente>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }

        public async Task<IEnumerable<ReservaChopp>> GetReservaExpedirAsync(int Id_reserva,
                                                                            string Cd_clifor,
                                                                            string Dt_ini,
                                                                            string Dt_fin)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/GetReservaExpedirAsync?Id_reserva=" + Id_reserva + "&Cd_clifor=" + Cd_clifor + "&Dt_ini=" + Dt_ini + "&Dt_fin=" + Dt_fin, Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<ReservaChopp>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaChopeira>> GetChopeirasExpedirAsync(string Cd_empresa, int Id_reserva)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/GetChopeirasExpedirAsync?Cd_empresa=" + Cd_empresa.Trim() + "&Id_reserva=" + Id_reserva, Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<ReservaChopeira>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaBarril>> GetBarrisExpedirAsync(string Cd_empresa, int Id_reserva)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/GetBarrisExpedirAsync?Cd_empresa=" + Cd_empresa.Trim() + "&Id_reserva=" + Id_reserva, Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<ReservaBarril>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaCilindro>> GetCilindrosExpedirAsync(string Cd_empresa, int Id_reserva)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/GetCilindrosExpedirAsync?Cd_empresa=" + Cd_empresa.Trim() + "&Id_reserva=" + Id_reserva, Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<ReservaCilindro>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaChopp>> GetReservaColetarAsync(int Id_reserva,
                                                                            string Cd_clifor,
                                                                            string Dt_ini,
                                                                            string Dt_fin)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/GetReservaColetarAsync?Id_reserva=" + Id_reserva + "&Cd_clifor=" + Cd_clifor + "&Dt_ini=" + Dt_ini + "&Dt_fin=" + Dt_fin, Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<ReservaChopp>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaChopeira>> GetChopeirasColetarAsync(string Cd_empresa, int Id_reserva)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/GetChopeirasColetarAsync?Cd_empresa=" + Cd_empresa.Trim() + "&Id_reserva=" + Id_reserva, Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<ReservaChopeira>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaBarril>> GetBarrisColetarAsync(string Cd_empresa, int Id_reserva)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/GetBarrisColetarAsync?Cd_empresa=" + Cd_empresa.Trim() + "&Id_reserva=" + Id_reserva, Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<ReservaBarril>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaCilindro>> GetCilindrosColetarAsync(string Cd_empresa, int Id_reserva)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/GetCilindrosColetarAsync?Cd_empresa=" + Cd_empresa.Trim() + "&Id_reserva=" + Id_reserva, Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<ReservaCilindro>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<Chopeira>> GetChopeiraLivreAsync(string Voltagem, string Qt_torneiras)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/Chopeira/GetChopeiraLivreAsync?Voltagem=" + Voltagem.Trim() + "&Qt_torneiras=" + Qt_torneiras.Trim(), Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<Chopeira>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ChopeiraDisponivel>> GetChopeirasDisponiveisAsync(string dt_ini, string dt_fin)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/Chopeira/GetChopeirasDisponiveisAsync?dt_ini=" + dt_ini + "&dt_fin=" + dt_fin, Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<ChopeiraDisponivel>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<Barril>> GetBarrilLivreAsync(string Cd_produto, int Volume)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/Barril/GetBarrilLivreAsync?Cd_produto=" + Cd_produto.Trim() + "&Volume=" + Volume, Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<Barril>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<BarrisTipo>> GetBarrisTiposAsync(string Cd_produto)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/Barril/GetBarrisTiposAsync?Cd_produto=" + Cd_produto.Trim(), Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<BarrisTipo>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<Cilindro>> GetCilindroLivreAsync()
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/Cilindro/GetCilindroLivreAsync", Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<Cilindro>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<Chopp>> GetChoppAsync()
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/Chopp/GetChoppAsync", Method.Get);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())));
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<IEnumerable<Chopp>>(response.Content);
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<bool> GravarExpedicaoAsync(ReservaChopp reserva)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/GravarExpedicaoAsync", Method.Post);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())))
                        .AddJsonBody<ReservaChopp>(reserva);
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<bool>(response.Content);
                    else return false;
                }
            }
            catch { return false; }
        }
        public async Task<bool> ProrrogarColetaAsync(ReservaChopp reserva)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/ProrrogarColetaAsync", Method.Post);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())))
                        .AddJsonBody<ReservaChopp>(reserva);
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<bool>(response.Content);
                    else return false;
                }
            }
            catch { return false; }
        }
        public async Task<bool> ColetarChopeiraAsync(ReservaChopeira reservaChopeira)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/ColetarChopeiraAsync", Method.Post);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())))
                        .AddJsonBody<ReservaChopeira>(reservaChopeira);
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<bool>(response.Content);
                    else return false;
                }
            }
            catch { return false; }
        }
        public async Task<bool> ColetarBarrilAsync(ReservaBarril reservaBarril)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/ColetarBarrilAsync", Method.Post);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())))
                        .AddJsonBody<ReservaBarril>(reservaBarril);
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<bool>(response.Content);
                    else return false;
                }
            }
            catch { return false; }
        }
        public async Task<bool> ColetarCilindroAsync(ReservaCilindro reservaCilindro)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/ColetarCilindroAsync", Method.Post);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())))
                        .AddJsonBody<ReservaCilindro>(reservaCilindro);
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<bool>(response.Content);
                    else return false;
                }
            }
            catch { return false; }
        }
        public async Task<string> GravarFotoAsync(ReservaFoto foto)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/GravarFotoAsync", Method.Post);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())))
                        .AddJsonBody<ReservaFoto>(foto);
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<string>(response.Content);
                    else return response.ErrorMessage;
                }
            }
            catch(Exception ex) { return ex.Message.Trim(); }
        }
        public async Task<bool> GravarReservaAsync(ReservaChopp reserva)
        {
            try
            {
                using (var client = new RestClient(App.url_api))
                {
                    var request = new RestRequest("/api/ReservaChopp/GravarReservaAsync", Method.Post);
                    request.AddHeader("token", Convert.ToBase64String(Encoding.UTF8.GetBytes(App.config.Cnpj.SoNumero())))
                        .AddJsonBody<ReservaChopp>(reserva);
                    RestResponse response = await client.ExecuteAsync(request);
                    if (response.IsSuccessful)
                        return JsonConvert.DeserializeObject<bool>(response.Content);
                    else return false;
                }
            }
            catch { return false; }
        }
    }
}
