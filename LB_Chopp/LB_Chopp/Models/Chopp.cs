using Prism.Mvvm;

namespace LB_Chopp.Models
{
    public class Chopp: BindableBase
    {
        string _cd_produto = string.Empty;
        public string Cd_produto { get { return _cd_produto; } set { SetProperty(ref _cd_produto, value); } }
        string _ds_produto = string.Empty;
        public string Ds_produto { get { return _ds_produto; } set { SetProperty(ref _ds_produto, value); } }
    }
}
