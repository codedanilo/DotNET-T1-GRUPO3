using System;
using System.Collections.Generic;
using System.Linq;

class Pessoa
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }

    public Pessoa(string nome, DateTime dataNascimento, string cpf)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf.Length == 11 ? cpf : throw new ArgumentException("CPF deve conter 11 dígitos");
    }
}

class Médico : Pessoa
{
    public string CRM { get; set; }
    public List<Atendimento> Atendimentos { get; set; }

    public Médico(string nome, DateTime dataNascimento, string cpf, string crm)
        : base(nome, dataNascimento, cpf)
    {
        CRM = crm;
        Atendimentos = new List<Atendimento>();
    }
}

class Paciente : Pessoa
{
    public string Sexo { get; set; }
    public List<string> Sintomas { get; set; }
    public List<Atendimento> Atendimentos { get; set; }

    public Paciente(string nome, DateTime dataNascimento, string cpf, string sexo, List<string> sintomas)
        : base(nome, dataNascimento, cpf)
    {
        Sexo = sexo;
        Sintomas = sintomas;
        Atendimentos = new List<Atendimento>();
    }
}

class Exame
{
    public string Título { get; set; }
    public float Valor { get; set; }
    public string Descrição { get; set; }
    public string Local { get; set; }
}

class Atendimento
{
    public DateTime Início { get; set; }
    public string SuspeitaInicial { get; set; }
    public List<(Exame, string)> ListaExamesResultados { get; set; }
    public float Valor { get; set; }
    public DateTime Fim { get; set; }
    public Médico MédicoResponsável { get; set; }
    public Paciente Paciente { get; set; }
    public string DiagnósticoFinal { get; set; }

    public void IniciarAtendimento(string suspeita)
    {
        Início = DateTime.Now;
        SuspeitaInicial = suspeita;
    }

    public void FinalizarAtendimento(string diagnóstico)
    {
        Fim = DateTime.Now;
        DiagnósticoFinal = diagnóstico;
    }
}

class Consultório
{
    public List<Médico> Médicos { get; set; }
    public List<Paciente> Pacientes { get; set; }
    public List<Exame> Exames { get; set; }
    public List<Atendimento> Atendimentos { get; set; }

    public Consultório()
    {
        Médicos = new List<Médico>();
        Pacientes = new List<Paciente>();
        Exames = new List<Exame>();
        Atendimentos = new List<Atendimento>();
    }

    public void CadastrarMédico(string nome, DateTime dataNascimento, string cpf, string crm)
    {
        Médico médico = new Médico(nome, dataNascimento, cpf, crm);
        Médicos.Add(médico);
        Console.WriteLine("Médico cadastrado com sucesso!");
    }

    public void CadastrarPaciente(string nome, DateTime dataNascimento, string cpf, string sexo, List<string> sintomas)
    {
        Paciente paciente = new Paciente(nome, dataNascimento, cpf, sexo, sintomas);
        Pacientes.Add(paciente);
        Console.WriteLine("Paciente cadastrado com sucesso!");
    }

    public void AgendarAtendimento(string cpfMedico, string cpfPaciente, string suspeita)
    {
        Médico médico = Médicos.FirstOrDefault(m => m.CPF == cpfMedico);
        Paciente paciente = Pacientes.FirstOrDefault(p => p.CPF == cpfPaciente);

        if (médico != null && paciente != null)
        {
            Atendimento atendimento = new Atendimento
            {
                MédicoResponsável = médico,
                Paciente = paciente
            };
            atendimento.IniciarAtendimento(suspeita);
            Atendimentos.Add(atendimento);
            médico.Atendimentos.Add(atendimento);
            paciente.Atendimentos.Add(atendimento);
            Console.WriteLine("Atendimento agendado com sucesso!");
        }
        else
        {
            Console.WriteLine("Médico ou paciente não encontrado.");
        }
    }

