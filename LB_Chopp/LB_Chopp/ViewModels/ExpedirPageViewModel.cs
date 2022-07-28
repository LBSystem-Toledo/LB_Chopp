using Acr.UserDialogs;
using LB_Chopp.Interface;
using LB_Chopp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LB_Chopp.ViewModels
{
    public class ExpedirPageViewModel : ViewModelBase
    {
        ReservaChopp reserva { get; set; }
        ObservableCollection<ReservaChopeira> _chopeiras;
        public ObservableCollection<ReservaChopeira> Chopeiras { get { return _chopeiras; } set { SetProperty(ref _chopeiras, value); } }
        ObservableCollection<ReservaBarril> _barris;
        public ObservableCollection<ReservaBarril> Barris { get { return _barris; } set { SetProperty(ref _barris, value); } }
        ObservableCollection<ReservaCilindro> _cilindros;
        public ObservableCollection<ReservaCilindro> Cilindros { get { return _cilindros; } set { SetProperty(ref _cilindros, value); } }

        public DelegateCommand<ReservaChopeira> ExpedirChopeiraCommand { get; }
        public DelegateCommand<ReservaBarril> ExpedirBarrilCommand { get; }
        public DelegateCommand<ReservaCilindro> ExpedirCilindroCommand { get; }
        public DelegateCommand SalvarCommand { get; }

        readonly IPageDialogService dialogService;
        readonly IDataService dataService;
        public ExpedirPageViewModel(INavigationService navigationService, IPageDialogService _dialogService, IDataService _dataService)
            : base(navigationService)
        {
            dialogService = _dialogService;
            dataService = _dataService;

            ExpedirChopeiraCommand = new DelegateCommand<ReservaChopeira>(async (ex) =>
            {
                if (ex != null)
                {
                    NavigationParameters param = new NavigationParameters();
                    param.Add("RESERVA", ex);
                    await navigationService.NavigateAsync("ChopeiraPage", param);
                }
            });
            ExpedirBarrilCommand = new DelegateCommand<ReservaBarril>(async (ex) =>
            {
                if (ex != null)
                {
                    NavigationParameters param = new NavigationParameters();
                    param.Add("RESERVA", ex);
                    await navigationService.NavigateAsync("BarrilPage", param);
                }
            });
            ExpedirCilindroCommand = new DelegateCommand<ReservaCilindro>(async (ex) =>
            {
                if (ex != null)
                {
                    NavigationParameters param = new NavigationParameters();
                    param.Add("RESERVA", ex);
                    await navigationService.NavigateAsync("CilindroPage", param);
                }
            });
            SalvarCommand = new DelegateCommand(async () =>
            {
                bool? ch = Chopeiras?.ToList().Exists(p => p.Id_chopeira == 0);
                bool? br = Barris?.ToList().Exists(p => p.Id_barril == 0);
                bool? cl = Cilindros?.ToList().Exists(p => p.Id_cilindro == 0);
                bool gravar = true;
                if ((ch ?? false) || (br ?? false) || (cl ?? false))
                    gravar = await dialogService.DisplayAlertAsync("Pergunta", "Existe reserva sem patrimonio informado.\r\nConfirma expedição PARCIAL?", "SIM", "NÃO");
                if (gravar)
                    using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
                    {
                        try
                        {
                            reserva.Chopeiras = Chopeiras?.Where(p => p.Id_chopeira > 0);
                            reserva.Barris = Barris?.Where(p => p.Id_barril > 0);
                            reserva.Cilindros = Cilindros?.Where(p => p.Id_cilindro > 0);
                            if (await dataService.GravarExpedicaoAsync(reserva))
                            {
                                await dialogService.DisplayAlertAsync("Mensagem", "Expedição gravada com sucesso.", "OK");
                                await navigationService.GoBackAsync();
                            }
                            else await dialogService.DisplayAlertAsync("Mensagem", "Erro gravar expedição.", "OK");
                        }
                        catch (Exception ex) { await dialogService.DisplayAlertAsync("Erro", ex.Message.Trim(), "OK"); }
                    }
            });
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("RESERVA"))
            {
                reserva = parameters["RESERVA"] as ReservaChopp;
                using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
                {
                    try
                    {
                        //Buscar chopeiras
                        Chopeiras = new ObservableCollection<ReservaChopeira>(await dataService.GetChopeirasExpedirAsync(reserva.Cd_empresa, reserva.Id_reserva));
                        //Barris
                        Barris = new ObservableCollection<ReservaBarril>(await dataService.GetBarrisExpedirAsync(reserva.Cd_empresa, reserva.Id_reserva));
                        //Cilindros
                        Cilindros = new ObservableCollection<ReservaCilindro>(await dataService.GetCilindrosExpedirAsync(reserva.Cd_empresa, reserva.Id_reserva));
                    }
                    catch { }
                }
            }
            if (parameters.ContainsKey("RESERVACHOPEIRA"))
            {
                ReservaChopeira reservaChopeira = parameters["RESERVACHOPEIRA"] as ReservaChopeira;
                Chopeiras.First(p => p.Cd_empresa == reservaChopeira.Cd_empresa &&
                                    p.Id_reserva == reservaChopeira.Id_reserva &&
                                    p.Id_item == reservaChopeira.Id_item).Id_chopeira = reservaChopeira.Id_chopeira;
                Chopeiras.First(p => p.Cd_empresa == reservaChopeira.Cd_empresa &&
                                    p.Id_reserva == reservaChopeira.Id_reserva &&
                                    p.Id_item == reservaChopeira.Id_item).Nr_chopeira = reservaChopeira.Nr_chopeira;
            }
            if (parameters.ContainsKey("RESERVABARRIL"))
            {
                ReservaBarril reservaBarril = parameters["RESERVABARRIL"] as ReservaBarril;
                Barris.First(p => p.Cd_empresa == reservaBarril.Cd_empresa &&
                                    p.Id_reserva == reservaBarril.Id_reserva &&
                                    p.Id_item == reservaBarril.Id_item).Id_barril = reservaBarril.Id_barril;
                Barris.First(p => p.Cd_empresa == reservaBarril.Cd_empresa &&
                                    p.Id_reserva == reservaBarril.Id_reserva &&
                                    p.Id_item == reservaBarril.Id_item).Nr_barril = reservaBarril.Nr_barril;
            }
            if (parameters.ContainsKey("RESERVACILINDRO"))
            {
                ReservaCilindro reservaCilindro = parameters["RESERVACILINDRO"] as ReservaCilindro;
                Cilindros.First(p => p.Cd_empresa == reservaCilindro.Cd_empresa &&
                                    p.Id_reserva == reservaCilindro.Id_reserva &&
                                    p.Id_item == reservaCilindro.Id_item).Id_cilindro = reservaCilindro.Id_cilindro;
                Cilindros.First(p => p.Cd_empresa == reservaCilindro.Cd_empresa &&
                                    p.Id_reserva == reservaCilindro.Id_reserva &&
                                    p.Id_item == reservaCilindro.Id_item).Nr_cilindro = reservaCilindro.Nr_cilindro;
            }
        }
    }
}
