internal class Program
{
    static Dictionary<string, string> paisesYCapitales = new Dictionary<string, string>
    { 
        {"Francia","Paris" },
        {"España","Madrid" },
        {"Italia","Roma" }
    };
    static void Main(string[] args)
    {
        foreach (KeyValuePair<string, string> par in paisesYCapitales)
        {
            Console.WriteLine("La capital de " + par.Key + " es " + par.Value + "." );
        }
    }
}