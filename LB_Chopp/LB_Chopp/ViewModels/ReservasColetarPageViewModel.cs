using Acr.UserDialogs;
using LB_Chopp.Interface;
using LB_Chopp.Models;
using LB_Chopp.Utils;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LB_Chopp.ViewModels
{
    public class ReservasColetarPageViewModel : ViewModelBase
    {
        Cliente _cliente;
        public Cliente Cliente { get { return _cliente; } set { SetProperty(ref _cliente, value); } }
        string _dt_ini = string.Empty;
        public string Dt_ini { get { return _dt_ini; } set { SetProperty(ref _dt_ini, value); } }
        string _dt_fin = string.Empty;
        public string Dt_fin { get { return _dt_fin; } set { SetProperty(ref _dt_fin, value); } }
        string _id_reserva = string.Empty;
        public string Id_reserva { get { return _id_reserva; } set { SetProperty(ref _id_reserva, value); } }
        ObservableCollection<ReservaChopp> _reservas;
        public ObservableCollection<ReservaChopp> Reservas { get { return _reservas; } set { SetProperty(ref _reservas, value); } }

        public DelegateCommand BuscarClienteCommand { get; }
        public DelegateCommand LimparCliente { get; }
        public DelegateCommand BuscarReservasCommand { get; }
        public DelegateCommand<ReservaChopp> ProrrogarCommand { get; }
        public DelegateCommand<ReservaChopp> ColetarCommand { get; }
        public DelegateCommand<ReservaChopp> AnexarFotoCommand { get; }

        readonly IPageDialogService dialogService;
        readonly IDataService dataService;
        public ReservasColetarPageViewModel(INavigationService navigationService, IPageDialogService _dialogService, IDataService _dataService)
            :base(navigationService)
        {
            dialogService = _dialogService;
            dataService = _dataService;

            LimparCliente = new DelegateCommand(() => Cliente = null);
            BuscarClienteCommand = new DelegateCommand(async () =>
            {
                await NavigationService.NavigateAsync("ConsultaClientePage");
            });
            BuscarReservasCommand = new DelegateCommand(async () =>
            {
                await BuscarReservasAsync();
            });
            ProrrogarCommand = new DelegateCommand<ReservaChopp>(async (ex) =>
            {
                if (ex != null)
                {
                    NavigationParameters param = new NavigationParameters();
                    param.Add("RESERVA", ex);
                    await NavigationService.NavigateAsync("ProrrogarColetaPage", param);
                }
            });
            ColetarCommand = new DelegateCommand<ReservaChopp>(async (ex) =>
            {
                if (ex != null)
                {
                    NavigationParameters param = new NavigationParameters();
                    param.Add("RESERVA", ex);
                    await NavigationService.NavigateAsync("ColetarPage", param);
                }
            });
            AnexarFotoCommand = new DelegateCommand<ReservaChopp>(async (ex) =>
            {
                if(ex != null)
                {
                    NavigationParameters param = new NavigationParameters();
                    param.Add("RESERVA", ex);
                    param.Add("ORIGEM", "C");
                    await NavigationService.NavigateAsync("ImagemPage", param);
                }
            });
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("CLIENTE"))
                Cliente = parameters["CLIENTE"] as Cliente;
            await BuscarReservasAsync();
        }
        public async Task BuscarReservasAsync()
        {
            using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
            {
                try
                {
                    Reservas = new ObservableCollection<ReservaChopp>(
                    await dataService.GetReservaColetarAsync(string.IsNullOrWhiteSpace(Id_reserva.SoNumero()) ? 0 : int.Parse(Id_reserva.SoNumero()),
                                                             Cliente == null ? string.Empty : Cliente.Cd_clifor,
                                                             Dt_ini.IsDateTime() ? DateTime.Parse(Dt_ini).ToString("yyyy-MM-dd") : string.Empty,
                                                             Dt_fin.IsDateTime() ? DateTime.Parse(Dt_fin).ToString("yyyy-MM-dd") : string.Empty));
                }
                catch { }
            }
        }
    }
}
