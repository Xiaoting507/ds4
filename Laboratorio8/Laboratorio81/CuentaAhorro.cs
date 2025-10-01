public class CuentaAhorro : Cuenta
{
    public CuentaAhorro(string prmtIdCuenta) : base(prmtIdCuenta)
    {
    }
    public override void CalcularInteres()
    {
        System.Console.WriteLine("CuentaAhorro.CalcularInteres() efectuando para " + "la cuenta {0}", getIdcuenta());
    }
}