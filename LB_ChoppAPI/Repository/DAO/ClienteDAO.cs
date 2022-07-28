using Dapper;
using LB_ChoppAPI.Models;
using LB_ChoppAPI.Repository.Interface;
using LB_ChoppAPI.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Repository.DAO
{
    public class ClienteDAO : ICliente
    {
        readonly IConfiguration _config;
        public ClienteDAO(IConfiguration config) { _config = config; }
        public async Task<IEnumerable<Cliente>> GetAsync(string Token, string Cd_clifor, string Nome)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(Token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select a.CD_Clifor, a.CD_Cidade, a.DS_Cidade , a.UF,")
                    .AppendLine("a.NM_Clifor, a.NM_Fantasia, a.CD_Endereco,")
                    .AppendLine("a.tp_pessoa, a.NR_CGC, a.Insc_Estadual, a.NR_CPF, a.NR_RG, ")
                    .AppendLine("dbo.FVALIDA_NUMEROS(a.cep) as cep, a.DS_Endereco, ")
                    .AppendLine("a.numero, a.bairro, a.ds_complemento, a.proximo")
                    .AppendLine("from VTB_FIN_CLIFOR a ")
                    .AppendLine("where ISNULL(a.ST_Registro, 'A') <> 'C'");
                if (!string.IsNullOrWhiteSpace(Cd_clifor))
                    sql.AppendLine("and a.cd_clifor = '" + Cd_clifor.Trim() + "'");
                if (!string.IsNullOrWhiteSpace(Nome))
                    sql.AppendLine("and (a.nm_clifor like '%" + Nome + "%' or a.nm_fantasia like '%" + Nome + "%')");

                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<Cliente>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
    }
}
