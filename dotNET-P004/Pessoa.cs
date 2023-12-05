namespace dotNET_P004 {
    public class Pessoa
    {
        public string Nome 
        { 
            get { return Nome; } 
            set 
            {
                if (value.Trim().Length < 3)
                {
                    throw new ArgumentException("O nome deve conter pelo menos 3 caracteres.");
                }
            }
        }
        public DateTime DataNascimento 
        { 
            get { return DataNascimento; }
            set
            {
                if (!Util.ehDataValida(value.ToString()) || value > DateTime.Now)
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

        public Pessoa(string nome, DateTime dataNascimento,  string cpf, string sexo)
        {
            this.Nome = nome;
            this.DataNascimento = dataNascimento;
            this.Cpf = cpf;
            this.Sexo = sexo;
        }
    }
}