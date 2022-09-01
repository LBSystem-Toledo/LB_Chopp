using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB_Chopp.Models
{
    public class ReservaChopeira: BindableBase
    {
        public int ID { get; set; }
        string _cd_empresa = string.Empty;
        public string Cd_empresa { get { return _cd_empresa; } set { SetProperty(ref _cd_empresa, value); } }
        int _id_reserva;
        public int Id_reserva { get { return _id_reserva; } set { SetProperty(ref _id_reserva, value); } }
        int _id_item;
        public int Id_item { get { return _id_item; } set { SetProperty(ref _id_item, value); } }
        int _id_chopeira;
        public int Id_chopeira { get { return _id_chopeira; } set { SetProperty(ref _id_chopeira, value); } }
        string _nr_chopeira = string.Empty;
        public string Nr_chopeira { get { return _nr_chopeira; } set { SetProperty(ref _nr_chopeira, value); } }
        string _voltagem = string.Empty;
        public string Voltagem { get { return _voltagem; } set { SetProperty(ref _voltagem, value); } }
        public string Voltagemstr
        {
            get
            {
                if (Voltagem.Equals("0"))
                    return "110";
                else if (Voltagem.Equals("1"))
                    return "220";
                else if (Voltagem.Equals("2"))
                    return "GELO";
                else return string.Empty;
            }
        }
        string _qt_torneiras = string.Empty;
        public string Qt_torneiras { get { return _qt_torneiras; } set { SetProperty(ref _qt_torneiras, value); } }
        bool _coletar = false;
        public bool Coletar { get { return _coletar; } set { SetProperty(ref _coletar, value); } }
    }
}
