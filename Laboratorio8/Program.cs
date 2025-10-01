namespace Laboratorio8
{
    class Program
    {
        static void Main(string[] args)
        {
            Trabajador p = new Trabajador("Josan", 22, "77588260-Z", 10000);
            Console.WriteLine("Nombre= " + p.Nombre);
            Console.WriteLine("Edad= " + p.Edad);
            Console.WriteLine("NIF= " + p.NIF);
            Console.WriteLine("Sueldo= " + p.Sueldo);
            Console.ReadKey();

        }
    }
}