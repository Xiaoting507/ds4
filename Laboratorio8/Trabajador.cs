class Trabajador : Persona
{
    public int Sueldo;

    public Trabajador(String nombre, int edad, string nif, int sueldo) : base(nombre, edad, nif)
    {
        Sueldo = sueldo;
    }
}