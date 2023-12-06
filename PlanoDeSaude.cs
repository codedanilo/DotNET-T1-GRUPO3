class PlanoSaude
{
    public string Titulo { get; set; }
    public double ValorPorMes { get; set; }

    public PlanoSaude(string titulo, double valorPorMes)
    {
        Titulo = titulo;
        ValorPorMes = valorPorMes;
    }
}
