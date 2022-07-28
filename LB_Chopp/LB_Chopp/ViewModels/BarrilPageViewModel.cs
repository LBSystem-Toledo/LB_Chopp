using Acr.UserDialogs;
using LB_Chopp.Interface;
using LB_Chopp.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LB_Chopp.ViewModels
{
    public class BarrilPageViewModel : ViewModelBase
    {
        ReservaBarril reservaBarril { get; set; }
        ObservableCollection<Barril> _barris;
        public ObservableCollection<Barril> Barris { get { return _barris; } set { SetProperty(ref _barris, value); } }

        public DelegateCommand<Barril> BarrilCommand { get; }

        readonly IPageDialogService dialogService;
        readonly IDataService dataService;
        public BarrilPageViewModel(INavigationService navigationService, IPageDialogService _dialogService, IDataService _dataService)
            : base(navigationService)
        {
            dialogService = _dialogService;
            dataService = _dataService;

            BarrilCommand = new DelegateCommand<Barril>(async (barril) =>
            {
                if (barril != null)
                {
                    var ret = await dialogService.DisplayAlertAsync("Pergunta", "Confirma expedição do barril Nº" + barril.Nr_barril + " ?", "SIM", "NÃO");
                    if (ret)
                    {
                        reservaBarril.Id_barril = barril.Id_barril;
                        reservaBarril.Nr_barril = barril.Nr_barril;
                        NavigationParameters param = new NavigationParameters();
                        param.Add("RESERVABARRIL", reservaBarril);
                        await navigationService.GoBackAsync(param);
                    }
                }
            });
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            reservaBarril = parameters["RESERVA"] as ReservaBarril;
            using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
            {
                try
                {
                    //Buscar chopeiras disponiveis
                    Barris = new ObservableCollection<Barril>(await dataService.GetBarrilLivreAsync(reservaBarril.Cd_produto, reservaBarril.Volume));
                }
                catch { }
            }
        }
    }
}
