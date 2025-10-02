namespace Laboratorio94
{
    class Program
    {
        static void Main(string[] args)
        {
            Aleatorio aleatorios = new Aleatorio();

            int[] arregloUnico = aleatorios.GenerarArregloNoRepetido();

            Console.WriteLine("Arreglo NO REPETIDO");
            Console.WriteLine("Arreglo generado: ");
            Console.WriteLine(string.Join(", ", arregloUnico));
        }
    }
}
