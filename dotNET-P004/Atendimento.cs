namespace dotNET_P004 {
    public class Atendimento
    {
        public DateTime Inicio 
        { 
            get { return Inicio; }
            set
            {
                if (!Util.ehDataValida(value, Fim))
                {
                    throw new ArgumentException("Data inicial não pode ser posterior a data final.");
                }
            }
        }
        public string SuspeitaInicial { get; set; }
        public List<(Exame, string)> ListaExamesResultados { get; set; }
        public float Valor { get; set; }
        public DateTime Fim 
        { 
            get { return Fim; }
            set
            {
                if (!Util.ehDataValida(value, Fim))
                {
                    throw new ArgumentException("Data final não pode ser anterior a data inicial.");
                }
            } 
        }
        public Medico MedicoResponsavel { get; set; }
        public Paciente Paciente { get; set; }
        public string DiagnosticoFinal { get; set; }

        public override string ToString()
        {
            return $"{Inicio} \t {Fim} \t {Paciente.Nome} \t {MedicoResponsavel.Nome}";
        }
    }
}