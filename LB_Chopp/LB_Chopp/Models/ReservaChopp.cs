using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB_Chopp.Models
{
    public class ReservaChopp: BindableBase
    {
        public int ID { get; set; }
        string _cd_empresa = string.Empty; 
        public string Cd_empresa { get { return _cd_empresa; } set { SetProperty(ref _cd_empresa, value); } }
        int _id_reserva;
        public int Id_reserva { get { return _id_reserva; } set { SetProperty(ref _id_reserva, value); } }
        string _cd_clifor = string.Empty;
        public string Cd_clifor { get { return _cd_clifor; } set { SetProperty(ref _cd_clifor, value); } }
        string _nm_clifor = string.Empty;
        public string Nm_clifor { get { return _nm_clifor; } set { SetProperty(ref _nm_clifor, value); } }
        string _cd_endereco = string.Empty;
        public string Cd_endereco { get { return _cd_endereco; } set { SetProperty(ref _cd_endereco, value); } }
        DateTime? _dt_reserva = null;
        public DateTime? Dt_reserva { get { return _dt_reserva; } set { SetProperty(ref _dt_reserva, value); } }
        DateTime? _dt_prevretorno = null;
        public DateTime? Dt_prevretorno { get { return _dt_prevretorno; } set { SetProperty(ref _dt_prevretorno, value); } }
        string _logradouro_ent = string.Empty;
        public string Logradouro_ent { get { return _logradouro_ent; } set { SetProperty(ref _logradouro_ent, value); } }
        string _numero_ent = string.Empty;
        public string Numero_ent { get { return _numero_ent; } set { SetProperty(ref _numero_ent, value); } }
        string _bairro_ent = string.Empty;
        public string Bairro_ent { get { return _bairro_ent; } set { SetProperty(ref _bairro_ent, value); } }
        string _complemento_ent = string.Empty;
        public string Complemento_ent { get { return _complemento_ent; } set { SetProperty(ref _complemento_ent, value); } }
        string _proximo_ent = string.Empty;
        public string Proximo_ent { get { return _proximo_ent; } set { SetProperty(ref _proximo_ent, value); } }
        string _obs = string.Empty;
        public string Obs { get { return _obs; } set { SetProperty(ref _obs, value); } }
        public DateTime? Dt_prevretornoOld { get; set; } = null;
        string _motivo = string.Empty;
        public string Motivo { get { return _motivo; } set { SetProperty(ref _motivo, value); } }
        int _qtcilindros = 0;
        public int QtCilindros { get { return _qtcilindros; } set { SetProperty(ref _qtcilindros, value); } }
        public IEnumerable<ReservaChopeira> Chopeiras { get; set; } = Enumerable.Empty<ReservaChopeira>();
        public IEnumerable<ReservaBarril> Barris { get; set; } = Enumerable.Empty<ReservaBarril>();
        public IEnumerable<ReservaCilindro> Cilindros { get; set; } = Enumerable.Empty<ReservaCilindro>();
    }
}
