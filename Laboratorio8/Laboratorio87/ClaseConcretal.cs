class ClaseConcretal : ClaseAbstracta
{
    protected override string tomarValor()
    {
        return "ClaseConcretal";
    }

    public override string prefixValor(string prefix)
    {
        return $"{prefix}ClaseConcretal";
    }
}
