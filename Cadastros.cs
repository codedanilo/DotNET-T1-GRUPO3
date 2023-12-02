using System.Globalization;

namespace SistemaMedico
{
    public static class Cadastros
    {
        public static void CadastrarMedico(ListaDeMedicos medicos)
        {
            Console.Clear();

            Console.WriteLine("---- Cadastro de Médico ----");

            Console.Write("Nome: ");
            string? nome = Console.ReadLine() ?? "";

            Console.Write("Data de Nascimento (DD/MM/AAAA): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataNascimento))
            {
                Console.Write("CPF: ");
                string? cpf = Console.ReadLine() ?? "";

                Console.Write("CRM: ");
                string? crm = Console.ReadLine() ?? "";

                try
                {
                    Medico novoMedico = new Medico(nome, dataNascimento, cpf, crm);
                    medicos.AdicionarMedico(novoMedico);
                    Console.WriteLine("Médico cadastrado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao cadastrar médico: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Formato de data inválido. Tente novamente.");
            }

            Console.WriteLine("Pressione qualquer tecla para voltar ao Menu Principal...");
            Console.ReadKey();
        }

    public static void CadastrarPaciente(ListaDePacientes pacientes)
    {
        Console.Clear();

        Console.WriteLine("---- Cadastro de Pacientes ----");

        Console.Write("Nome: ");
        string nome = Console.ReadLine() ?? "";

        Console.Write("Data de Nascimento (DD/MM/AAAA): ");
        if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataNascimento))
        {
            Console.Write("CPF: ");
            string? cpf = Console.ReadLine() ?? "";

            Console.Write("Sexo (opcional - pressione Enter para pular): ");
            string? sexo = Console.ReadLine();
            
            try
            {   
                Paciente novoPaciente = string.IsNullOrWhiteSpace(sexo)
                    ? new Paciente(nome, dataNascimento, cpf)
                    : new Paciente(nome, dataNascimento, cpf, sexo);

                Console.WriteLine("Adicione os sintomas do paciente. Digite '0' para parar.");
                do
                {
                    Console.Write("Sintoma: ");
                    string? sintoma = Console.ReadLine() ?? "";

                    if (sintoma.ToLower() == "0")
                        break;

                    novoPaciente.AdicionarSintoma(sintoma);

                    Console.WriteLine("Sintoma adicionado com sucesso!");
                } while (true);

                pacientes.AdicionarPaciente(novoPaciente);
                Console.WriteLine("Paciente cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar paciente: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Formato de data inválido. Tente novamente.");
        }

        Console.WriteLine("Pressione qualquer tecla para voltar ao Menu Principal...");
        Console.ReadKey();
    }

    }

}