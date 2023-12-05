namespace SistemaMedico
{
    public class Paciente : Pessoa
    {
        private string sexo = "NI";
        private List<string> sintomas = new List<string>();

        public string Sexo
        {
            get { return sexo; }
            set
            {
                if (Validacoes.ValidarSexo(value))
                {
                    sexo = value;
                }
                else
                {
                    throw new ArgumentException("Inválida inserção de sexo (Insira (masculino) ou (feminino))");
                }
            }
        }

        public List<string> Sintomas
        {
            get { return sintomas; }
            set { sintomas = value; }
        }

        public void AdicionarSintoma(string sintoma)
        {
            if (string.IsNullOrWhiteSpace(sintoma))
            {
                throw new ArgumentException("O sintoma não pode ser vazio ou nulo", nameof(sintoma));
            }

            sintomas.Add(sintoma);
        }

        public Paciente(string nome, DateTime dataNascimento, string cpf, string? sexo = "NI")
            : base(nome, dataNascimento, cpf)
        {
            if (!sexo.Equals("NI") && !Validacoes.ValidarSexo(sexo)){
                throw new ArgumentException("Inválida inserção de sexo (Insira (masculino) ou (feminino))");
            }

            Sintomas = new List<string>();
        }

        public override string ToString()
        {
            string sintomasStr = Sintomas.Count > 0 ? string.Join(", ", Sintomas) : "Não informado";
            return $"Paciente:\nNome: {Nome}\nData de Nascimento: {DataNascimento:dd/MM/yyyy}\nCPF: {CPF}\nSexo: {Sexo}\nSintomas: {sintomasStr}";
        }

    }
}
