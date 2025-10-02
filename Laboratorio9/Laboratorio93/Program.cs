namespace Laboratorio93
{
    class Program
    {
        static void Main(string[] args)
        {
            Aleatorios aleatorios = new Aleatorios();
            Console.WriteLine("Numero entre 5 y 10: " + aleatorios.NumeroEntre(1, 10));

            int[] arreglo = aleatorios.ArregloEntre(5, 10, 50);
            Console.WriteLine("Arreglo aleatorio: " + string.Join(", ", arreglo));
        }
    }
}
