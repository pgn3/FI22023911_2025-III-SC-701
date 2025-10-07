using System;
using System.Collections.Generic;
using PP2App.Models;

namespace PP2App.Helpers
{
    // Helper que contiene la lógica para procesar los datos binarios y retornar los resultados en diferentes formatos
    public static class BinaryHelper
    {
        private static string Normalize(string bin) // Asegura que la cadena binaria tenga al menos 8 bits
        {
            return bin.PadLeft(8, '0');
        }

        private static int ToInt(string bin) // Convierte una cadena binaria a su valor decimal
        {
            return Convert.ToInt32(bin, 2);
        }

        private static string ToBin(int num) // Convierte un número decimal a su representación binaria
        {
            return Convert.ToString(num, 2);
        }

        private static string BinaryAnd(string a, string b) // Realiza la operación AND bit a bit entre dos cadenas binarias
        {
            int len = Math.Min(a.Length, b.Length);
            string result = "";
            for (int i = 0; i < len; i++)
                result += (a[i] == '1' && b[i] == '1') ? "1" : "0";
            return result;
        }

        private static string BinaryOr(string a, string b) // Realiza la operación OR bit a bit
        {
            int len = Math.Min(a.Length, b.Length);
            string result = "";
            for (int i = 0; i < len; i++)
                result += (a[i] == '1' || b[i] == '1') ? "1" : "0";
            return result;
        }

        private static string BinaryXor(string a, string b) // Realiza la operación XOR bit a bit
        {
            int len = Math.Min(a.Length, b.Length);
            string result = "";
            for (int i = 0; i < len; i++)
                result += (a[i] != b[i]) ? "1" : "0";
            return result;
        }

        public static List<ResultModel> Process(string a, string b) // procesa las 2 entradas binarias y retorna los resultados en diferentes formatos
        {
            var results = new List<ResultModel>();

            a = Normalize(a);
            b = Normalize(b);

            int aDec = ToInt(a);
            int bDec = ToInt(b);

            var ops = new Dictionary<string, int>
            {
                { "a", aDec },
                { "b", bDec },
                { "a AND b", ToInt(BinaryAnd(a,b)) },
                { "a OR b",  ToInt(BinaryOr(a,b)) },
                { "a XOR b", ToInt(BinaryXor(a,b)) },
                { "a + b", aDec + bDec },
                { "a • b", aDec * bDec }
            };

            foreach (var op in ops)
            {
                results.Add(new ResultModel
                {
                    Label = op.Key,
                    Bin = ToBin(op.Value),
                    Oct = Convert.ToString(op.Value, 8),
                    Dec = op.Value.ToString(),
                    Hex = Convert.ToString(op.Value, 16).ToUpper()
                });
            }

            return results;
        }
    }
}
