using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Models
{
    public class Cliente
    {
        public string Cd_clifor { get; set; } = string.Empty;
        public string Cd_cidade { get; set; } = string.Empty;
        public string Ds_cidade { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
        public string Nm_clifor { get; set; } = string.Empty;
        public string Nm_fantasia { get; set; } = string.Empty;
        public string Tp_pessoa { get; set; } = string.Empty;
        public string Nr_cgc { get; set; } = string.Empty;
        public string Insc_estadual { get; set; } = string.Empty;
        public string St_naocontribuinte { get; set; } = string.Empty;
        public string Nr_cpf { get; set; } = string.Empty;
        public string Nr_rg { get; set; } = string.Empty;
        public string Cd_endereco { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string Ds_endereco { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Ds_complemento { get; set; } = string.Empty;
        public string Proximo { get; set; } = string.Empty;
    }
}
