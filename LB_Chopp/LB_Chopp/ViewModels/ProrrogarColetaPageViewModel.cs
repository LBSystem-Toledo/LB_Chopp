using Acr.UserDialogs;
using LB_Chopp.Interface;
using LB_Chopp.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LB_Chopp.ViewModels
{
    public class ProrrogarColetaPageViewModel : ViewModelBase
    {
        ReservaChopp _reserva;
        public ReservaChopp Reserva { get { return _reserva; } set { SetProperty(ref _reserva, value); } }

        public DelegateCommand SalvarCommand { get; }

        readonly IPageDialogService dialogService;
        readonly IDataService dataService;
        public ProrrogarColetaPageViewModel(INavigationService navigationService, IPageDialogService _dialogService, IDataService _dataService)
            :base(navigationService)
        {
            dialogService = _dialogService;
            dataService = _dataService;

            SalvarCommand = new DelegateCommand(async () =>
            {
                if(string.IsNullOrWhiteSpace(Reserva.Motivo))
                {
                    await dialogService.DisplayAlertAsync("Mensagem", "Obrigatório informar motivo.", "OK");
                    return;
                }
                using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
                {
                    try
                    {
                        if (await dataService.ProrrogarColetaAsync(Reserva))
                        {
                            await dialogService.DisplayAlertAsync("Mensagem", "Coleta prorrogada com sucesso.", "OK");
                            await navigationService.GoBackAsync();
                        }
                        else await dialogService.DisplayAlertAsync("Erro", "Erro ao prorrogar coleta.", "OK");
                    }
                    catch(Exception ex) { await dialogService.DisplayAlertAsync("Erro", ex.Message.Trim(), "OK"); }
                }
            });
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("RESERVA"))
            {
                Reserva = parameters["RESERVA"] as ReservaChopp;
                Reserva.Dt_prevretornoOld = Reserva.Dt_prevretorno;
            }
        }
    }
}
