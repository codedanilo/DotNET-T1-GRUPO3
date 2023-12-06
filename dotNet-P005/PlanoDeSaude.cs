public class PlanoDeSaude
{
    public string Titulo { get; set; }

    public double ValorPorMes { get; set; }

    public PlanoDeSaude(string titulo, double valorPorMes)
    {
        Titulo = titulo;
        ValorPorMes = valorPorMes;
    }
}

