namespace dotNET_P004;

public static class Service
{
    public static List<Paciente> listaPacientes = new List<Paciente>();
    public static List<Medico> listaMedicos = new List<Medico>();
    public static List<Atendimento> listaAtendimentos = new List<Atendimento>();

    #region CRUD Paciente
    public static void AddPaciente(Paciente paciente) => listaPacientes.Add(paciente);
    public static void RemoverPaciente(Paciente paciente) => listaPacientes.Remove(paciente);
    public static void ListarPacientes() => listaPacientes.ForEach(x => Console.WriteLine(x.ToString()));
    #endregion

    #region CRUD Medico
    public static void AddMedico(Medico medico) => listaMedicos.Add(medico);
    public static void RemoverMedico(Medico medico) => listaMedicos.Remove(medico);
    public static void ListarMedicos() => listaMedicos.ForEach(x => Console.WriteLine(x.ToString()));
    #endregion

    #region CRUD Atendimento
    public static void AddAtendimento(Atendimento medico) => listaAtendimentos.Add(medico);
    public static void RemoverAtendimento(Atendimento medico) => listaAtendimentos.Remove(medico);
    public static void ListarAtendimentos() => listaAtendimentos.ForEach(x => Console.WriteLine(x.ToString()));
    #endregion
}