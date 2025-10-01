public partial class Coordenadas
{
    private int x;
    private int y;

    public Coordenadas(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public partial class Coordenadas
{
    public void VerCoordenadas()
    {
        System.Console.WriteLine("Coordenadas: {0} , {1}", x, y);
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        Coordenadas misCoords = new Coordenadas(10, 15);
        misCoords.VerCoordenadas();
    }
}