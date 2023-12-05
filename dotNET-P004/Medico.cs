namespace dotNET_P004 {
    public class Medico : Pessoa
    {
        public string Crm { get; set; }

        public Medico(string nome, DateTime dataNascimento, string cpf, string sexo, string crm)
            : base(nome, dataNascimento,  cpf, sexo)
        {
            this.Crm = crm;
        }

        public override string ToString()
        {
            return $"{Nome} \t {Cpf} \t {Sexo} \t {DataNascimento} \t {Crm}";
        }
    }
}