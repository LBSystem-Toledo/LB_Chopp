using System;
using System.Collections.Generic;
using System.Linq;

namespace LB_ChoppAPI.Models
{
    public class ReservaChopp
    {
        public string Cd_empresa { get; set; } = string.Empty;
        public int Id_reserva { get; set; }
        public string Cd_clifor { get; set; } = string.Empty;
        public string Nm_clifor { get; set; } = string.Empty;
        public string Cd_endereco { get; set; } = string.Empty;
        public DateTime? Dt_reserva { get; set; } = null;
        public DateTime? Dt_prevretorno { get; set; } = null;
        public string Logradouro_ent { get; set; } = string.Empty;
        public string Numero_ent { get; set; } = string.Empty;
        public string Bairro_ent { get; set; } = string.Empty;
        public string Complemento_ent { get; set; } = string.Empty;
        public string Proximo_ent { get; set; } = string.Empty;
        public DateTime? Dt_prevretornoOld { get; set; } = null;
        public string Motivo { get; set; } = string.Empty;
        public string Obs { get; set; } = string.Empty;
        public int QtCilindros { get; set; }
        public IEnumerable<ReservaChopeira> Chopeiras { get; set; } = Enumerable.Empty<ReservaChopeira>();
        public IEnumerable<ReservaBarril> Barris { get; set; } = Enumerable.Empty<ReservaBarril>();
        public IEnumerable<ReservaCilindro> Cilindros { get; set; } = Enumerable.Empty<ReservaCilindro>();
    }
}
