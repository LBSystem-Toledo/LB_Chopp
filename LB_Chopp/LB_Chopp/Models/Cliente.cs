using LB_Chopp.Utils;
using System;

namespace LB_Chopp.Models
{
    public class ConsultaCNPJ
    {
        public string Nome { get; set; } = string.Empty;
        public string Fantasia { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class TipoPessoa
    {
        public string Tp_pessoa { get; set; } = string.Empty;
        public string Tipo_pessoa { get; set; } = string.Empty;
    }
    public class Cliente: Prism.Mvvm.BindableBase
    {
        private string _cd_clifor = string.Empty;
        public string Cd_clifor { get { return _cd_clifor; } set { SetProperty(ref _cd_clifor, value); } }
        private string _nm_clifor = string.Empty;
        public string Nm_clifor { get { return _nm_clifor; } set { SetProperty(ref _nm_clifor, value); } }
        private string _nm_fantasia = string.Empty;
        public string Nm_fantasia { get { return _nm_fantasia; } set { SetProperty(ref _nm_fantasia, value); } }
        private string _tp_pessoa = string.Empty;
        public string Tp_pessoa { get { return _tp_pessoa; } set { SetProperty(ref _tp_pessoa, value); } }
        private string _nr_cgc = string.Empty;
        public string Nr_cgc { get { return _nr_cgc.FormatarCNPJ(); } set { SetProperty(ref _nr_cgc, value); } }
        private string _inscestadual = string.Empty;
        public string Insc_Estadual { get { return _inscestadual; } set { SetProperty(ref _inscestadual, value); } }
        private string _nr_cpf = string.Empty;
        public string Nr_cpf { get { return _nr_cpf.FormatarCPF(); } set { SetProperty(ref _nr_cpf, value); } }
        public string CnpjCpf { get { return Tp_pessoa.Trim().ToUpper().Equals("F") ? Nr_cpf.FormatarCPF() : Nr_cgc.FormatarCNPJ(); } }
        private string _nr_rg = string.Empty;
        public string Nr_rg { get { return _nr_rg; } set { SetProperty(ref _nr_rg, value); } }
        public string Cd_endereco { get; set; } = string.Empty;
        private string _cep = string.Empty;
        public string Cep { get { return _cep; } set { SetProperty(ref _cep, value); } }
        private string _ds_endereco = string.Empty;
        public string Ds_endereco { get { return _ds_endereco; } set { SetProperty(ref _ds_endereco, value); } }
        private string _numero = string.Empty;
        public string Numero { get { return _numero; } set { SetProperty(ref _numero, value); } }
        private string _bairro = string.Empty;
        public string Bairro { get { return _bairro; } set { SetProperty(ref _bairro, value); } }
        private string _ds_complemento = string.Empty;
        public string Ds_complemento { get { return _ds_complemento; } set { SetProperty(ref _ds_complemento, value); } }
        private string _proximo = string.Empty;
        public string Proximo { get { return _proximo; } set { SetProperty(ref _proximo, value); } }
        private string _cd_cidade = string.Empty;
        public string Cd_cidade { get { return _cd_cidade; } set { SetProperty(ref _cd_cidade, value); } }
        private string _ds_cidade = string.Empty;
        public string Ds_cidade { get { return _ds_cidade; } set { SetProperty(ref _ds_cidade, value); } }
        private string _uf = string.Empty;
        public string UF { get { return _uf; } set { SetProperty(ref _uf, value); } }
    }
}
