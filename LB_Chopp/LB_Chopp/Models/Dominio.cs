using Prism.Mvvm;

namespace LB_Chopp.Models
{
    public class Dominio: BindableBase
    {
        public string Chave { get; set; } = string.Empty;
        private string _valor = string.Empty;
        public string Valor { get { return _valor; } set { SetProperty(ref _valor, value); } }
    }
}
