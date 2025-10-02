using System;
using System.Linq;

namespace Laboratorio9
{
    class Program
    {
        static void Main(string[] args)
        {
            double precio;

            
            do
            {
                Console.WriteLine("Ingrese el precio del producto:");
                if (!double.TryParse(Console.ReadLine(), out precio) || precio < 0)
                {
                    Console.WriteLine("El precio no puede ser negativo ni un valor no numérico. Intente nuevamente.");
                    precio = -1;
                }
            } while (precio < 0);

            Console.WriteLine("Precio ingresado del producto es: " + precio);

            Console.WriteLine("Ingrese la forma de pago (Efectivo o Tarjeta):");
            string formaPago = Console.ReadLine().Trim().ToLower();

            if (formaPago == "tarjeta")
            {
                string NumeroCuenta;

                
                do
                {
                    Console.WriteLine("Ingrese el número de cuenta (16 dígitos):");
                    NumeroCuenta = Console.ReadLine().Trim();
                    if (NumeroCuenta.Length != 16 || !NumeroCuenta.All(char.IsDigit))
                    {
                        Console.WriteLine("Número de cuenta inválido. Debe tener exactamente 16 dígitos. Intente nuevamente.");
                    }
                } while (NumeroCuenta.Length != 16 || !NumeroCuenta.All(char.IsDigit));

                Console.WriteLine($"\nPrecio: {precio:C}");
                Console.WriteLine("Forma de pago: Tarjeta");
                Console.WriteLine($"Número de cuenta: {NumeroCuenta}");
            }
            else if (formaPago == "efectivo")
            {
                double montoRecibido;
                do
                {
                    Console.WriteLine("Ingrese el monto recibido:");
                    if (!double.TryParse(Console.ReadLine(), out montoRecibido) || montoRecibido < precio)
                    {
                        Console.WriteLine("El monto recibido es insuficiente o inválido. Intente nuevamente.");
                        montoRecibido = -1;
                    }
                } while (montoRecibido < precio);

                double cambio = montoRecibido - precio;

                Console.WriteLine($"\nPrecio: {precio:C}");
                Console.WriteLine("Forma de pago: Efectivo");
                Console.WriteLine($"Monto recibido: {montoRecibido:C}");
                Console.WriteLine($"Cambio a devolver: {cambio:C}");
            }
            else
            {
                Console.WriteLine("Forma de pago inválida. Debe ser 'Efectivo' o 'Tarjeta'.");
            }
        }
    }
}
