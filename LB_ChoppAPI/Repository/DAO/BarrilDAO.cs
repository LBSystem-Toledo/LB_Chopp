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
    public class BarrilDAO: IBarril
    {
        readonly IConfiguration _config;
        public BarrilDAO(IConfiguration config) { _config = config; }

        public async Task<IEnumerable<Barril>> GetBarrilLivreAsync(string Token,
                                                                   string Cd_produto,
                                                                   int Volume)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(Token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select a.Id_barril, a.nr_barril")
                    .AppendLine("from VTB_RES_BARRIL a")
                    .AppendLine("where ISNULL(a.cancelado, 0) <> 1")
                    .AppendLine("and not exists(select 1 from TB_RES_ReservaChopp x")
                    .AppendLine("				inner join TB_RES_ReservaBarril y")
                    .AppendLine("				on x.CD_Empresa = y.CD_Empresa")
                    .AppendLine("				and x.ID_Reserva = y.ID_Reserva")
                    .AppendLine("				where ISNULL(x.ST_Registro, 'A') <> 'C'")
                    .AppendLine("				and ISNULL(y.ST_Registro, 'A') in('A', 'E')")
                    .AppendLine("				and y.Id_barril = a.Id_barril")
                    .AppendLine("				and y.ID_Tipo = a.id_tipo)")
                    .AppendLine("and a.Cd_produto = '" + Cd_produto.Trim() + "'")
                    .AppendLine("and a.volume = " + Volume);
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<Barril>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<BarrisTipo>> GetBarrisTiposAsync(string token,
                                                                       string cd_produto)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("with barris as")
                    .AppendLine("(")
                    .AppendLine("	select distinct a.ID_Volume, a.ID_Tipo,")
                    .AppendLine("	b.Volume, c.DS_Tipo ")
                    .AppendLine("	from TB_RES_Barril a")
                    .AppendLine("	inner join TB_RES_VolumeBarril b")
                    .AppendLine("	on a.ID_Volume = b.ID_Volume")
                    .AppendLine("	and a.ID_Tipo = b.ID_Tipo")
                    .AppendLine("	inner join TB_RES_TipoBarril c")
                    .AppendLine("	on a.ID_Tipo = c.ID_Tipo")
                    .AppendLine("	where a.Cancelado = 0")
                    .AppendLine(")")
                    .AppendLine("select ID_Volume, ID_Tipo,")
                    .AppendLine("Volume, DS_Tipo,")
                    .AppendLine("Cheio = (select COUNT(x.Id_barril)")
                    .AppendLine("				from VTB_RES_BARRIL x")
                    .AppendLine("				where x.id_volume = a.ID_Volume")
                    .AppendLine("				and x.id_tipo = a.ID_Tipo")
                    .AppendLine("				and x.Cd_produto = '" + cd_produto.Trim() + "') -")
                    .AppendLine("		(select count(x.id_barril)")
                    .AppendLine("				from TB_RES_ReservaBarril x")
                    .AppendLine("                inner join TB_RES_ReservaChopp y")
                    .AppendLine("                on x.CD_Empresa = y.CD_Empresa")
                    .AppendLine("                and x.ID_Reserva = y.ID_Reserva")
                    .AppendLine("                and ISNULL(y.ST_Registro, 'A') <> 'C'")
                    .AppendLine("				inner join TB_RES_Barril z")
                    .AppendLine("				on x.Id_barril = z.Id_barril")
                    .AppendLine("				inner join VTB_DIV_EMPRESA w")
                    .AppendLine("				on y.CD_Empresa = w.CD_Empresa")
                    .AppendLine("                where z.ID_Volume = a.ID_Volume")
                    .AppendLine("                and z.ID_Tipo = a.ID_Tipo")
                    .AppendLine("				and x.CD_Produto = '" + cd_produto.Trim() + "'")
                    .AppendLine("				and dbo.FVALIDA_NUMEROS(case when w.tp_pessoa = 'F' then w.NR_CPF else w.NR_CGC end) = '" + _conexaostr + "'")
                    .AppendLine("                and isnull(x.ST_Registro, 'A') in('E')),")
                    .AppendLine("Preco = ISNULL((select top 1 x.PrecoVenda")
                    .AppendLine("				from TB_RES_PrecoBarril x")
                    .AppendLine("				inner join VTB_DIV_EMPRESA y")
                    .AppendLine("				on x.CD_Empresa = y.CD_Empresa")
                    .AppendLine("				inner join TB_RES_Config z")
                    .AppendLine("				on z.CD_Empresa = x.CD_Empresa")
                    .AppendLine("				and x.CD_TabelaPreco = x.CD_TabelaPreco")
                    .AppendLine("				where x.ID_Volume = a.ID_Volume")
                    .AppendLine("				and x.ID_Tipo = a.ID_Tipo")
                    .AppendLine("				and x.CD_Produto = '" + cd_produto.Trim() + "'")
                    .AppendLine("				and dbo.FVALIDA_NUMEROS(case when y.tp_pessoa = 'F' then y.NR_CPF else y.NR_CGC end) = '" + _conexaostr + "'), 0)")
                    .AppendLine("from barris a");
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<BarrisTipo>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
    }
}
