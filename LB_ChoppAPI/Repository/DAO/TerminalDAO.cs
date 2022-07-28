using Dapper;
using LB_ChoppAPI.Models;
using LB_ChoppAPI.Repository.Interface;
using LB_ChoppAPI.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Repository.DAO
{
    public class TerminalMobileDAO : ITerminalMobile
    {
        readonly IConfiguration _config;

        public TerminalMobileDAO(IConfiguration config) { _config = config; }

        public async Task<bool> ValidarDeviceAsync(string Token, TerminalMobile terminal)
        {
            string _conexaostr = Encoding.UTF8.GetString(Convert.FromBase64String(Token));
            string _conexaoHelp = _config.GetConnectionString("conexaoHelp");
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select 1 ")
                    .AppendLine("from TB_DIV_TerminalMobile a")
                    .AppendLine("where a.CD_Empresa = '" + terminal.Cd_empresa.Trim() + "'")
                    .AppendLine("and a.IdDevice = '" + terminal.IdDevice.Trim() + "'");
                using (TConexao conexao = new TConexao(_config.GetConnectionString(_conexaostr)))
                {
                    if (await conexao.OpenConnectionAsync())
                    {
                        var ret = await conexao._conexao.ExecuteScalarAsync<bool>(sql.ToString());
                        if (!ret)
                        {
                            //Buscar quantidade de terminal disponivel para empresa
                            //int qt_terminal = 0;
                            //sql.Clear();
                            //sql.AppendLine("select ISNULL(a.QT_Mobile, 0)")
                            //    .AppendLine("from TB_CRM_CLIENTE a")
                            //    .AppendLine("where dbo.FVALIDA_NUMEROS(a.CNPJ) = '" + terminal.Cnpj.SoNumero() + "'")
                            //    .AppendLine("and ISNULL(a.ST_REGISTRO, 'A') = 'A'");
                            //using (TConexao conHelp = new TConexao(_conexaoHelp))
                            //{
                            //    if (await conHelp.OpenConnectionAsync())
                            //        qt_terminal = await conHelp._conexao.ExecuteScalarAsync<int>(sql.ToString());
                            //}
                            ////Buscar qtde de terminais ja cadastrados
                            //sql.Clear();
                            //sql.AppendLine("select count(*)")
                            //    .AppendLine("from tb_div_terminalmobile")
                            //    .AppendLine("where cd_empresa = '" + terminal.Cd_empresa + "'");
                            //int tot_terminal = await conexao._conexao.ExecuteScalarAsync<int>(sql.ToString());
                            //if (tot_terminal < qt_terminal)
                            //{
                            //    //Gravar novo terminal
                            //    DynamicParameters p = new DynamicParameters();
                            //    p.Add("@P_CD_EMPRESA", terminal.Cd_empresa, dbType: DbType.String, direction: ParameterDirection.Input);
                            //    p.Add("@P_ID_TERMINAL", dbType: DbType.Int32, direction: ParameterDirection.Output);
                            //    p.Add("@P_IDDEVICE", terminal.IdDevice, dbType: DbType.String, direction: ParameterDirection.Input);
                            //    int i = await conexao._conexao.ExecuteAsync("IA_DIV_TERMINALMOBILE", p, commandType: CommandType.StoredProcedure);
                            //    ret = i > 0;
                            //}
                            //else ret = false;
                            return true;
                        }
                        return ret;
                    }
                    else return false;
                }
            }
            catch { return false; }
        }
    }
}
