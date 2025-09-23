using System;

namespace PP1App
{
    class Program
    {
        static void Main(string[] args)
        {
            int max = int.MaxValue;
            //int max = 100; // Para pruebas rápidas

            Console.WriteLine("• SumFor:");
            RunSumFor(max);

            Console.WriteLine("\n• SumIte:");
            RunSumIte(max);
        }

        // Fórmula directa
        static int SumFor(int n)
        {
            return n * (n + 1) / 2;
        }

        // Iterativa
        static int SumIte(int n)
        {
            int sum = 0;
            for (int i = 1; i <= n; i++)
            {
                sum += i;
            }
            return sum;
        }

        // Ejecuta SumFor en orden ascendente y descendente
        static void RunSumFor(int max)
        {
            // Ascendente
            int lastValidN = 0;
            int lastValidSum = 0;
            for (int n = 1; n <= max; n++)
            {
                int sum = SumFor(n);
                if (sum > 0)
                {
                    lastValidN = n;
                    lastValidSum = sum;
                }
                else break;
            }
            Console.WriteLine($"\t◦ From 1 to Max → n: {lastValidN} → sum: {lastValidSum}");

            // Descendente
            int firstValidN = 0;
            int firstValidSum = 0;
            for (int n = max; n >= 1; n--)
            {
                int sum = SumFor(n);
                if (sum > 0)
                {
                    firstValidN = n;
                    firstValidSum = sum;
                    break;
                }
            }
            Console.WriteLine($"\t◦ From Max to 1 → n: {firstValidN} → sum: {firstValidSum}");
        }

        // Ejecuta SumIte en orden ascendente y descendente
        static void RunSumIte(int max)
        {
            // Ascendente
            int lastValidN = 0;
            int lastValidSum = 0;
            for (int n = 1; n <= max; n++) // Ciclo que va de 1 a max
            {
                int sum = SumIte(n);
                if (sum > 0)
                {
                    lastValidN = n;
                    lastValidSum = sum;
                }
                else break;
            }
            Console.WriteLine($"\t◦ From 1 to Max → n: {lastValidN} → sum: {lastValidSum}");

            // Descendente
            int firstValidN = 0;
            int firstValidSum = 0;
            for (int n = max; n >= 1; n--) // Ciclo que va de max a 1
            {
                int sum = SumIte(n);
                if (sum > 0)
                {
                    firstValidN = n;
                    firstValidSum = sum;
                    break;
                }
            }
            Console.WriteLine($"\t◦ From Max to 1 → n: {firstValidN} → sum: {firstValidSum}");
        }
    }
}
