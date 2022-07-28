using Acr.UserDialogs;
using LB_Chopp.Interface;
using LB_Chopp.Models;
using LB_Chopp.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LB_Chopp.ViewModels
{
    public class ConsultaClientePageViewModel : ViewModelBase
    {
        private string _filtrocliente = string.Empty;
        public string Filtrocliente { get { return _filtrocliente; } set { SetProperty(ref _filtrocliente, value); } }
        private ObservableCollection<Cliente> _clientes;
        public ObservableCollection<Cliente> Clientes { get { return _clientes; } set { SetProperty(ref _clientes, value); } }

        public DelegateCommand BuscarCommand { get; }
        public DelegateCommand<Cliente> SelecionarCommand { get; }

        readonly IPageDialogService dialogService;
        readonly IDataService dataService;
        public ConsultaClientePageViewModel(INavigationService navigationService, IPageDialogService _dialogService, IDataService _dataService)
            :base(navigationService)
        {
            dialogService = _dialogService;
            dataService = _dataService;

            BuscarCommand = new DelegateCommand(async() =>
            {
                try
                {
                    using(UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
                    {
                        IEnumerable<Cliente> lista = await dataService.GetClienteAsync(string.Empty, Filtrocliente);
                        Clientes = new ObservableCollection<Cliente>(lista.OrderBy(p => p.Nm_clifor));
                    }
                }
                catch { }
            });
            SelecionarCommand = new DelegateCommand<Cliente>(async (Cliente c) =>
            {
                if (c != null)
                {
                    NavigationParameters param = new NavigationParameters();
                    param.Add("CLIENTE", c);
                    await NavigationService.GoBackAsync(param);
                }
                else await NavigationService.GoBackAsync();
            });
        }
    }
}
