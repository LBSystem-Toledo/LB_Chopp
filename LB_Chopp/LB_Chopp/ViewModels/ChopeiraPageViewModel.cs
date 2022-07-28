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
    public class ChopeiraPageViewModel : ViewModelBase
    {
        ReservaChopeira reservaChopeira { get; set; }
        ObservableCollection<Chopeira> _chopeiras;
        public ObservableCollection<Chopeira> Chopeiras { get { return _chopeiras; } set { SetProperty(ref _chopeiras, value); } }

        public DelegateCommand<Chopeira> ChopeiraCommand { get; }

        readonly IPageDialogService dialogService;
        readonly IDataService dataService;
        public ChopeiraPageViewModel(INavigationService navigationService, IPageDialogService _dialogService, IDataService _dataService)
            : base(navigationService)
        {
            dialogService = _dialogService;
            dataService = _dataService;

            ChopeiraCommand = new DelegateCommand<Chopeira>(async (chopeira) =>
            {
                if (chopeira != null)
                {
                    var ret = await dialogService.DisplayAlertAsync("Pergunta", "Confirma expedição da chopeira Nº" + chopeira.Nr_chopeira + " ?", "SIM", "NÃO");
                    if (ret)
                    {
                        reservaChopeira.Id_chopeira = chopeira.Id_chopeira;
                        reservaChopeira.Nr_chopeira = chopeira.Nr_chopeira;
                        NavigationParameters param = new NavigationParameters();
                        param.Add("RESERVACHOPEIRA", reservaChopeira);
                        await navigationService.GoBackAsync(param);
                    }
                }
            });
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            reservaChopeira = parameters["RESERVA"] as ReservaChopeira;
            using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
            {
                try
                {
                    //Buscar chopeiras disponiveis
                    Chopeiras = new ObservableCollection<Chopeira>(await dataService.GetChopeiraLivreAsync(reservaChopeira.Voltagem, reservaChopeira.Qt_torneiras));
                }
                catch { }
            }
        }
    }
}
