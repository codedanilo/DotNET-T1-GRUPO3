namespace dotNET_P004 {
    public class Pessoa
    {
        public string Nome { get; set; }
        public DateTime DataNascimento 
        { 
            get { return DataNascimento; }
            set
            {
                if (!Util.ehDataValida(value.ToString()))
                {
                    throw new ArgumentException("Data inválida.");
                }
            } 
        }
        public string Cpf
        {
            get { return Cpf; }
            set
            {
                if (!Util.ehCpfValido(value))
                {
                    throw new ArgumentException("O CPF deve conter 11 dígitos.");
                }
            }
        }

        public string Sexo
        {
            get { return Sexo; }
            set
            {
                if (!Util.ehSexoValido(value))
                {
                    throw new ArgumentException("O sexo dever ser 'masculino' ou 'feminino'.");
                }
            }
        }
    }
}