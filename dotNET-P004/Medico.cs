namespace dotNET_P004 {
    public class Medico : Pessoa
    {
        public string CRM { get; set; }

        public override string ToString()
        {
            return $"{Nome} \t {Cpf} \t {Sexo} \t {DataNascimento} \t {CRM}";
        }
    }
}