namespace dotNET_P004 {
    public class Paciente : Pessoa
    {
        public List<string> Sintomas { get; set; }

        public Paciente()
        {
            Sintomas = new List<string>();
        }

        public override string ToString()
        {
            return $"{Nome} \t {Cpf} \t {Sexo} \t {DataNascimento}";
        }
    }
}