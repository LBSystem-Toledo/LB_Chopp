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
    public class FecharReservaPageViewModel : ViewModelBase
    {
        DateTime Dt_reserva { get; set; }
        DateTime Dt_retorno { get; set; }

        decimal _total = decimal.Zero;
        public decimal Total { get { return _total; } set { SetProperty(ref _total, value); } }
        decimal _desconto = decimal.Zero;
        public decimal Desconto { get { return _desconto; } set { SetProperty(ref _desconto, value); } }
        decimal _taxa = decimal.Zero;
        public decimal Taxa { get { return _taxa; } set { SetProperty(ref _taxa, value); } }
        Cliente _cliente;
        public Cliente Cliente { get { return _cliente; } set { SetProperty(ref _cliente, value); } }
        ReservaChopp _reserva;
        public ReservaChopp Reserva { get { return _reserva; } set { SetProperty(ref _reserva, value); } }
        ObservableCollection<ReservaChopeira> _chopeiras;
        public ObservableCollection<ReservaChopeira> Chopeiras { get { return _chopeiras; } set { SetProperty(ref _chopeiras, value); } }
        ObservableCollection<ReservaBarril> _barris;
        public ObservableCollection<ReservaBarril> Barris 
        { 
            get { return _barris; } 
            set 
            { 
                SetProperty(ref _barris, value);
                Total = _barris.Sum(p => p.Valor);
            } 
        }

        public DelegateCommand SalvarCommand { get; }
        public DelegateCommand CancelarCommand { get; }
        public DelegateCommand BuscarClienteCommand { get; }
        public DelegateCommand LimparCliente { get; }
        public DelegateCommand<ReservaChopeira> ExcluirChopeiraCommand { get; }
        public DelegateCommand<ReservaBarril> ExcluirBarrilCommand { get; }

        readonly IPageDialogService dialogService;
        readonly IDataService dataService;
        public FecharReservaPageViewModel(INavigationService navigationService, IPageDialogService _dialogService, IDataService _dataService)
            :base(navigationService)
        {
            dialogService = _dialogService;
            dataService = _dataService;
            Reserva = new ReservaChopp();

            SalvarCommand = new DelegateCommand(async () =>
            {
                if (Chopeiras.Count.Equals(0) &&
                    Barris.Count.Equals(0) &&
                    Reserva.QtCilindros.Equals(0))
                {
                    await dialogService.DisplayAlertAsync("Mensagem", "Obrigatório informar pelo menos um item para reserva.", "OK");
                    return;
                }
                if (Cliente == null)
                {
                    await dialogService.DisplayAlertAsync("Mensagem", "Obrigatório informar cliente.", "OK");
                    return;
                }
                Reserva.Cd_empresa = App.config.Cd_empresa;
                Reserva.Dt_reserva = Dt_reserva;
                Reserva.Dt_prevretorno = Dt_retorno;
                Reserva.Cd_clifor = Cliente.Cd_clifor;
                Reserva.Cd_endereco = Cliente.Cd_endereco;
                Reserva.Chopeiras = Chopeiras.ToList();
                Reserva.Barris = Barris.ToList();
                if(Barris.Count > 0)
                {
                    if(Desconto > decimal.Zero)
                    {
                        decimal rateio = Desconto / Barris.Count;
                        Barris.ToList().ForEach(p => p.Vl_desconto = rateio);
                        Barris.Last().Vl_desconto += Desconto - Barris.Sum(p => p.Vl_desconto);
                    }
                    if (Taxa > decimal.Zero)
                    {
                        decimal rateio = Taxa / Barris.Count;
                        Barris.ToList().ForEach(p => p.Vl_frete = rateio);
                        Barris.Last().Vl_frete += Taxa - Barris.Sum(p => p.Vl_frete);
                    }
                }
                using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
                {
                    try
                    {
                        if (await dataService.GravarReservaAsync(Reserva))
                        {
                            await dialogService.DisplayAlertAsync("Mensagem", "Reserva gravada com sucesso.", "OK");
                            Chopeiras.Clear();
                            Barris.Clear();
                            await navigationService.GoBackAsync();
                        }
                        else await dialogService.DisplayAlertAsync("Erro", "Erro ao gravar reserva.", "OK");
                    }
                    catch (Exception ex) { await dialogService.DisplayAlertAsync("Erro", ex.Message.Trim(), "OK"); }
                }
            });
            CancelarCommand = new DelegateCommand(async () =>
            {
                var ret = await dialogService.DisplayAlertAsync("Pergunta", "Limpar carrinho?", "SIM", "NÃO");
                if(ret)
                {
                    Chopeiras.Clear();
                    Barris.Clear();
                    await navigationService.GoBackAsync();
                }
            });
            ExcluirChopeiraCommand = new DelegateCommand<ReservaChopeira>(async (ReservaChopeira c) =>
            {
                if (c != null)
                {
                    var ret = await dialogService.DisplayAlertAsync("Pergunta", "Confirma exclusão da chopeira?", "SIM", "NÃO");
                    if (ret)
                        Chopeiras.Remove(c);
                }
            });
            ExcluirBarrilCommand = new DelegateCommand<ReservaBarril>(async (ReservaBarril b) =>
            {
                if(b != null)
                {
                    var ret = await dialogService.DisplayAlertAsync("Pergunta", "Confirma exclusão do barril?", "SIM", "NÃO");
                    if (ret)
                        Barris.Remove(b);
                }
            });
            LimparCliente = new DelegateCommand(() => Cliente = null);
            BuscarClienteCommand = new DelegateCommand(async () =>
            {
                await NavigationService.NavigateAsync("ConsultaClientePage");
            });
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("CHOPEIRAS"))
            {
                Chopeiras = new ObservableCollection<ReservaChopeira>(parameters["CHOPEIRAS"] as List<ReservaChopeira>);
                Reserva.QtCilindros = Chopeiras.Count;
            }
            if (parameters.ContainsKey("BARRIS"))
                Barris = new ObservableCollection<ReservaBarril>(parameters["BARRIS"] as List<ReservaBarril>);
            if (parameters.ContainsKey("CLIENTE"))
            {
                Cliente = parameters["CLIENTE"] as Cliente;
                Reserva.Logradouro_ent = Cliente.Ds_endereco;
                Reserva.Numero_ent = Cliente.Numero;
                Reserva.Bairro_ent = Cliente.Bairro;
                Reserva.Proximo_ent = Cliente.Proximo;
                Reserva.Complemento_ent = Cliente.Ds_complemento;
            }
            if (parameters.ContainsKey("DT_RESERVA"))
                Dt_reserva = (DateTime)parameters["DT_RESERVA"];
            if (parameters.ContainsKey("DT_RETORNO"))
                Dt_retorno = (DateTime)parameters["DT_RETORNO"];
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add("RESERVACHOPEIRA", Chopeiras.ToList());
            parameters.Add("RESERVABARRIL", Barris.ToList());
        }
    }
}
