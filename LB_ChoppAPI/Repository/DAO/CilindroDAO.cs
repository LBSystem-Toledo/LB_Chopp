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
    public class CilindroDAO: ICilindro
    {
        readonly IConfiguration _config;
        public CilindroDAO(IConfiguration config) { _config = config; }

        public async Task<IEnumerable<Cilindro>> GetCilindroLivreAsync(string Token)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(Token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select a.ID_Cilindro, a.NR_Cilindro")
                    .AppendLine("from TB_RES_Cilindro a")
                    .AppendLine("where not exists(select 1 from TB_RES_ReservaChopp x")
                    .AppendLine("					inner join TB_RES_ReservaCilindro y")
                    .AppendLine("					on x.CD_Empresa = y.CD_Empresa")
                    .AppendLine("					and x.ID_Reserva = y.ID_Reserva")
                    .AppendLine("					where ISNULL(x.ST_Registro, 'A') <> 'C'")
                    .AppendLine("					and ISNULL(y.ST_Registro, 'A') in('A', 'E')")
                    .AppendLine("					and y.ID_Cilindro = a.ID_Cilindro)");
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<Cilindro>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
    }
}
