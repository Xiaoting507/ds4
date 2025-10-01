internal class program
{
    private static void Main(string[] args)
    {
        Empleado empleado = new Empleado();
        empleado.Nombre = "John Doe";
        Console.WriteLine($"Nombre del empleado: {empleado.Nombre}");

        CuentaBancaria cta = new CuentaBancaria();
        cta.Saldo = 100;
        Console.WriteLine($"Saldo del empleado: {cta.Saldo}");

        Cobertura c = new Cobertura(5);
        Console.WriteLine($"Con una cobertura de: {c.Radio}");

    }

}