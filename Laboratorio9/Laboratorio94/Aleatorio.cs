class Aleatorio
{
    private Random random;

    public Aleatorio()
    {
        random = new Random();
    }

    public int NumeroEntre(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    public int[] ArregloEntre(int min, int max, int cantidad)
    {
        int[] arreglo = new int[cantidad];
        for (int i = 0; i < cantidad; i++)
        {
            arreglo[i] = NumeroEntre(min, max);
        }
        return arreglo;
    }

    public int[] GenerarArregloNoRepetido()
    {
        int min = 1;
        int max = 50;
        int cantidad = 10;

        List<int> listaNumeros = new List<int>();

        while (listaNumeros.Count < cantidad)
        {
            int nuevoNumero = random.Next(min, max + 1);

            if (!listaNumeros.Contains(nuevoNumero))
            {
                listaNumeros.Add(nuevoNumero);
            }
        }
        return listaNumeros.ToArray();
    }
}