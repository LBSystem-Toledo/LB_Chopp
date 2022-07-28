using System;
using System.Collections.Generic;
using System.Text;

namespace LB_Chopp.Models
{
    public class ChopeiraDisponivel
    {
        public string Voltagem { get; set; } = string.Empty;
        public string Voltagemstr
        {
            get
            {
                if (Voltagem.Trim().Equals("0"))
                    return "110";
                else if (Voltagem.Trim().Equals("1"))
                    return "220";
                else if (Voltagem.Trim().Equals("2"))
                    return "GELO";
                else return string.Empty;
            }
        }
        public string Qt_torneiras { get; set; } = string.Empty;
        public int Qt_chopeira { get; set; }
        public int Qt_reservada { get; set; }
        public int Disponivel => Qt_chopeira - Qt_reservada;
    }
}
