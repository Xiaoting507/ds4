using System;

namespace parcial1
{
    class MatrizPatron
    {
        private int[,] matriz;
        private Random random;
        private int N;
        private int suma;

        public MatrizPatron(int dimension)
        {
            if (dimension % 2 == 0)
                throw new ArgumentException("La dimensión debe ser impar.");

            N = dimension;
            matriz = new int[N, N];
            random = new Random();
            suma = 0;
        }

        public void GenerarMatriz()
        {
            int medio = N / 2; 

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                 
                    if (i == 0 || i == N - 1 || j == medio)
                    {
                        matriz[i, j] = random.NumeroEntre(101, 200);
                        suma += matriz[i, j];
                    }
                    else
                    {
                        matriz[i, j] = 0;
                    }
                }
            }
        }

        public void MostrarMatriz()
        {
            Console.WriteLine("\nMatriz generada:\n");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write($"{matriz[i, j],5}");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\nSuma de los elementos del patrón: {suma}");
        }
    }

    
    public static class RandomExtensions
    {
        public static int NumeroEntre(this Random random, int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue + 1);
        }
    }
}