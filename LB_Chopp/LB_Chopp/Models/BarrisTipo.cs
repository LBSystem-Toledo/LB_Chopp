using Prism.Mvvm;
using System.Drawing;

namespace LB_Chopp.Models
{
    public class BarrisTipo: BindableBase
    {
        int _id_volume;
        public int Id_volume { get{ return _id_volume;} set { SetProperty(ref _id_volume, value); } }
        int _id_tipo;
        public int Id_tipo { get { return _id_tipo; } set { SetProperty(ref _id_tipo, value); } }
        int _volume;
        public int Volume { get { return _volume; } set { SetProperty(ref _volume, value); } }
        string _ds_tipo = string.Empty;
        public string Ds_tipo { get { return _ds_tipo; } set { SetProperty(ref _ds_tipo, value); } }
        int _cheio;
        public int Cheio { get { return _cheio; } set { SetProperty(ref _cheio, value); } }
        decimal _preco = decimal.Zero;
        public decimal Preco { get { return _preco; } set { SetProperty(ref _preco, value); } }
        public decimal PrecoBarril => Volume * Preco;
        public Color CorBotao => Cheio > 0 ? Color.FromArgb(240,139,41) : Color.FromArgb(243,248,252);
    }
}
