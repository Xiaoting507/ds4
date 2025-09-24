using System.Runtime.CompilerServices;

internal class Program
    {
    private int[] sueldos;

    public void Cargar()
        {
        sueldos = new int[6];
        for (int f = 1; f <=5; f++)
            {
            Console.Write("Ingrese el sueldo del operario " +f+ ": ");
            string linea;
            linea = Console.ReadLine();
            sueldos[f] = int.Parse(linea);
        }

    }
    public void imprimir()
        {
        
        Console.WriteLine("Los 5 sueldos de los aprerarios \n");
        for (int f = 1; f <=5; f++)
            {
            Console.WriteLine("["+sueldos[f]+"]");
        }
        Console.ReadKey();
    }
    static void Main(string[] args)
        {
        Program p = new Program();
        p.Cargar();
        p.imprimir();
    }
}