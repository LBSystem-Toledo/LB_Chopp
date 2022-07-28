using LB_Chopp.Models;
using System;
using System.IO;

namespace LB_Chopp.Utils
{
    public static class Arquivo
    {
        static string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
        public static Usuario GetValues()
        {
            if (File.Exists(fileName))
            {
                string[] s = File.ReadAllLines(fileName);
                if (s.Length.Equals(3))
                    return new Usuario { Login = s[0], Senha = s[1], Cnpj = s[2] };
                else return null;
            }
            else return null;
        }
        public static void SetValues(string login, string senha, string cnpj)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            File.WriteAllLines(fileName, new string[] { login, senha, cnpj });
        }
        public static void DeleteFile()
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
    }
}
