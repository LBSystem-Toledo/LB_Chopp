using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB_Chopp.Models
{
    public class Cilindro: BindableBase
    {
        int _id_cilindro;
        public int Id_cilindro { get { return _id_cilindro; } set { SetProperty(ref _id_cilindro, value); } }
        string _nr_cilindro = string.Empty;
        public string Nr_cilindro { get { return _nr_cilindro; } set { SetProperty(ref _nr_cilindro, value); } }
    }
}
