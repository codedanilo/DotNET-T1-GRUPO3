using System.Text.Json;

namespace SistemaMedico
{
    public static class Persistencia
    {
        private static string CaminhoMedicos = "./BancoDeDados/medicos.json";
        private static string CaminhooPacientes = "./BancoDeDados/pacientes.json";
        private static string CaminhoAtendimentos = "./BancoDeDados/atendimentos.json";

        public static void GravarMedicos(ListaDeMedicos medicos) => GravarLista(medicos.ListaMedicos, CaminhoMedicos);

        public static List<Medico> CarregarMedicos() => CarregarLista<Medico>(CaminhoMedicos);

        public static void GravarPacientes(ListaDePacientes pacientes) => GravarLista(pacientes.ListaPacientes, CaminhooPacientes);

        public static List<Paciente> CarregarPacientes() => CarregarLista<Paciente>(CaminhooPacientes);

        public static void GravarAtendimentos(ListaDeAtendimentos atendimentos) => GravarLista(atendimentos.ListaAtendimentos, CaminhoAtendimentos);

        public static List<Atendimento> CarregarAtendimento() => CarregarLista<Atendimento>(CaminhoAtendimentos);


        private static void GravarLista<T>(List<T> lista, string caminhoArquivo)
        {
            try
            {
                string json = JsonSerializer.Serialize(lista);
                File.WriteAllText(caminhoArquivo, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gravar dados: {ex.Message}");
            }
        }

        private static List<T> CarregarLista<T>(string caminhoArquivo)
        {
            try
            {
                if (File.Exists(caminhoArquivo))
                {
                    string json = File.ReadAllText(caminhoArquivo);
                    return JsonSerializer.Deserialize<List<T>>(json);
                }
                return new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados: {ex.Message}");
                return new List<T>();
            }
        }
    }
}
