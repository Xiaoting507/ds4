internal class Program
    {
    private int[,] mat;

    public void Ingresar()
    {
        mat = new int[3, 4];
        for (int f = 0; f < 3; f++)
        {
            for (int c = 0; c < 4; c++)
            {
                Console.Write("Ingrese posición  [" + (f + 1)+","+ (c + 1)+ "]: ");
                string linea;
                linea = Console.ReadLine();
                mat[f, c] = int.Parse(linea);
            }
        }
    }
    public void imprimir()
    {
        for (int f = 0; f < 3; f++)
        {
            for (int c = 0; c < 4; c++)
            {
                Console.Write(mat[f, c] + " ");
            }
            Console.WriteLine();
        }
        Console.ReadKey();
    }
    
    static void Main(string[] args)
        {
        Program p = new Program();
        p.Ingresar();
        p.imprimir();
    }   
}