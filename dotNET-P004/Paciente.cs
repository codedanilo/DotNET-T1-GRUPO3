public class Paciente : Pessoa
{
    public List<string> Sintomas { get; set; }

    public Paciente()
    {
        Sintomas = new List<string>();
    }
}