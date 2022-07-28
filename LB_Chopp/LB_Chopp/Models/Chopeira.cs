using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB_Chopp.Models
{
    public class Chopeira: BindableBase
    {
        int _id_chopeira;
        public int Id_chopeira { get { return _id_chopeira; } set { SetProperty(ref _id_chopeira, value); } }
        string _nr_chopeira = string.Empty;
        public string Nr_chopeira { get { return _nr_chopeira; } set { SetProperty(ref _nr_chopeira, value); } }
    }
}
