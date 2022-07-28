using LB_Chopp.Interface;
using LB_Chopp.Utils;
using Prism.Commands;
using Prism.Navigation;
using System;
using Xamarin.Forms;

namespace LB_Chopp.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        private string _login = string.Empty;
        public string Login { get { return _login.ToUpper(); } set { SetProperty(ref _login, value); } }
        
        public DelegateCommand ReservaCommand { get; }
        public DelegateCommand ExpedicaoCommand { get; }
        public DelegateCommand ColetaCommand { get; }
        public DelegateCommand SairCommand { get; }

        public MenuPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            if (App.config != null)
                Login = App.config.Login;

            ReservaCommand = new DelegateCommand(async()=> await NavigationService.NavigateAsync(new Uri("/MenuPage/NavigationPage/NovaReservaPage", System.UriKind.Relative)));
            ExpedicaoCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync(new Uri("/MenuPage/NavigationPage/ExpedicaoChoppPage", System.UriKind.Relative)));
            ColetaCommand = new DelegateCommand(async () => await NavigationService.NavigateAsync(new Uri("/MenuPage/NavigationPage/ReservasColetarPage", System.UriKind.Relative)));

            SairCommand = new DelegateCommand(() =>
            {
                Arquivo.DeleteFile();
                DependencyService.Get<ICloseApplication>()?.closeApplication();
            });
        }
    }
}