    public void FinalizarAtendimento(string cpfPaciente, string diagnóstico)
    {
        Paciente paciente = Pacientes.FirstOrDefault(p => p.CPF == cpfPaciente);
        if (paciente != null)
        {
            Atendimento atendimento = paciente.Atendimentos.LastOrDefault();
            if (atendimento != null)
            {
                atendimento.FinalizarAtendimento(diagnóstico);
                Console.WriteLine("Atendimento finalizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Nenhum atendimento encontrado para o paciente.");
            }
        }
        else
        {
            Console.WriteLine("Paciente não encontrado.");
        }
    }

    public void RelatórioPacientesOrdemAlfabética()
    {
        var pacientesOrdenados = Pacientes.OrderBy(p => p.Nome);

        foreach (var paciente in pacientesOrdenados)
        {
            Console.WriteLine($"Paciente: {paciente.Nome}");
        }
    }

    public void RelatórioPacientesComSintoma(string texto)
    {
        var pacientesComSintoma = Pacientes.Where(p => p.Sintomas.Contains(texto));

        foreach (var paciente in pacientesComSintoma)
        {
            Console.WriteLine($"Paciente: {paciente.Nome}, Sintomas: {string.Join(", ", paciente.Sintomas)}");
        }
    }

    public void RelatórioAniversariantesDoMês(int mês)
{
    var aniversariantes = Pacientes.Cast<Pessoa>().Concat(Médicos.Cast<Pessoa>())
        .Where(pessoa => pessoa.DataNascimento.Month == mês);

    foreach (var pessoa in aniversariantes)
    {
        Console.WriteLine($"Nome: {pessoa.Nome}, Data de Nascimento: {pessoa.DataNascimento}");
    }
}

    public void RelatórioAtendimentosEmAberto()
    {
        var atendimentosAbertos = Atendimentos.Where(atendimento => atendimento.Fim == DateTime.MinValue)
                                             .OrderByDescending(atendimento => atendimento.Início);

        foreach (var atendimento in atendimentosAbertos)
        {
            Console.WriteLine($"Paciente: {atendimento.Paciente.Nome}, Início: {atendimento.Início}");
        }
    }

    public void RelatórioMédicosPorAtendimentosConcluídos()
    {
        var médicosOrdenadosPorAtendimentos = Médicos.OrderByDescending(m => m.Atendimentos.Count(a => a.Fim != DateTime.MinValue));

        foreach (var médico in médicosOrdenadosPorAtendimentos)
        {
            Console.WriteLine($"Médico: {médico.Nome}, Atendimentos Concluídos: {médico.Atendimentos.Count(a => a.Fim != DateTime.MinValue)}");
        }
    }

    public void RelatórioAtendimentosPorPalavraChave(string palavra)
    {
        var atendimentosFiltrados = Atendimentos.Where(atendimento => atendimento.SuspeitaInicial.Contains(palavra)
                                                                  || atendimento.DiagnósticoFinal.Contains(palavra));

        foreach (var atendimento in atendimentosFiltrados)
        {
            Console.WriteLine($"Paciente: {atendimento.Paciente.Nome}, Suspeita: {atendimento.SuspeitaInicial}, Diagnóstico: {atendimento.DiagnósticoFinal}");
        }
    }

    public void RelatórioExamesMaisUtilizados()
    {
        var examesAgrupados = Atendimentos.SelectMany(a => a.ListaExamesResultados)
                                          .GroupBy(tuple => tuple.Item1.Título)
                                          .Select(group => new { Title = group.Key, Count = group.Count() })
                                          .OrderByDescending(item => item.Count);

        foreach (var exame in examesAgrupados)
        {
            Console.WriteLine($"Exame: {exame.Title}, Utilizado {exame.Count} vezes");
        }
    }
}

class Program
{
    static Consultório consultorio = new Consultório();

