namespace Laboratorio92
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double a, b, c;

            Console.WriteLine("Ingresa 3 lados del triangulo (a, b, c): ");

             a = Convert.ToDouble(Console.ReadLine());
             b = Convert.ToDouble(Console.ReadLine());
             c = Convert.ToDouble(Console.ReadLine());

            if (a + b > c && a + c > b && b + c > a)
            {
                string tipo = (a == b && b == c) ? "EQUILÁTERO" : (a == b || a == c || b == c) ? "ISÓSCELES" : "ESCALENO";

                Console.WriteLine(tipo);
            }
            else
            {
                Console.WriteLine("Error!.... ESTO NO ES UN TRIANGULO");
            }
        }
    }
}