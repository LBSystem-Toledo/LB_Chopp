using System;

namespace LB_ChoppAPI.Utils
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
    }
}
