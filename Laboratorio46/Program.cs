class Program
{
    static void Main(string[] args)
    {
        int numeroUno = 70;
        double numeroDos = 67.89;
        double numeroTres = 67.89;

        Console.Write(Suma(numeroUno, numeroDos));
        Console.Write(Suma(numeroUno, numeroDos, numeroTres));

    }
    static double Suma(int x, double y, double z = 0)
    {
        return x + y + z;
    }
}