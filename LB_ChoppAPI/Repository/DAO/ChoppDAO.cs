using Dapper;
using LB_ChoppAPI.Models;
using LB_ChoppAPI.Repository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Repository.DAO
{
    public class ChoppDAO: IChopp
    {
        readonly IConfiguration _config;
        public ChoppDAO(IConfiguration config) { _config = config; }

        public async Task<IEnumerable<Chopp>> GetChoppAsync(string Token)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(Token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select a.CD_Produto, a.DS_Produto")
                    .AppendLine("from TB_EST_Produto a")
                    .AppendLine("inner join TB_EST_TpProduto b")
                    .AppendLine("on a.TP_Produto = b.TP_Produto")
                    .AppendLine("where ISNULL(a.ST_Registro, 'A') <> 'C'")
                    .AppendLine("and ISNULL(b.ST_BarrilChopp, 0) = 1");
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<Chopp>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
    }
}
