using Dapper;
using LB_ChoppAPI.Models;
using LB_ChoppAPI.Repository.Interface;
using LB_ChoppAPI.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LB_ChoppAPI.Repository.DAO
{
    public class VendedorDAO: IVendedor
    {
        readonly IConfiguration _config;
        public VendedorDAO(IConfiguration config) { _config = config; }

        public async Task<TokenVendedor> validarAsync(string Token, string Login, string Senha, string Cnpj)
        {
            string _conexaostr = _config.GetConnectionString(Encoding.UTF8.GetString(Convert.FromBase64String(Token)));
            string _conexaoHelp = _config.GetConnectionString("conexaoHelp");
            try

            {
                //Verificar se cnpj esta habilitado para utilizar mobile
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select 1")
                    .AppendLine("from TB_CRM_CLIENTE")
                    .AppendLine("where ISNULL(Mobile, 0) = 1")
                    .AppendLine("and ISNULL(ST_REGISTRO, 'A') <> 'I'")
                    .AppendLine("and dbo.FVALIDA_NUMEROS(CNPJ) = '" + Cnpj.SoNumero() + "'");
                bool cnpj_habilitado = false;
                using (TConexao conexao = new TConexao(_conexaoHelp))
                {
                    if (await conexao.OpenConnectionAsync())
                        cnpj_habilitado = await conexao._conexao.ExecuteScalarAsync<bool>(sql.ToString());
                }
                if (cnpj_habilitado)
                {
                    sql.Clear();
                    sql.AppendLine("select a.Login, a.Nome_Usuario, c.CD_Empresa,")
                        .AppendLine("c.NM_Empresa, c.NR_CGC as CNPJ")
                        .AppendLine("from TB_DIV_Usuario a")
                        .AppendLine("inner join TB_DIV_Usuario_X_Empresa b")
                        .AppendLine("on a.Login = b.Login")
                        .AppendLine("inner join VTB_DIV_EMPRESA c")
                        .AppendLine("on b.CD_Empresa = c.CD_Empresa")
                        .AppendLine("where a.Login = '" + Login.Trim() + "'")
                        .AppendLine("and a.Senha = '" + Senha.Trim() + "'")
                        .AppendLine("and dbo.FVALIDA_NUMEROS(c.NR_CGC) = '" + Cnpj.SoNumero() + "'");
                    using (TConexao conexao = new TConexao(_conexaostr))
                    {
                        if (await conexao.OpenConnectionAsync())
                            return await conexao._conexao.QueryFirstOrDefaultAsync<TokenVendedor>(sql.ToString());
                        else return null;
                    }
                }
                else return null;
            }
            catch { return null; }
        }
    }
}
