namespace dotNET_P004 {
    public class Paciente : Pessoa
    {
        public List<string> Sintomas { get; set; }

        public Paciente(string nome, DateTime dataNascimento, string cpf, string sexo)
            : base(nome, dataNascimento,  cpf, sexo)
        {
            Sintomas = new List<string>();
        }

        public override string ToString()
        {
            return $"{Nome} \t {Cpf} \t {Sexo} \t {DataNascimento}";
        }
    }
}