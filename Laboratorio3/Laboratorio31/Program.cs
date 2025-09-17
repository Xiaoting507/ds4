using System;

namespace Laboratorio31
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese el primer número(a): ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el segundo número(b): ");
            int b = Convert.ToInt32(Console.ReadLine());
            
            CalculosMatematicos calculos = new CalculosMatematicos();

            int resultadoSuma = calculos.Sumar(a, b);

            Console.WriteLine("El resultado de la suma es: {0}", resultadoSuma);

        }
    }

    public class CalculosMatematicos
    {
        public int Sumar(int a, int b)
        {
            return (a + b) * (a - b);
        }
    }



}