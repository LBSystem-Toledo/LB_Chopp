using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB_Chopp.Models
{
    public class Barril: BindableBase
    {
        int _id_barril;
        public int Id_barril { get { return _id_barril; } set { SetProperty(ref _id_barril, value); } }
        string _nr_barril = string.Empty;
        public string Nr_barril { get { return _nr_barril; } set { SetProperty(ref _nr_barril, value); } }
    }
}
