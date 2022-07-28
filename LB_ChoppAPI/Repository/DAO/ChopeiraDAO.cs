using Dapper;
using LB_ChoppAPI.Models;
using LB_ChoppAPI.Repository.Interface;
using LB_ChoppAPI.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Repository.DAO
{
    public class ChopeiraDAO: IChopeira
    {
        readonly IConfiguration _config;
        public ChopeiraDAO(IConfiguration config) { _config = config; }

        public async Task<IEnumerable<Chopeira>> GetChopeiraLivreAsync(string Token,
                                                                       string Voltagem,
                                                                       string Qt_torneiras)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(Token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select a.Id_Chopeira, a.Nr_Chopeira")
                    .AppendLine("from TB_RES_Chopeira a")
                    .AppendLine("where ISNULL(a.Cancelado, 0) <> 1")
                    .AppendLine("and not exists(select 1 from TB_RES_ReservaChopp x")
                    .AppendLine("				inner join TB_RES_ReservaChopeira y")
                    .AppendLine("				on x.CD_Empresa = y.CD_Empresa")
                    .AppendLine("				and x.ID_Reserva = y.ID_Reserva")
                    .AppendLine("				where ISNULL(x.ST_Registro, 'A') <> 'C'")
                    .AppendLine("				and ISNULL(y.ST_Registro, 'A') in('A', 'E')")
                    .AppendLine("				and y.Id_Chopeira = a.Id_Chopeira)")
                    .AppendLine("and a.QT_Torneiras = '" + Qt_torneiras.Trim() + "'")
                    .AppendLine("and a.Voltagem = '" + Voltagem.Trim() + "'");
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<Chopeira>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ChopeiraDisponivel>> GetChopeirasDisponiveisAsync(string token,
                                                                                        string dt_ini,
                                                                                        string dt_fin)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("declare @startDate datetime;")
                    .AppendLine("declare @endDate datetime;")
                    .AppendLine("set @startDate = '" + dt_ini.SoNumero() + "';")
                    .AppendLine("set @endDate = '" + dt_fin.SoNumero() + "';")
                    .AppendLine("with dateRange as")
                    .AppendLine("(")
                    .AppendLine("select dt = @startDate ")
                    .AppendLine("where @startDate <= @endDate")
                    .AppendLine("union all")
                    .AppendLine("select dateadd(dd, 1, dt)")
                    .AppendLine("from dateRange")
                    .AppendLine("where dateadd(dd, 1, dt) < @endDate")
                    .AppendLine(")")
                    .AppendLine("select a.voltagem, a.QT_Torneiras, count(1) as Qt_chopeira, ")
                    .AppendLine("Qt_reservada = isnull((select count(*) ")
                    .AppendLine("               from TB_RES_ReservaChopp x ")
                    .AppendLine("				inner join TB_RES_ReservaChopeira y ")
                    .AppendLine("				on x.CD_Empresa = y.CD_Empresa ")
                    .AppendLine("				and x.ID_Reserva = y.ID_Reserva ")
                    .AppendLine("				and isnull(x.ST_Registro, 'A') <> 'C' ")
                    .AppendLine("				and (isnull(y.st_registro, 'A') in('A', 'E'))")
                    .AppendLine("				inner join VTB_DIV_EMPRESA z")
                    .AppendLine("				on x.CD_Empresa = z.CD_Empresa")
                    .AppendLine("				where exists(select 1 from dateRange w where convert(datetime, floor(convert(decimal(30,10), w.dt))) ")
                    .AppendLine("					between convert(datetime, floor(convert(decimal(30,10), x.DT_Reserva))) ")
                    .AppendLine("					and convert(datetime, floor(convert(decimal(30,10), ")
                    .AppendLine("					case when convert(datetime, floor(convert(decimal(30,10), x.dt_prevretorno))) < convert(datetime, floor(convert(decimal(30,10), getdate()))) then ")
                    .AppendLine("					case when convert(datetime, floor(convert(decimal(30,10), x.DT_PrevRetorno))) < ")
                    .AppendLine("								convert(datetime, floor(convert(decimal(30,10), w.dt))) and isnull(y.st_registro, 'A') = 'E' then ")
                    .AppendLine("								w.dt else convert(datetime, floor(convert(decimal(30,10), x.dt_prevretorno))) end else convert(datetime, floor(convert(decimal(30,10), x.dt_prevretorno))) end))))")
                    .AppendLine("				and dbo.FVALIDA_NUMEROS(case when z.tp_pessoa = 'F' then z.NR_CPF else z.NR_CGC end) = '" + _conexaostr + "'")
                    .AppendLine("				and y.voltagem = a.voltagem ")
                    .AppendLine("				and y.qt_torneiras = a.qt_torneiras), 0) ")
                    .AppendLine("from tb_res_chopeira a ")
                    .AppendLine("where a.cancelado = 0 ")
                    .AppendLine("group by voltagem, QT_Torneiras ");
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<ChopeiraDisponivel>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
    }
}
