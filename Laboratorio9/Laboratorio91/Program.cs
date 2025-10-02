using System;

namespace Laboratorio91
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Números del 1 al 100 que son pares o divisibles entre 3:");

            for (int i = 1; i <= 100; i++)
            {
                if (i % 2 == 0 || i % 3 == 0)
                {
                    if (i < 100)
                        Console.Write(i + ", ");
                    else
                        Console.Write(i);
                }
            }
        }
    }
}
