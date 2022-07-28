using Acr.UserDialogs;
using LB_Chopp.Interface;
using LB_Chopp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LB_Chopp.ViewModels
{
    public class NovaReservaPageViewModel : ViewModelBase
    {
        List<ReservaChopeira> ReservaChopeiras { get; set; }
        List<ReservaBarril> ReservaBarris { get; set; }

        DateTime _dt_ini = DateTime.Now;
        public DateTime Dt_ini 
        { 
            get { return _dt_ini; } 
            set 
            { 
                SetProperty(ref _dt_ini, value);
                BuscasChopeirasDisponiveisAsync();
            } 
        }
        DateTime _dt_fin = DateTime.Now;
        public DateTime Dt_fin
        {
            get { return _dt_fin; }
            set 
            { 
                SetProperty(ref _dt_fin, value);
                BuscasChopeirasDisponiveisAsync();
            }
        }
        bool _visiblecarrinho = false;
        public bool VisibleCarrinho { get { return _visiblecarrinho; } set { SetProperty(ref _visiblecarrinho, value); } }

        ObservableCollection<ChopeiraDisponivel> _chopeirasdisponiveis;
        public ObservableCollection<ChopeiraDisponivel> ChopeirasDisponiveis { get { return _chopeirasdisponiveis; }set{ SetProperty(ref _chopeirasdisponiveis, value); } }
        ObservableCollection<BarrisTipo> _barris;
        public ObservableCollection<BarrisTipo> Barris { get { return _barris; }set { SetProperty(ref _barris, value); } }
        ObservableCollection<Chopp> _chopp;
        public ObservableCollection<Chopp> Chopp { get { return _chopp; } set { SetProperty(ref _chopp, value); } }
        Chopp _choppcorrente;
        public Chopp Chopcorrente
        {
            get { return _choppcorrente; }
            set
            {
                SetProperty(ref _choppcorrente, value);
                BuscarBarrisTipoAsync();
            }
        }

        public DelegateCommand<ChopeiraDisponivel> AdicionarChopeiraCommand { get; }
        public DelegateCommand<BarrisTipo> AdicionarBarrilCommand { get; }
        public DelegateCommand VerCarrinhoCommand { get; }

        readonly IPageDialogService dialogService;
        readonly IDataService dataService;
        public NovaReservaPageViewModel(INavigationService navigationService, IPageDialogService _dialogService, IDataService _dataService)
            :base(navigationService)
        {
            dialogService = _dialogService;
            dataService = _dataService;

            ReservaChopeiras = new List<ReservaChopeira>();
            ReservaBarris = new List<ReservaBarril>();

            AdicionarChopeiraCommand = new DelegateCommand<ChopeiraDisponivel>(async (p) =>
            {
                if (p != null)
                {
                    var ret = await dialogService.DisplayAlertAsync("Pergunta", "Confirma reserva chopeira <" +
                        p.Voltagemstr.Trim() + "-" + p.Qt_torneiras.Trim() + ">?", "SIM", "NÃO");
                    if (ret)
                    {
                        ReservaChopeiras.Add(
                            new ReservaChopeira { Voltagem = p.Voltagem, Qt_torneiras = p.Qt_torneiras });
                        VisibleCarrinho = true;
                    }
                }
            });

            AdicionarBarrilCommand = new DelegateCommand<BarrisTipo>(async (p) =>
            {
                if (p != null)
                {
                    var ret = await dialogService.DisplayAlertAsync("Pergunta", "Confirma reserva barril <" +
                        p.Volume.ToString() + "LT-" + Chopcorrente.Ds_produto.Trim() + ">?", "SIM", "NÃO");
                    if (ret)
                    {
                        ReservaBarris.Add(new ReservaBarril 
                        { 
                            Volume = p.Volume, 
                            Valor = p.PrecoBarril, 
                            Cd_produto = Chopcorrente.Cd_produto, 
                            Ds_produto = Chopcorrente.Ds_produto,
                            Id_tipo = p.Id_tipo 
                        });
                        VisibleCarrinho = true;
                    }
                }
            });

            VerCarrinhoCommand = new DelegateCommand(async () =>
            {
                NavigationParameters param = new NavigationParameters();
                if(ReservaChopeiras.Count > 0)
                    param.Add("CHOPEIRAS", ReservaChopeiras);
                if (ReservaBarris.Count > 0)
                    param.Add("BARRIS", ReservaBarris);
                param.Add("DT_RESERVA", Dt_ini);
                param.Add("DT_RETORNO", Dt_fin);
                await navigationService.NavigateAsync("FecharReservaPage", param);
            });
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("RESERVACHOPEIRA"))
                ReservaChopeiras = parameters["RESERVACHOPEIRA"] as List<ReservaChopeira>;
            if (parameters.ContainsKey("RESERVABARRIL"))
                ReservaBarris = parameters["RESERVABARRIL"] as List<ReservaBarril>;
            VisibleCarrinho = ReservaChopeiras.Count > 0 || ReservaBarris.Count > 0;
            using (UserDialogs.Instance.Loading(title: string.Empty, maskType: MaskType.Black))
            {
                try
                {
                    await BuscasChopeirasDisponiveisAsync();
                    //Buscar Produtos Chopp
                    Chopp = new ObservableCollection<Chopp>(await dataService.GetChoppAsync());
                    Barris.Clear();
                }
                catch { }
            }
        }

        async Task BuscasChopeirasDisponiveisAsync()
        {
            ChopeirasDisponiveis = new ObservableCollection<ChopeiraDisponivel>(await dataService.GetChopeirasDisponiveisAsync(Dt_ini.ToString("yyyy-MM-dd"),
                                                                                                                               Dt_fin.ToString("yyyy-MM-dd")));
        }
        
        async Task BuscarBarrisTipoAsync()
        {
            if(Chopcorrente != null)
                Barris = new ObservableCollection<BarrisTipo>(await dataService.GetBarrisTiposAsync(Chopcorrente.Cd_produto));
        }
    }
}
