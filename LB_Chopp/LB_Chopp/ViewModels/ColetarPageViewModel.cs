using Acr.UserDialogs;
using LB_Chopp.Interface;
using LB_Chopp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;

namespace LB_Chopp.ViewModels
{
    public class ColetarPageViewModel : ViewModelBase
    {
        ReservaChopp reserva { get; set; }
        ObservableCollection<ReservaChopeira> _chopeiras;
        public ObservableCollection<ReservaChopeira> Chopeiras { get { return _chopeiras; } set { SetProperty(ref _chopeiras, value); } }
        ObservableCollection<ReservaBarril> _barris;
        public ObservableCollection<ReservaBarril> Barris { get { return _barris; } set { SetProperty(ref _barris, value); } }
        ObservableCollection<ReservaCilindro> _cilindros;
        public ObservableCollection<ReservaCilindro> Cilindros { get { return _cilindros; } set { SetProperty(ref _cilindros, value); } }

        public DelegateCommand<ReservaChopeira> ColetarChopeiraCommand { get; }
        public DelegateCommand<ReservaBarril> ColetarBarrilCommand { get; }
        public DelegateCommand<ReservaCilindro> ColetarCilindroCommand { get; }

        readonly IPageDialogService dialogService;
        readonly IDataService dataService;
        public ColetarPageViewModel(INavigationService navigationService, IPageDialogService _dialogService, IDataService _dataService)
            :base(navigationService)
        {
            dialogService = _dialogService;
            dataService = _dataService;

            ColetarChopeiraCommand = new DelegateCommand<ReservaChopeira>(async (ch) =>
            {
                if(ch != null)
                {
                    var ret = await dialogService.DisplayAlertAsync("Pergunta", "Confirma coleta chopeira Nº" + ch.Nr_chopeira + "?", "SIM", "NÃO");
                    if(ret)
                        using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
                        {
                            try
                            {
                                await dataService.ColetarChopeiraAsync(ch);
                                await dialogService.DisplayAlertAsync("Mensagem", "Coleta gravada com sucesso.", "OK");
                                Chopeiras.Remove(ch);
                                if (Cilindros.Count.Equals(0) &&
                                    Barris.Count.Equals(0) &&
                                    Chopeiras.Count.Equals(0))
                                    await navigationService.GoBackAsync();
                            }
                            catch(Exception ex) { await dialogService.DisplayAlertAsync("Erro", ex.Message.Trim(), "OK"); }
                        }
                }
            });
            ColetarBarrilCommand = new DelegateCommand<ReservaBarril>(async (barril) =>
            {
                if (barril != null)
                {
                    var ret = await dialogService.DisplayAlertAsync("Pergunta", "Confirma coleta barril Nº" + barril.Nr_barril + "?", "SIM", "NÃO");
                    if (ret)
                        using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
                        {
                            try
                            {
                                ret = await dialogService.DisplayAlertAsync("Pergunta", "Retornou Cheio?", "SIM", "NÃO");
                                barril.RetornouCheio = ret;
                                await dataService.ColetarBarrilAsync(barril);
                                await dialogService.DisplayAlertAsync("Mensagem", "Coleta gravada com sucesso.", "OK");
                                Barris.Remove(barril);
                                if (Cilindros.Count.Equals(0) &&
                                    Barris.Count.Equals(0) &&
                                    Chopeiras.Count.Equals(0))
                                    await navigationService.GoBackAsync();
                            }
                            catch (Exception ex) { await dialogService.DisplayAlertAsync("Erro", ex.Message.Trim(), "OK"); }
                        }
                }
            });
            ColetarCilindroCommand = new DelegateCommand<ReservaCilindro>(async (cilindro) =>
            {
                if (cilindro != null)
                {
                    var ret = await dialogService.DisplayAlertAsync("Pergunta", "Confirma coleta cilindro Nº" + cilindro.Nr_cilindro + "?", "SIM", "NÃO");
                    if (ret)
                        using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
                        {
                            try
                            {
                                await dataService.ColetarCilindroAsync(cilindro);
                                await dialogService.DisplayAlertAsync("Mensagem", "Coleta gravada com sucesso.", "OK");
                                Cilindros.Remove(cilindro);
                                if (Cilindros.Count.Equals(0) &&
                                    Barris.Count.Equals(0) &&
                                    Chopeiras.Count.Equals(0))
                                    await navigationService.GoBackAsync();
                            }
                            catch (Exception ex) { await dialogService.DisplayAlertAsync("Erro", ex.Message.Trim(), "OK"); }
                        }
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
                        Chopeiras = new ObservableCollection<ReservaChopeira>(await dataService.GetChopeirasColetarAsync(reserva.Cd_empresa, reserva.Id_reserva));
                        //Barris
                        Barris = new ObservableCollection<ReservaBarril>(await dataService.GetBarrisColetarAsync(reserva.Cd_empresa, reserva.Id_reserva));
                        //Cilindros
                        Cilindros = new ObservableCollection<ReservaCilindro>(await dataService.GetCilindrosColetarAsync(reserva.Cd_empresa, reserva.Id_reserva));
                    }
                    catch { }
                }
            }
        }
    }
}
