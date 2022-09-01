using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB_Chopp.Models
{
    public class ReservaCilindro: BindableBase
    {
        public int ID { get; set; }
        string _cd_empresa = string.Empty;
        public string Cd_empresa { get { return _cd_empresa; } set { SetProperty(ref _cd_empresa, value); } }
        int _id_reserva;
        public int Id_reserva { get { return _id_reserva; } set { SetProperty(ref _id_reserva, value); } }
        int _id_item;
        public int Id_item { get { return _id_item; } set{ SetProperty(ref _id_item, value); } }
        int _id_cilindro;
        public int Id_cilindro { get { return _id_cilindro; } set { SetProperty(ref _id_cilindro, value); } }
        string _nr_cilindro = string.Empty;
        public string Nr_cilindro { get { return _nr_cilindro; } set { SetProperty(ref _nr_cilindro, value); } }
        bool _coletar = false;
        public bool Coletar { get { return _coletar; } set { SetProperty(ref _coletar, value); } }
    }
}
