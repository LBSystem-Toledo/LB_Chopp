using LB_ChoppAPI.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System;
using LB_ChoppAPI.Utils;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using LB_ChoppAPI.Repository.Interface;
using System.IO;
using System.IO.Compression;

namespace LB_ChoppAPI.Repository.DAO
{
    public class ReservaChoppDAO: IReservaChopp
    {
        readonly IConfiguration _config;
        public ReservaChoppDAO(IConfiguration config) { _config = config; }

        public async Task<IEnumerable<ReservaChopp>> GetReservaExpedirAsync(string token, 
                                                                            int Id_reserva,
                                                                            string Cd_clifor,
                                                                            string Dt_ini,
                                                                            string Dt_fin)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select a.CD_Empresa, a.ID_Reserva,")
                    .AppendLine("a.CD_Clifor, b.NM_Clifor, a.CD_Endereco,")
                    .AppendLine("a.DT_Reserva, a.Dt_prevretorno, a.Logradouro_Ent, a.Numero_Ent,")
                    .AppendLine(" a.Bairro_Ent, a.Complemento_Ent, a.Proximo_Ent")
                    .AppendLine("from TB_RES_ReservaChopp a")
                    .AppendLine("inner join TB_FIN_Clifor b")
                    .AppendLine("on a.CD_Clifor = b.CD_Clifor")
                    .AppendLine("inner join VTB_DIV_EMPRESA c")
                    .AppendLine("on a.CD_Empresa = c.CD_Empresa")
                    .AppendLine("where ISNULL(a.ST_Registro, 'A') <> 'C'")
                    .AppendLine("and dbo.FVALIDA_NUMEROS(case when c.tp_pessoa = 'F' then c.NR_CPF else c.NR_CGC end) = '" + _conexaostr.SoNumero() + "'")
                    .AppendLine("and (exists(select 1 from TB_RES_ReservaChopeira x where x.cd_empresa = a.cd_empresa and x.id_reserva = a.id_reserva and isnull(x.st_registro, 'A') = 'A') or")
                    .AppendLine("exists(select 1 from TB_RES_ReservaBarril x where x.cd_empresa = a.cd_empresa and x.id_reserva = a.id_reserva and isnull(x.st_registro, 'A') = 'A') or")
                    .AppendLine("exists(select 1 from TB_RES_ReservaCilindro x where x.cd_empresa = a.cd_empresa and x.id_reserva = a.id_reserva and isnull(x.st_registro, 'A') = 'A'))");
                if (Id_reserva > 0)
                    sql.AppendLine("and a.id_reserva = " + Id_reserva.ToString());
                if (!string.IsNullOrWhiteSpace(Cd_clifor))
                    sql.AppendLine("and a.cd_clifor = '" + Cd_clifor.Trim() + "'");
                if (Dt_ini.IsDateTime())
                    sql.AppendLine("and convert(datetime, floor(convert(decimal(30,10), a.DT_Reserva))) >= '" + Dt_ini.SoNumero() + "'");
                if (Dt_fin.IsDateTime())
                    sql.AppendLine("and convert(datetime, floor(convert(decimal(30,10), a.DT_Reserva))) <= '" + Dt_fin.SoNumero() + "'");

                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<ReservaChopp>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaChopeira>> GetChopeirasExpedirAsync(string token,
                                                                                 string Cd_empresa,
                                                                                 int Id_reserva)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select a.CD_Empresa, a.ID_Reserva, a.ID_Item,")
                .AppendLine("a.Id_Chopeira, a.Voltagem, a.QT_Torneiras")
                .AppendLine("from TB_RES_ReservaChopeira a")
                .AppendLine("where a.CD_Empresa = '" + Cd_empresa.Trim() + "'")
                .AppendLine("and a.ID_Reserva = " + Id_reserva.ToString())
                .AppendLine("and isnull(a.st_registro, 'A') = 'A'");

                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<ReservaChopeira>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaBarril>> GetBarrisExpedirAsync(string token,
                                                                            string Cd_empresa,
                                                                            int Id_reserva)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select a.CD_Empresa, a.ID_Reserva, a.ID_Item,")
                .AppendLine("a.Id_barril, a.ID_Tipo, c.DS_Tipo, a.CD_Produto, b.DS_Produto, a.Volume")
                .AppendLine("from TB_RES_ReservaBarril a")
                .AppendLine("inner join TB_EST_Produto b")
                .AppendLine("on a.CD_Produto = b.CD_Produto")
                .AppendLine("inner join TB_RES_TipoBarril c")
                .AppendLine("on a.id_tipo = c.id_tipo")
                .AppendLine("where a.CD_Empresa = '" + Cd_empresa.Trim() + "'")
                .AppendLine("and a.ID_Reserva = " + Id_reserva.ToString())
                .AppendLine("and isnull(a.st_registro, 'A') = 'A'");

                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<ReservaBarril>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaCilindro>> GetCilindrosExpedirAsync(string token,
                                                                                 string Cd_empresa,
                                                                                 int Id_reserva)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select a.CD_Empresa, a.ID_Reserva,")
                        .AppendLine("a.ID_Item, a.ID_Cilindro")
                        .AppendLine("from TB_RES_ReservaCilindro a")
                        .AppendLine("where a.CD_Empresa = '" + Cd_empresa.Trim() + "'")
                        .AppendLine("and a.ID_Reserva = " + Id_reserva.ToString())
                        .AppendLine("and isnull(a.st_registro, 'A') = 'A'");

                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<ReservaCilindro>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaChopp>> GetReservaColetarAsync(string token,
                                                                            int Id_reserva,
                                                                            string Cd_clifor,
                                                                            string Dt_ini,
                                                                            string Dt_fin)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select a.CD_Empresa, a.ID_Reserva,")
                    .AppendLine("a.CD_Clifor, b.NM_Clifor, a.CD_Endereco,")
                    .AppendLine("a.DT_Reserva, a.DT_PrevRetorno, a.Logradouro_Ent, a.Numero_Ent,")
                    .AppendLine(" a.Bairro_Ent, a.Complemento_Ent, a.Proximo_Ent")
                    .AppendLine("from TB_RES_ReservaChopp a")
                    .AppendLine("inner join TB_FIN_Clifor b")
                    .AppendLine("on a.CD_Clifor = b.CD_Clifor")
                    .AppendLine("inner join VTB_DIV_EMPRESA c")
                    .AppendLine("on a.CD_Empresa = c.CD_Empresa")
                    .AppendLine("where ISNULL(a.ST_Registro, 'A') <> 'C'")
                    .AppendLine("and dbo.FVALIDA_NUMEROS(case when c.tp_pessoa = 'F' then c.NR_CPF else c.NR_CGC end) = '" + _conexaostr.SoNumero() + "'")
                    .AppendLine("and (exists(select 1 from TB_RES_ReservaChopeira x where x.cd_empresa = a.cd_empresa and x.id_reserva = a.id_reserva and isnull(x.st_registro, 'A') = 'E') or")
                    .AppendLine("exists(select 1 from TB_RES_ReservaBarril x where x.cd_empresa = a.cd_empresa and x.id_reserva = a.id_reserva and isnull(x.st_registro, 'A') = 'E') or")
                    .AppendLine("exists(select 1 from TB_RES_ReservaCilindro x where x.cd_empresa = a.cd_empresa and x.id_reserva = a.id_reserva and isnull(x.st_registro, 'A') = 'E'))");
                if (Id_reserva > 0)
                    sql.AppendLine("and a.id_reserva = " + Id_reserva.ToString());
                if (!string.IsNullOrWhiteSpace(Cd_clifor))
                    sql.AppendLine("and a.cd_clifor = '" + Cd_clifor.Trim() + "'");
                if (Dt_ini.IsDateTime())
                    sql.AppendLine("and convert(datetime, floor(convert(decimal(30,10), a.DT_PrevRetorno))) >= '" + Dt_ini.SoNumero() + "'");
                if (Dt_fin.IsDateTime())
                    sql.AppendLine("and convert(datetime, floor(convert(decimal(30,10), a.DT_PrevRetorno))) <= '" + Dt_fin.SoNumero() + "'");

                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<ReservaChopp>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaChopeira>> GetChopeirasColetarAsync(string token,
                                                                                 string Cd_empresa,
                                                                                 int Id_reserva)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select a.CD_Empresa, a.ID_Reserva, a.ID_Item,")
                .AppendLine("a.Id_Chopeira, b.Nr_Chopeira, a.Voltagem, a.QT_Torneiras")
                .AppendLine("from TB_RES_ReservaChopeira a")
                .AppendLine("left join TB_RES_Chopeira b")
                .AppendLine("on a.Id_Chopeira = b.Id_Chopeira")
                .AppendLine("where a.CD_Empresa = '" + Cd_empresa.Trim() + "'")
                .AppendLine("and a.ID_Reserva = " + Id_reserva.ToString())
                .AppendLine("and isnull(a.st_registro, 'A') = 'E'");

                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<ReservaChopeira>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaBarril>> GetBarrisColetarAsync(string token,
                                                                            string Cd_empresa,
                                                                            int Id_reserva)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select a.CD_Empresa, a.ID_Reserva, a.ID_Item, a.CD_Produto,")
                .AppendLine("a.Id_barril, a.ID_Tipo, c.Nr_barril, b.DS_Produto, a.Volume")
                .AppendLine("from TB_RES_ReservaBarril a")
                .AppendLine("inner join TB_EST_Produto b")
                .AppendLine("on a.CD_Produto = b.CD_Produto")
                .AppendLine("left join TB_RES_Barril c")
                .AppendLine("on a.Id_barril = c.Id_barril")
                .AppendLine("and a.Id_tipo = c.Id_tipo")
                .AppendLine("where a.CD_Empresa = '" + Cd_empresa.Trim() + "'")
                .AppendLine("and a.ID_Reserva = " + Id_reserva.ToString())
                .AppendLine("and isnull(a.st_registro, 'A') = 'E'");

                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<ReservaBarril>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<IEnumerable<ReservaCilindro>> GetCilindrosColetarAsync(string token,
                                                                             string Cd_empresa,
                                                                             int Id_reserva)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select a.CD_Empresa, a.ID_Reserva,")
                        .AppendLine("a.ID_Item, a.ID_Cilindro, b.NR_Cilindro")
                        .AppendLine("from TB_RES_ReservaCilindro a")
                        .AppendLine("left join TB_RES_Cilindro b")
                        .AppendLine("on a.ID_Cilindro = b.ID_Cilindro")
                        .AppendLine("where a.CD_Empresa = '" + Cd_empresa.Trim() + "'")
                        .AppendLine("and a.ID_Reserva = " + Id_reserva.ToString())
                        .AppendLine("and isnull(a.st_registro, 'A') = 'E'");

                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                        return await conexao._conexao.QueryAsync<ReservaCilindro>(sql.ToString());
                    else return null;
                }
            }
            catch { return null; }
        }
        public async Task<bool> GravarExpedicaoAsync(string token, ReservaChopp reserva)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            SqlTransaction t = null;
            try
            {
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                    {
                        t = conexao._conexao.BeginTransaction(IsolationLevel.ReadCommitted);
                        if (reserva.Chopeiras != null)
                            foreach (var p in reserva.Chopeiras)
                            {
                                DynamicParameters param = new DynamicParameters();
                                param.Add("@ID_CHOPEIRA", p.Id_chopeira);
                                param.Add("@CD_EMPRESA", p.Cd_empresa);
                                param.Add("@ID_RESERVA", p.Id_reserva);
                                param.Add("@ID_ITEM", p.Id_item);
                                StringBuilder sql = new StringBuilder();
                                sql.AppendLine("update TB_RES_ReservaChopeira")
                                .AppendLine("set Id_Chopeira = @ID_CHOPEIRA,")
                                .AppendLine("DT_Expedicao = GETDATE(),")
                                .AppendLine("ST_Registro = 'E',")
                                .AppendLine("DT_Alt = GETDATE()")
                                .AppendLine("where CD_Empresa = @CD_EMPRESA")
                                .AppendLine("and ID_Reserva = @ID_RESERVA")
                                .AppendLine("and ID_Item = @ID_ITEM");
                                await conexao._conexao.ExecuteAsync(sql.ToString(), param, transaction: t, commandType: CommandType.Text);
                            }
                        if(reserva.Barris != null)
                            foreach (var p in reserva.Barris)
                            {
                                DynamicParameters param = new DynamicParameters();
                                param.Add("@ID_BARRIL", p.Id_barril);
                                param.Add("@ID_TIPO", p.Id_tipo);
                                param.Add("@CD_EMPRESA", p.Cd_empresa);
                                param.Add("@ID_RESERVA", p.Id_reserva);
                                param.Add("@ID_ITEM", p.Id_item);
                                StringBuilder sql = new StringBuilder();
                                sql.AppendLine("update TB_RES_ReservaBarril")
                                .AppendLine("set Id_barril = @ID_BARRIL,")
                                .AppendLine("ID_Tipo = @ID_TIPO,")
                                .AppendLine("DT_Expedicao = GETDATE(),")
                                .AppendLine("ST_Registro = 'E',")
                                .AppendLine("DT_Alt = GETDATE()")
                                .AppendLine("where CD_Empresa = @CD_EMPRESA")
                                .AppendLine("and ID_Reserva = @ID_RESERVA")
                                .AppendLine("and ID_Item = @ID_ITEM");
                                await conexao._conexao.ExecuteAsync(sql.ToString(), param, transaction: t, commandType: CommandType.Text);
                            }
                        if(reserva.Cilindros != null)
                            foreach (var p in reserva.Cilindros)
                            {
                                DynamicParameters param = new DynamicParameters();
                                param.Add("@ID_CILINDRO", p.Id_cilindro);
                                param.Add("@CD_EMPRESA", p.Cd_empresa);
                                param.Add("@ID_RESERVA", p.Id_reserva);
                                param.Add("@ID_ITEM", p.Id_item);
                                StringBuilder sql = new StringBuilder();
                                sql.AppendLine("update TB_RES_ReservaCilindro")
                                .AppendLine("set ID_Cilindro = @ID_CILINDRO,")
                                .AppendLine("DT_Expedicao = GETDATE(),")
                                .AppendLine("ST_Registro = 'E',")
                                .AppendLine("DT_Alt = GETDATE()")
                                .AppendLine("where CD_Empresa = @CD_EMPRESA")
                                .AppendLine("and ID_Reserva = @ID_RESERVA")
                                .AppendLine("and ID_Item = @ID_ITEM");
                                await conexao._conexao.ExecuteAsync(sql.ToString(), param, transaction: t, commandType: CommandType.Text);
                            }
                        t.Commit();
                        return true;
                    }
                    else return false;
                }
            }
            catch 
            {
                if (t != null)
                {
                    t.Rollback();
                    t.Dispose();
                }
                return false; 
            }
        }
        public async Task<bool> ProrrogarColetaAsync(string token, ReservaChopp reserva)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            SqlTransaction t = null;
            try
            {
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                    {
                        t = conexao._conexao.BeginTransaction(IsolationLevel.ReadCommitted);
                        //Alterar data coleta
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("update TB_RES_ReservaChopp")
                            .AppendLine("set DT_PrevRetorno = @DT_PREVRETORNO,")
                            .AppendLine("DT_Alt = GETDATE()")
                            .AppendLine("where CD_Empresa = @CD_EMPRESA")
                            .AppendLine("and ID_Reserva = @ID_RESERVA");
                        DynamicParameters param = new DynamicParameters();
                        param.Add("@DT_PREVRETORNO", reserva.Dt_prevretorno);
                        param.Add("@CD_EMPRESA", reserva.Cd_empresa);
                        param.Add("@ID_RESERVA", reserva.Id_reserva);
                        await conexao._conexao.ExecuteAsync(sql.ToString(), param, transaction: t, commandType: CommandType.Text);
                        //Gravar prorrogação
                        param = new DynamicParameters();
                        param.Add("@P_CD_EMPRESA", reserva.Cd_empresa);
                        param.Add("@P_ID_RESERVA", reserva.Id_reserva);
                        param.Add("@P_ID_REGISTRO", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        param.Add("@P_DT_PREVRETORNOOLD", reserva.Dt_prevretornoOld);
                        param.Add("@P_MOTIVO", reserva.Motivo);
                        await conexao._conexao.ExecuteAsync("IA_RES_PRORROGARCOLETA", param, transaction: t, commandType: CommandType.StoredProcedure);
                        t.Commit();
                        return true;
                    }
                    else return false;
                }
            }
            catch
            {
                if (t != null)
                {
                    t.Rollback();
                    t.Dispose();
                }
                return false;
            }
        }
        public async Task<bool> ColetarChopeiraAsync(string token, ReservaChopeira reservaChopeira)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            try
            {
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                    {
                        DynamicParameters param = new DynamicParameters();
                        param.Add("@CD_EMPRESA", reservaChopeira.Cd_empresa);
                        param.Add("@ID_RESERVA", reservaChopeira.Id_reserva);
                        param.Add("@ID_ITEM", reservaChopeira.Id_item);
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("update TB_RES_ReservaChopeira")
                        .AppendLine("set DT_Coleta = GETDATE(),")
                        .AppendLine("ST_Registro = 'D',")
                        .AppendLine("DT_Alt = GETDATE()")
                        .AppendLine("where CD_Empresa = @CD_EMPRESA")
                        .AppendLine("and ID_Reserva = @ID_RESERVA")
                        .AppendLine("and ID_Item = @ID_ITEM");
                        await conexao._conexao.ExecuteAsync(sql.ToString(), param, commandType: CommandType.Text);
                        return true;
                    }
                    else return false;
                }
            }
            catch{ return false; }
        }
        public async Task<bool> ColetarBarrilAsync(string token, ReservaBarril reservaBarril)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            SqlTransaction t = null;
            try
            {
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                    {
                        t = conexao._conexao.BeginTransaction(IsolationLevel.ReadCommitted);
                        DynamicParameters param = new DynamicParameters();
                        param.Add("@CD_EMPRESA", reservaBarril.Cd_empresa);
                        param.Add("@ID_RESERVA", reservaBarril.Id_reserva);
                        param.Add("@ID_ITEM", reservaBarril.Id_item);
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("update TB_RES_ReservaBarril")
                        .AppendLine("set DT_Coleta = GETDATE(),")
                        .AppendLine("ST_Registro = 'D',")
                        .AppendLine("DT_Alt = GETDATE()")
                        .AppendLine("where CD_Empresa = @CD_EMPRESA")
                        .AppendLine("and ID_Reserva = @ID_RESERVA")
                        .AppendLine("and ID_Item = @ID_ITEM");
                        await conexao._conexao.ExecuteAsync(sql.ToString(), param, transaction: t, commandType: CommandType.Text);
                        if(!reservaBarril.RetornouCheio)
                        {
                            param = new DynamicParameters();
                            param.Add("@CD_EMPRESA", reservaBarril.Cd_empresa);
                            param.Add("@ID_BARRIL", reservaBarril.Id_barril);
                            param.Add("@CD_PRODUTO", reservaBarril.Cd_produto);
                            sql = new StringBuilder();
                            sql.AppendLine("update TB_RES_MovBarril")
                                .AppendLine("set DT_Descarga = GETDATE(),")
                                .AppendLine("ST_Registro = 'V',")
                                .AppendLine("DT_Alt = GETDATE()")
                                .AppendLine("where CD_Empresa = @CD_EMPRESA")
                                .AppendLine("and Id_barril = @ID_BARRIL")
                                .AppendLine("and CD_Produto = @CD_PRODUTO")
                                .AppendLine("and st_registro = 'C'");
                            await conexao._conexao.ExecuteAsync(sql.ToString(), param, transaction: t, commandType: CommandType.Text);
                        }
                        t.Commit();
                        return true;
                    }
                    else return false;
                }
            }
            catch 
            {
                if (t != null)
                {
                    t.Rollback();
                    t.Dispose();
                }
                return false; 
            }
        }
        public async Task<bool> ColetarCilindroAsync(string token, ReservaCilindro reservaCilindro)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            try
            {
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                    {
                        DynamicParameters param = new DynamicParameters();
                        param.Add("@CD_EMPRESA", reservaCilindro.Cd_empresa);
                        param.Add("@ID_RESERVA", reservaCilindro.Id_reserva);
                        param.Add("@ID_ITEM", reservaCilindro.Id_item);
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("update TB_RES_ReservaCilindro")
                        .AppendLine("set DT_Coleta = GETDATE(),")
                        .AppendLine("ST_Registro = 'D',")
                        .AppendLine("DT_Alt = GETDATE()")
                        .AppendLine("where CD_Empresa = @CD_EMPRESA")
                        .AppendLine("and ID_Reserva = @ID_RESERVA")
                        .AppendLine("and ID_Item = @ID_ITEM");
                        await conexao._conexao.ExecuteAsync(sql.ToString(), param, commandType: CommandType.Text);
                        return true;
                    }
                    else return false;
                }
            }
            catch { return false; }
        }
        public async Task<string> GravarFotoAsync(string token, ReservaFoto foto)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            try
            {
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                    {
                        DynamicParameters param = new DynamicParameters();
                        param.Add("@P_CD_EMPRESA", foto.Cd_empresa);
                        param.Add("@P_ID_RESERVA", foto.Id_reserva);
                        param.Add("@P_ID_IMAGEM", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        param.Add("@P_IMAGEM", foto.Foto);
                        param.Add("@P_ORIGEM", foto.Origem);

                        await conexao._conexao.ExecuteAsync("IA_RES_IMAGENSRESERVA", param, commandType: CommandType.StoredProcedure);
                        return "1";
                    }
                    else return "0";
                }
            }
            catch(Exception ex) { return ex.Message.Trim(); }
        }
        public async Task<bool> GravarReservaAsync(string token, ReservaChopp reserva)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            SqlTransaction t = null;
            try
            {
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                    {
                        t = conexao._conexao.BeginTransaction(IsolationLevel.ReadCommitted);
                        //Gravar Reserva
                        DynamicParameters param = new DynamicParameters();
                        param.Add("@P_CD_EMPRESA", reserva.Cd_empresa);
                        param.Add("@P_ID_RESERVA", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        param.Add("@P_CD_CLIFOR", reserva.Cd_clifor);
                        param.Add("@P_CD_ENDERECO", reserva.Cd_endereco);
                        param.Add("@P_LOGRADOURO_ENT", reserva.Logradouro_ent);
                        param.Add("@P_NUMERO_ENT", reserva.Numero_ent);
                        param.Add("@P_BAIRRO_ENT", reserva.Bairro_ent);
                        param.Add("@P_COMPLEMENTO_ENT", reserva.Complemento_ent);
                        param.Add("@P_PROXIMO_ENT", reserva.Proximo_ent);
                        param.Add("@P_DT_RESERVA", reserva.Dt_reserva);
                        param.Add("@P_DT_PREVRETORNO", reserva.Dt_prevretorno);
                        param.Add("@P_ST_REGISTRO", "A");
                        param.Add("@P_MOTIVOCANC", string.Empty);
                        param.Add("@P_OBS", reserva.Obs);
                        await conexao._conexao.ExecuteAsync("IA_RES_RESERVACHOPP", param, transaction: t, commandType: CommandType.StoredProcedure);
                        reserva.Id_reserva = param.Get<int>("@P_ID_RESERVA");
                        //Gravar Chopeiras
                        foreach(var p in reserva.Chopeiras)
                        {
                            param = new DynamicParameters();
                            param.Add("@P_CD_EMPRESA", reserva.Cd_empresa);
                            param.Add("@P_ID_RESERVA", reserva.Id_reserva);
                            param.Add("@P_ID_ITEM", dbType: DbType.Int32, direction: ParameterDirection.Output);
                            param.Add("@P_ID_CHOPEIRA", null);
                            param.Add("@P_VOLTAGEM", p.Voltagem);
                            param.Add("@P_QT_TORNEIRAS", p.Qt_torneiras);
                            param.Add("@P_MOTIVOCANC", string.Empty);
                            param.Add("@P_DT_EXPEDICAO", null);
                            param.Add("@P_DT_COLETA", null);
                            param.Add("@P_ST_REGISTRO", "A");
                            await conexao._conexao.ExecuteAsync("IA_RES_RESERVACHOPEIRA", param, transaction: t, commandType: CommandType.StoredProcedure);
                        }
                        //Gravar Barris
                        foreach(var p in reserva.Barris)
                        {
                            param = new DynamicParameters();
                            param.Add("@P_CD_EMPRESA", reserva.Cd_empresa);
                            param.Add("@P_ID_RESERVA", reserva.Id_reserva);
                            param.Add("@P_ID_ITEM", dbType: DbType.Int32, direction: ParameterDirection.Output);
                            param.Add("@P_CD_PRODUTO", p.Cd_produto);
                            param.Add("@P_ID_BARRIL", null);
                            param.Add("@P_LOGINRETCHEIO", null);
                            param.Add("@P_ID_TIPO", p.Id_tipo);
                            param.Add("@P_VL_UNITARIO", p.Valor / p.Volume);
                            param.Add("@P_VL_DESCONTO", p.Vl_desconto);
                            param.Add("@P_VL_FRETE", p.Vl_frete);
                            param.Add("@P_VOLUME", p.Volume);
                            param.Add("@P_MOTIVOCANC", string.Empty);
                            param.Add("@P_DT_EXPEDICAO", null);
                            param.Add("@P_DT_COLETA", null);
                            param.Add("@P_RETORNOUCHEIO", false);
                            param.Add("@P_ST_REGISTRO", "A");
                            await conexao._conexao.ExecuteAsync("IA_RES_RESERVABARRIL", param, transaction: t, commandType: CommandType.StoredProcedure);
                        }
                        for (int i = 0; i < reserva.QtCilindros; i++)
                        {
                            param = new DynamicParameters();
                            param.Add("@P_CD_EMPRESA", reserva.Cd_empresa);
                            param.Add("@P_ID_RESERVA", reserva.Id_reserva);
                            param.Add("@P_ID_ITEM", dbType: DbType.Int32, direction: ParameterDirection.Output);
                            param.Add("@P_ID_CILINDRO", null);
                            param.Add("@P_ST_CILINDRO", true);
                            param.Add("@P_MOTIVOCANC", string.Empty);
                            param.Add("@P_DT_EXPEDICAO", null);
                            param.Add("@P_DT_COLETA", null);
                            param.Add("@P_ST_REGISTRO", "A");
                            await conexao._conexao.ExecuteAsync("IA_RES_RESERVACILINDRO", param, transaction: t, commandType: CommandType.StoredProcedure);
                        }
                        t.Commit();
                        return true;
                    }
                    else return false;
                }
            }
            catch 
            {
                if (t != null)
                {
                    t.Dispose();
                    t = null;
                }
                return false; 
            }
        }
    }
}
