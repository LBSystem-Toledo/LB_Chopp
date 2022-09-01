using LB_Chopp.Interface;
using LB_Chopp.Models;
using LB_Chopp.Service;
using LB_Chopp.ViewModels;
using LB_Chopp.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace LB_Chopp
{
    public partial class App
    {
        #if (DEBUG)
            public const string url_api = "http://192.168.1.105:45455";
        //public const string url_api = "http://cloud.lbsystemsoftware.com.br:33209/chopp";
        #else
            public const string url_api = "http://cloud.lbsystemsoftware.com.br:33209/chopp";
            //public const string url_api = "http://192.168.1.11:45455";
        #endif

        static AcessoDB database;
        public static AcessoDB Database
        {
            get
            {
                if (database == null)
                    database = new AcessoDB(Xamarin.Essentials.FileSystem.AppDataDirectory);
                return database;
            }
        }

        public static TokenVendedor config { get; set; }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            DevExpress.XamarinForms.CollectionView.Initializer.Init();

            InitializeComponent();

            await NavigationService.NavigateAsync("/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterSingleton<IDataService, DataService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
            containerRegistry.RegisterForNavigation<BarrilPage, BarrilPageViewModel>();
            containerRegistry.RegisterForNavigation<ChopeiraPage, ChopeiraPageViewModel>();
            containerRegistry.RegisterForNavigation<CilindroPage, CilindroPageViewModel>();
            containerRegistry.RegisterForNavigation<ConsultaClientePage, ConsultaClientePageViewModel>();
            containerRegistry.RegisterForNavigation<ExpedicaoChoppPage, ExpedicaoChoppPageViewModel>();
            containerRegistry.RegisterForNavigation<ExpedirPage, ExpedirPageViewModel>();
            containerRegistry.RegisterForNavigation<ReservasColetarPage, ReservasColetarPageViewModel>();
            containerRegistry.RegisterForNavigation<ColetarPage, ColetarPageViewModel>();
            containerRegistry.RegisterForNavigation<ProrrogarColetaPage, ProrrogarColetaPageViewModel>();
            containerRegistry.RegisterForNavigation<ImagemPage, ImagemPageViewModel>();
            containerRegistry.RegisterForNavigation<NovaReservaPage, NovaReservaPageViewModel>();
            containerRegistry.RegisterForNavigation<FecharReservaPage, FecharReservaPageViewModel>();
        }
    }
}
