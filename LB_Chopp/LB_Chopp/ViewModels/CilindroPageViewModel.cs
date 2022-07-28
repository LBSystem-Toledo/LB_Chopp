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
    public class CilindroPageViewModel : ViewModelBase
    {
        ReservaCilindro reservaCilindro { get; set; }
        ObservableCollection<Cilindro> _cilindros;
        public ObservableCollection<Cilindro> Cilindros { get { return _cilindros; } set { SetProperty(ref _cilindros, value); } }

        public DelegateCommand<Cilindro> CilindroCommand { get; }

        readonly IPageDialogService dialogService;
        readonly IDataService dataService;
        public CilindroPageViewModel(INavigationService navigationService, IPageDialogService _dialogService, IDataService _dataService)
            : base(navigationService)
        {
            dialogService = _dialogService;
            dataService = _dataService;

            CilindroCommand = new DelegateCommand<Cilindro>(async (cilindro) =>
            {
                if (cilindro != null)
                {
                    var ret = await dialogService.DisplayAlertAsync("Pergunta", "Confirma expedição do cilindro Nº" + cilindro.Nr_cilindro + " ?", "SIM", "NÃO");
                    if (ret)
                    {
                        reservaCilindro.Id_cilindro = cilindro.Id_cilindro;
                        reservaCilindro.Nr_cilindro = cilindro.Nr_cilindro;
                        NavigationParameters param = new NavigationParameters();
                        param.Add("RESERVACILINDRO", reservaCilindro);
                        await navigationService.GoBackAsync(param);
                    }
                }
            });
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            reservaCilindro = parameters["RESERVA"] as ReservaCilindro;
            using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
            {
                try
                {
                    //Buscar chopeiras disponiveis
                    Cilindros = new ObservableCollection<Cilindro>(await dataService.GetCilindroLivreAsync());
                }
                catch { }
            }
        }
    }
}
