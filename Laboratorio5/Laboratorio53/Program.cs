internal class Program
{
    static void Main(string[] args)
    {
        List<int> calificaciones = new List<int> { 85, 90, 78, 92, 88 };

        int suma = 0;

        foreach (int calificacion in calificaciones)
        {
            suma += calificacion;
        }
        double promedio = (double)suma / calificaciones.Count;

        Console.WriteLine($"El promedio de las calificaciones es: {promedio}");
    }

}