    static void Main(string[] args)
    {
        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Cadastrar Médico");
            Console.WriteLine("2. Cadastrar Paciente");
            Console.WriteLine("3. Agendar Atendimento");
            Console.WriteLine("4. Finalizar Atendimento");
            Console.WriteLine("5. Relatório Pacientes em Ordem Alfabética");
            Console.WriteLine("6. Relatório Pacientes por Sintoma");
            Console.WriteLine("7. Relatório Aniversariantes do Mês");
            Console.WriteLine("8. Relatório Atendimentos em Aberto");
            Console.WriteLine("9. Relatório Médicos por Atendimentos Concluídos");
            Console.WriteLine("10. Relatório Atendimentos por Palavra-chave");
            Console.WriteLine("11. Relatório Exames Mais Utilizados");
            Console.WriteLine("12. Sair");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarNovoMédico();
                    break;
                case "2":
                    CadastrarNovoPaciente();
                    break;
                case "3":
                    AgendarAtendimento();
                    break;
                case "4":
                    FinalizarAtendimento();
                    break;
                case "5":
                    consultorio.RelatórioPacientesOrdemAlfabética();
                    break;
                case "6":
                    RelatórioPacientesSintoma();
                    break;
                case "7":
                    RelatórioAniversariantes();
                    break;
                case "8":
                    consultorio.RelatórioAtendimentosEmAberto();
                    break;
                case "9":
                    consultorio.RelatórioMédicosPorAtendimentosConcluídos();
                    break;
                case "10":
                    RelatórioAtendimentosPalavraChave();
                    break;
                case "11":
                    consultorio.RelatórioExamesMaisUtilizados();
                    break;
                case "12":
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    static void CadastrarNovoMédico()
    {
        Console.WriteLine("Nome do médico:");
        string nome = Console.ReadLine();
        Console.WriteLine("Data de nascimento do médico (dd/mm/aaaa):");
        DateTime dataNascimento = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("CPF do médico:");
        string cpf = Console.ReadLine();
        Console.WriteLine("CRM do médico:");
        string crm = Console.ReadLine();

        consultorio.CadastrarMédico(nome, dataNascimento, cpf, crm);
    }

    static void CadastrarNovoPaciente()
    {
        Console.WriteLine("Nome do paciente:");
        string nome = Console.ReadLine();
        Console.WriteLine("Data de nascimento do paciente (dd/mm/aaaa):");
        DateTime dataNascimento = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("CPF do paciente:");
        string cpf = Console.ReadLine();
        Console.WriteLine("Sexo do paciente:");
        string sexo = Console.ReadLine();
        Console.WriteLine("Sintomas do paciente (separados por vírgula):");
        List<string> sintomas = Console.ReadLine().Split(',').ToList();

        consultorio.CadastrarPaciente(nome, dataNascimento, cpf, sexo, sintomas);
    }

    static void AgendarAtendimento()
    {
        Console.WriteLine("CPF do médico:");
        string cpfMedico = Console.ReadLine();
        Console.WriteLine("CPF do paciente:");
        string cpfPaciente = Console.ReadLine();
        Console.WriteLine("Suspeita para o atendimento:");
        string suspeita = Console.ReadLine();

        consultorio.AgendarAtendimento(cpfMedico, cpfPaciente, suspeita);
    }

    static void FinalizarAtendimento()
    {
        Console.WriteLine("CPF do paciente:");
        string cpfPaciente = Console.ReadLine();
        Console.WriteLine("Diagnóstico do atendimento:");
        string diagnóstico = Console.ReadLine();

        consultorio.FinalizarAtendimento(cpfPaciente, diagnóstico);
    }

    static void RelatórioPacientesSintoma()
    {
        Console.WriteLine("Texto do sintoma:");
        string texto = Console.ReadLine();

        consultorio.RelatórioPacientesComSintoma(texto);
    }

    static void RelatórioAniversariantes()
    {
        Console.WriteLine("Mês (número) para filtrar aniversariantes:");
        int mes = int.Parse(Console.ReadLine());

        consultorio.RelatórioAniversariantesDoMês(mes);
    }

    static void RelatórioAtendimentosPalavraChave()
    {
        Console.WriteLine("Palavra-chave para busca nos atendimentos:");
        string palavra = Console.ReadLine();

        consultorio.RelatórioAtendimentosPorPalavraChave(palavra);
    }
}
