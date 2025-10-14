using parcial1;
using System;

namespace Laboratorio94
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Ingrese la dimensión de la matriz (N x N, debe ser impar): ");
                int N = int.Parse(Console.ReadLine());

                MatrizPatron matriz = new MatrizPatron(N);
                matriz.GenerarMatriz();
                matriz.MostrarMatriz();
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: debe ingresar un número válido.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
            }
        }
    }
}
