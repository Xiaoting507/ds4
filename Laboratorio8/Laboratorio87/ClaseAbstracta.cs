abstract class ClaseAbstracta
{
    abstract protected string tomarValor();
    abstract public string prefixValor(string prefix);

    public void printOut()
        {
        Console.WriteLine(tomarValor());
    }
}