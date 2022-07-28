using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

namespace LB_Chopp.Utils
{
    public static class Extensoes
    {
        public static string SoNumero(this object valor)
        {
            if (valor == null)
                return string.Empty;
            string ret = string.Empty;
            foreach (char c in valor.ToString().ToCharArray())
                if (char.IsNumber(c))
                    ret += c;
            return ret;
        }
        public static bool IsDateTime(this object valor)
        {
            if (valor == null)
                return false;
            try
            {
                Convert.ToDateTime(valor);
                return true;
            }
            catch { return false; }
        }

        public static bool ValidaCNPJ(this string num)
        {
            num = num.SoNumero();
            if (num.SoNumero().Length != 14)
                return false;
            Int32[] n = new Int32[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int d1 = 0, d2 = 0, y = 0;
            string digitado = num.Substring(12, 2), calculado = "";

            try
            {
                for (int x = 0; x < 12; x++)
                    n[x] = Convert.ToInt32(num[x].ToString());

                y = 2;
                d1 = 0;
                d2 = 0;
                for (int x = 12; x >= 1; x--)
                {
                    d1 += n[x - 1] * y;
                    y++;
                    if (y > 9) y = 2;
                };
                d1 = 11 - (d1 % 11);
                if (d1 >= 10) d1 = 0;

                y = 3;
                d2 = d1 * 2;

                for (int x = 12; x >= 1; x--)
                {
                    d2 += n[x - 1] * y;
                    y++;
                    if (y > 9) y = 2;
                };


                d2 = 11 - (d2 % 11);
                if (d2 >= 10) d2 = 0;
                calculado = d1.ToString() + d2.ToString();


                if (calculado == digitado)
                    return true;
                else
                    return false;

            }
            catch
            {
                return false;
            };
        }
        public static string FormatarCNPJ(this string num)
        {
            if (num.SoNumero().Length.Equals(14))
            {
                string aux = num.SoNumero();
                return aux.Substring(0, 2) + "." +
                    aux.Substring(2, 3) + "." +
                    aux.Substring(5, 3) + "/" +
                    aux.Substring(8, 4) + "-" +
                    aux.Substring(12, 2);
            }
            else return num;
        }
        public static bool ValidaCPF(this string num)
        {
            num = num.SoNumero();
            if (num.SoNumero().Length != 11)
                return false;
            Int32[] n = new Int32[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int d1 = 0, d2 = 0, y = 0;
            string digitado = num.Substring(9, 2), calculado = "";

            try
            {
                for (int x = 0; x < 9; x++)
                    n[x] = Convert.ToInt32(num[x].ToString());

                y = 2;
                for (int x = 9; x >= 1; x--)
                {
                    d1 += n[x - 1] * y;
                    y++;
                };
                d1 = 11 - (d1 % 11);
                if (d1 >= 10) d1 = 0;

                y = 3;
                for (int x = 9; x >= 1; x--)
                {
                    d2 += n[x - 1] * y;
                    y++;
                };

                d2 = d1 * 2 + d2;
                d2 = 11 - (d2 % 11);
                if (d2 >= 10) d2 = 0;
                calculado = d1.ToString() + d2.ToString();
                if (calculado == digitado)
                    return true;
                else
                    return false;

            }
            catch
            {
                return false;
            }
        }
        public static string FormatarCPF(this string num)
        {
            if (num.SoNumero().Length.Equals(11))
            {
                string aux = num.SoNumero();
                return aux.Substring(0, 3) + "." +
                    aux.Substring(3, 3) + "." +
                    aux.Substring(6, 3) + "-" +
                    aux.Substring(9, 2);
            }
            else return num;
        }
        public static string FormatarFone(this string num)
        {
            if (num.SoNumero().Length.Equals(10))
            {
                string aux = num.SoNumero();
                return "|" + aux.Substring(0, 2) + "|" +
                    aux.Substring(2, 4) + "-" +
                    aux.Substring(6, 4);
            }
            else return num;
        }
        public static string FormatarCelular(this string num)
        {
            if (num.SoNumero().Length.Equals(11))
            {
                string aux = num.SoNumero();
                return "|" + aux.Substring(0, 2) + "|" +
                    aux.Substring(2, 5) + "-" +
                    aux.Substring(7, 4);
            }
            else return num;
        }
        public static string RemoverCaracteres(this object valor)
        {
            if (valor == null)
                return string.Empty;
            string retorno = valor.ToString();
            //Remover acentos do (a) minusculo
            retorno = Regex.Replace(retorno, "[äáâàã]", "a");
            //Remover acentos do (A) maiusculo
            retorno = Regex.Replace(retorno, "[ÄÅÁÂÀÃ]", "A");
            //Remover acentos do (e) minusculo
            retorno = Regex.Replace(retorno, "[éêëè]", "e");
            //Remover acentos do (E) maiusculo
            retorno = Regex.Replace(retorno, "[ÉÊËÈ]", "E");
            //Remover acentos do (i) minusculo
            retorno = Regex.Replace(retorno, "[íîïì]", "i");
            //Remover acentos do (I) maiusculo
            retorno = Regex.Replace(retorno, "[ÍÎÏÌ]", "I");
            //Remover acentos do (o) minusculo
            retorno = Regex.Replace(retorno, "[öóôòõ]", "o");
            //Remover acentos do (O) maiusculo
            retorno = Regex.Replace(retorno, "[ÖÓÔÒÕ]", "O");
            //Remover acentos do (u) minusculo
            retorno = Regex.Replace(retorno, "[üúûù]", "u");
            //Remover acentos do (U) maiusculo
            retorno = Regex.Replace(retorno, "[ÜÚÛ]", "U");
            //Remover acentos do (ç) minusculo
            retorno = Regex.Replace(retorno, "[ç]", "c");
            //Remover acentos do (Ç) maiusculo
            retorno = Regex.Replace(retorno, "[Ç]", "C");
            //Remover caracter especial
            retorno = Regex.Replace(retorno, "[º]", "");
            //Remover acentos do (nº) minusculo
            retorno = Regex.Replace(retorno, "[nº]", "n");
            //Remover acentos do (Nº) maiusculo
            retorno = Regex.Replace(retorno, "[Nº]", "N");
            //Remover acentos do (nª) minusculo
            retorno = Regex.Replace(retorno, "[nª]", "n");
            //Remover acentos do (Nª) maiusculo
            retorno = Regex.Replace(retorno, "[Nª]", "N");

            return retorno;
        }
        public static string SubstCaracteresEsp(this object valor)
        {
            if (valor == null)
                return string.Empty;
            string retorno = valor.ToString();
            //Sinal <
            retorno = Regex.Replace(retorno, "[<]", "&lt;");
            //Sinal >
            retorno = Regex.Replace(retorno, "[>]", "&gt;");
            //Sinal &
            retorno = Regex.Replace(retorno, "[&]", "&amp;");
            //Sinal "
            retorno = Regex.Replace(retorno, "[\"]", "&quot;");
            //Sinal '
            retorno = Regex.Replace(retorno, "[']", "&#39;");

            return retorno;
        }
    }
}
