internal class program
{
    private static void Main(string[] args)
    {
        ClaseConcretal concretal = new ClaseConcretal();
        concretal.printOut();
        Console.WriteLine(concretal.prefixValor("ES_"));

        ClaseConcretal2 concreta2 = new ClaseConcretal2();
        concreta2.printOut();
        Console.WriteLine(concreta2.prefixValor("ES_"));

    }
}