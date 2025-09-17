using System;

namespace Laboratorio33
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Cálculo del Perímetro de un Rectángulo");

            Console.Write("Ingrese el valor del largo: ");
            int largo = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese el valor del ancho: ");
            int ancho = Convert.ToInt32(Console.ReadLine());

            CalculosMatematicos calculo = new CalculosMatematicos();
            int perimetro = calculo.CalculoPerimetro(largo, ancho);

            Console.WriteLine("El perímetro del rectángulo es: {0}", perimetro);
        }
    }

    public class CalculosMatematicos
    {
        public int CalculoPerimetro(int largo, int ancho)
        {
            return 2 * (largo + ancho);
        }
    }

}
