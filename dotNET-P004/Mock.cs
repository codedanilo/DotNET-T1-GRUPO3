namespace dotNET_P004;
public static class Mock
{
    public static List<Medico> CriarMedicosAleatorios()
    {
        Random random = new Random();
        List<Medico> medicos = new List<Medico>();

        for (int i = 0; i < 5; i++)
        {
            string nome = $"Médico{i + 1}";
            DateTime dataNascimento = DateTime.Now.AddYears(-random.Next(30, 60));
            string cpf = GerarCPF();
            string sexo = random.Next(2) == 0 ? "Masculino" : "Feminino";
            string crm = $"CRM{i + 1}";

            Medico medico = new Medico(nome, dataNascimento, cpf, sexo, crm);
            medicos.Add(medico);
        }

        return medicos;
    }

    public static List<Paciente> CriarPacientesAleatorios()
    {
        Random random = new Random();
        List<Paciente> pacientes = new List<Paciente>();

        for (int i = 0; i < 5; i++)
        {
            string nome = $"Paciente{i + 1}";
            DateTime dataNascimento = DateTime.Now.AddYears(-random.Next(18, 80));
            string cpf = GerarCPF();
            string sexo = random.Next(2) == 0 ? "Masculino" : "Feminino";

            Paciente paciente = new Paciente(nome, dataNascimento, cpf, sexo);
            pacientes.Add(paciente);
        }

        return pacientes;
    }

    private static string GerarCPF()
    {
        Random random = new Random();
        return $"{random.Next(100, 999)}{random.Next(100, 999)}{random.Next(100, 999)}{random.Next(10, 99)}";
    }
}