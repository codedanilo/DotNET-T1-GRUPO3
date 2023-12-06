class Paciente : Pessoa
{
    public string Sexo { get; set; }
    public List<string> Sintomas { get; set; }
    public PlanoSaude PlanoSaude { get; set; }
    public List<Pagamento> Pagamentos { get; set; }

    public Paciente(string nome, DateTime dataNascimento, string cpf, string sexo, List<string> sintomas, PlanoSaude planoSaude)
        : base(nome, dataNascimento, cpf)
    {
        Sexo = sexo;
        Sintomas = sintomas;
        PlanoSaude = planoSaude;
        Pagamentos = new List<Pagamento>();
    }
}
