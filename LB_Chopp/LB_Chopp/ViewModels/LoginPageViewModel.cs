using Acr.UserDialogs;
using LB_Chopp.Interface;
using LB_Chopp.Models;
using LB_Chopp.Service;
using LB_Chopp.Utils;
using Plugin.DeviceInfo;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LB_Chopp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private string _login = string.Empty;
        public string Login { get { return _login.ToUpper(); } set { SetProperty(ref _login, value); } }
        private string _senha = string.Empty;
        public string Senha { get { return _senha; } set { SetProperty(ref _senha, value); } }
        public string _cnpj = string.Empty;
        public string Cnpj { get { return _cnpj; } set { SetProperty(ref _cnpj, value); } }
        public DelegateCommand LoginCommand { get; }
        public DelegateCommand SairCommand { get; }

        readonly IPageDialogService dialogService;
        readonly IDataService dataService;
        public LoginPageViewModel(INavigationService navigationService, IPageDialogService _dialogService, IDataService _dataService)
            : base(navigationService)
        {
            dialogService = _dialogService;
            dataService = _dataService;

            LoginCommand = new DelegateCommand(async () =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(Login))
                    {
                        await dialogService.DisplayAlertAsync("Mensagem", "Obrigatório informar LOGIN.", "OK");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(Senha))
                    {
                        await dialogService.DisplayAlertAsync("Mensagem", "Obrigatório informar SENHA.", "OK");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(Cnpj))
                    {
                        await dialogService.DisplayAlertAsync("Mensagem", "Obrigatório informar CNPJ.", "OK");
                        return;
                    }
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
                        {
                            App.config = await dataService.LoginAsync(Cnpj, Login, Senha);
                            if (App.config == null ? false : !string.IsNullOrWhiteSpace(App.config.Login))
                            {
                                App.config.Login = Login;
                                //Validar device
                                TerminalMobile terminal = new TerminalMobile();
                                terminal.Cd_empresa = App.config.Cd_empresa;
                                terminal.Cnpj = App.config.Cnpj;
                                terminal.IdDevice = CrossDeviceInfo.Current.Id;
                                if (await dataService.ValidarTerminalAsync(terminal))
                                {
                                    Arquivo.SetValues(Login, Senha, Cnpj);
                                    await NavigationService.NavigateAsync(new Uri("/MenuPage/NavigationPage/NovaReservaPage", System.UriKind.Relative));
                                }
                                else await dialogService.DisplayAlertAsync("Mensagem", "Erro ao validar terminal MOBILE.\r\n" +
                                                                           "Entre em contato com o suporte tecnico e informe este código<" + CrossDeviceInfo.Current.Id + ">.",
                                                                           "OK");
                            }
                            else
                                await dialogService.DisplayAlertAsync("Mensagem", "Credenciais invalidas.", "OK");
                        }
                    }
                    else await dialogService.DisplayAlertAsync("Mensagem", "Sem conexão com internet.", "OK");
                }
                catch (Exception ex)
                { await dialogService.DisplayAlertAsync("Erro", ex.Message.Trim(), "OK"); }
            });

            SairCommand = new DelegateCommand(() => DependencyService.Get<ICloseApplication>()?.closeApplication());
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Usuario u = Arquivo.GetValues();
            if (u != null)
            {
                Login = u.Login;
                Senha = u.Senha;
                Cnpj = u.Cnpj;
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
                    {
                        App.config = await dataService.LoginAsync(Cnpj, Login, Senha);
                        if (App.config == null ? false : !string.IsNullOrWhiteSpace(App.config.Login))
                        {
                            App.config.Login = Login;
                            //Validar device
                            TerminalMobile terminal = new TerminalMobile();
                            terminal.Cd_empresa = App.config.Cd_empresa;
                            terminal.Cnpj = App.config.Cnpj;
                            terminal.IdDevice = CrossDeviceInfo.Current.Id;
                            if (await dataService.ValidarTerminalAsync(terminal))
                            {
                                Arquivo.SetValues(Login, Senha, Cnpj);
                                await NavigationService.NavigateAsync(new Uri("/MenuPage/NavigationPage/NovaReservaPage", System.UriKind.Relative));
                            }
                        }
                    }
                }
                else await dialogService.DisplayAlertAsync("Mensagem", "Sem conexão com internet.", "OK");
            }
        }
    }
}
