using System.Text.RegularExpressions;

namespace dotNET_P004 {
    public class Pessoa
    {
        public string Nome { get; set; }
        public DateTime DataNascimento 
        { 
            get { return DataNascimento; }
            set
            {
                if (!ehDataValida(value.ToString()))
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
                if (value.Length != 11)
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
                if (value.ToLower() != "masculino" || value.ToLower() != "feminino")
                {
                    throw new ArgumentException("O sexo dever ser 'masculino' ou 'feminino'.");
                }
            }
        }

        private bool ehDataValida(string value) 
        {
            Regex r = new Regex(@"(\d{2}\/\d{2}\/\d{4})");
            string valueAsString = value.ToString()!;
            
            return r.Match(valueAsString).Success;
        }
    }
}