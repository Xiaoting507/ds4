interface iTemplate
{
    public void ponerVariable(string nombre, string var);
    public void verHtml(string template);
}

class Template : iTemplate
{
    public void ponerVariable(string nombre, string var)
    {
        Console.WriteLine("Metodo poner variable {nombre} : {var}");
    }

    public void verHtml(string template)
    {
        Console.WriteLine(template);
    }

}

internal class Program
{
    private static void Main(string[] args)
    {
        Template temp1 = new Template();
        temp1.ponerVariable("var1", "valor 1");
        temp1.ponerVariable("var1", "valor 1");
        temp1.ponerVariable("var1", "valor 1");
        temp1.verHtml("<br>Texto de prueba</b>");
    }
}