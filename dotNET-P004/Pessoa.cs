namespace dotNET_P004 {
    public class Pessoa
    {
        private string nome;
        private DateTime dataNascimento;
        private string cpf;
        private string sexo;

        public string Nome 
        { 
            get { return nome; } 
            set 
            {
                if (value.Trim().Length < 3)
                {
                    throw new ArgumentException("O nome deve conter pelo menos 3 caracteres.");
                }

                nome = value;
            }
        }
        public DateTime DataNascimento 
        { 
            get { return dataNascimento; }
            set
            {
                if (!Util.ehDataValida(value.ToString()) || value > DateTime.Now)
                {
                    throw new ArgumentException("Data inválida.");
                }
                
                dataNascimento = value; 
            }
        }
        public string Cpf
        {
            get { return cpf; }
            set
            {
                if (!Util.ehCpfValido(value))
                {
                    throw new ArgumentException("O CPF deve conter 11 dígitos.");
                }

                cpf = value;
            }
        }

        public string Sexo
        {
            get { return sexo; }
            set
            {
                if (!Util.ehSexoValido(value))
                {
                    throw new ArgumentException("O sexo dever ser 'masculino' ou 'feminino'.");
                }

                sexo = value;
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