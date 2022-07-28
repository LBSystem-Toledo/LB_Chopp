using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB_Chopp.Models
{
    public class ReservaBarril: BindableBase
    {
        string _cd_empresa = string.Empty;
        public string Cd_empresa { get { return _cd_empresa; } set { SetProperty(ref _cd_empresa, value); } }
        int _id_reserva;
        public int Id_reserva { get { return _id_reserva; } set { SetProperty(ref _id_reserva, value); } }
        int _id_item;
        public int Id_item { get { return _id_item; } set { SetProperty(ref _id_item, value); } }
        string _cd_produto = string.Empty;
        public string Cd_produto { get { return _cd_produto; } set { SetProperty(ref _cd_produto, value); } }
        string _ds_produto = string.Empty;
        public string Ds_produto { get { return _ds_produto; } set { SetProperty(ref _ds_produto, value); } }
        int _id_barril;
        public int Id_barril { get { return _id_barril; } set { SetProperty(ref _id_barril, value); } }
        string _nr_barril = string.Empty;
        public string Nr_barril { get { return _nr_barril; } set { SetProperty(ref _nr_barril, value); } }
        int _id_tipo;
        public int Id_tipo { get { return _id_tipo; } set { SetProperty(ref _id_tipo, value); } }
        string _ds_tipo = string.Empty;
        public string Ds_tipo { get { return _ds_tipo; } set { SetProperty(ref _ds_tipo, value); } }
        int _volume;
        public int Volume { get { return _volume; } set { SetProperty(ref _volume, value); } }
        decimal _valor = decimal.Zero;
        public decimal Valor { get { return _valor; } set { SetProperty(ref _valor, value); } }
        public bool RetornouCheio { get; set; } = false;
        decimal _vl_desconto = decimal.Zero;
        public decimal Vl_desconto { get { return _vl_desconto; } set { SetProperty(ref _vl_desconto, value); } }
        decimal _vl_frete = decimal.Zero;
        public decimal Vl_frete { get { return _vl_frete; } set { SetProperty(ref _vl_frete, value); } }
    }
}
