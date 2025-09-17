using System;

namespace Laboratorio32
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(" Cálculo del Área de un Círculo ");
            Console.Write("Ingrese el radio del círculo: ");

            double radio = Convert.ToDouble(Console.ReadLine());

            CalculoArea calculo = new CalculoArea();
            double area = calculo.CalculoAreas(radio);

            Console.WriteLine("El área del círculo con radio {0} es: {1:F2}", radio, area);
        }
    }
    public class CalculoArea
    {
        public double CalculoAreas(double radio)
        {
            double pi = 3.1416; 
            return pi * (radio * radio);
        }
    }


}
