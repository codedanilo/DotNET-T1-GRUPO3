class Gerenciamento
{
    static List<Medico> medicos = new List<Medico>();
    static List<Paciente> pacientes = new List<Paciente>();
    static List<Atendimento> atendimentos = new List<Atendimento>();
    static List<Exame> examesUtilizados = new List<Exame>();
    static List<PlanoSaude> planosDeSaude = new List<PlanoSaude>();

    static void Main()
    {
        int opcao;

        do
        {
            Console.WriteLine("1 - Incluir paciente");
            Console.WriteLine("2 - Incluir médico");
            Console.WriteLine("3 - Iniciar Atendimento");
            Console.WriteLine("4 - Finalizar Atendimento");
            Console.WriteLine("5 - Exibir relatórios");
            Console.WriteLine("6 - Sair");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                IncluirPaciente();
            }
            else if (opcao == 2)
            {
                IncluirMedico();
            }
            else if (opcao == 3)
            {
                IniciarAtendimento();
            }
            else if (opcao == 4)
            {
                FinalizarAtendimento();
            }
            else if (opcao == 5)
            {
                ExibirRelatorios();
            }
            else if (opcao == 6)
            {
                Console.WriteLine("Programa Encerrado!");
            }
        } while (opcao != 6);
    }

    static void IncluirPaciente()
    {
        Console.WriteLine("Nome do Paciente: ");
        string _nome = Console.ReadLine();

        Console.WriteLine("Data de Nascimento no formato ano/mes/dia: ");
        DateTime _dataDeNascimento = DateTime.Parse(Console.ReadLine());

        Console.Write("CPF do paciente: ");
        string _cpf = Console.ReadLine();

        if (pacientes.Any(p => p.CPF == _cpf) || _cpf.Length != 11)
        {
            Console.WriteLine("CPF pertence a outro paciente ou não tem 11 dígitos");
            return;
        }

        Console.WriteLine("Sexo do paciente (M ou F): ");
        string _sexo = Console.ReadLine();

        Console.WriteLine("Sintomas do paciente: ");
        string _sintomas = Console.ReadLine();

        Paciente paciente = new Paciente(_nome, _dataDeNascimento, _cpf, _sexo, _sintomas);
        pacientes.Add(paciente);

        Console.WriteLine("Paciente cadastrado com sucesso!!\n");
    }

    static void IncluirMedico()
    {
        Console.WriteLine("Nome do Médico: ");
        string _nome = Console.ReadLine();

        Console.WriteLine("Data de Nascimento no formato ano/mes/dia: ");
        DateTime _dataDeNascimento = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("CPF do médico: ");
        string _cpf = Console.ReadLine();

        if (medicos.Any(m => m.CPF == _cpf) || _cpf.Length != 11)
        {
            Console.WriteLine("CPF pertence a outro médico ou não tem 11 dígitos");
            return;
        }

        Console.WriteLine("CRM do médico: ");
        string _crm = Console.ReadLine();

        if (medicos.Any(m => m.CRM == _crm))
        {
            Console.WriteLine("CRM pertence a outro médico");
            return;
        }

        Medico medico = new Medico(_nome, _dataDeNascimento, _cpf, _crm);
        medicos.Add(medico);

        Console.WriteLine("Médico adicionado com sucesso!!");
    }

    static void IniciarAtendimento()
    {
        Console.WriteLine("Selecione o paciente pelo CPF:");
        string cpfPaciente = Console.ReadLine();
        Paciente paciente = pacientes.FirstOrDefault(p => p.CPF == cpfPaciente);

        if (paciente == null)
        {
            Console.WriteLine("Paciente não encontrado.");
            return;
        }

        Console.WriteLine("Selecione o médico pelo CRM:");
        string crmMedico = Console.ReadLine();
        Medico medico = medicos.FirstOrDefault(m => m.CRM == crmMedico);

        if (medico == null)
        {
            Console.WriteLine("Médico não encontrado.");
            return;
        }

        Atendimento atendimento = new Atendimento
        {
            Inicio = DateTime.Now,
            SuspeitaInicial = Console.ReadLine(), 
            ListaExamesResultado = new List<(Exame, string)>(),
            Valor = 0,
            Fim = null,
            MedicoResponsavel = medico,
            Paciente = paciente,
            DiagnosticoFinal = ""
        };

        atendimentos.Add(atendimento);

        Console.WriteLine("Atendimento iniciado com sucesso!");
    }

    static void FinalizarAtendimento()
    {
        Console.WriteLine("Informe o CPF do paciente:");
        string cpfPaciente = Console.ReadLine();
        Paciente paciente = pacientes.FirstOrDefault(p => p.CPF == cpfPaciente);
    
        if (paciente == null)
        {
            Console.WriteLine("Paciente não encontrado.");
            return;
        }
    
        Atendimento atendimentoEmAberto = atendimentos.FirstOrDefault(a => a.Paciente == paciente && !a.Fim.HasValue);
    
        if (atendimentoEmAberto == null)
        {
            Console.WriteLine("Nenhum atendimento em aberto para este paciente.");
            return;
        }
    
        Console.WriteLine("Informe o diagnóstico final:");
        atendimentoEmAberto.DiagnosticoFinal = Console.ReadLine();
    
        Console.WriteLine("Informe a data de fechamento no formato ano/mes/dia: ");
        atendimentoEmAberto.Fim = DateTime.Parse(Console.ReadLine());
    
        Console.WriteLine("Atendimento finalizado com sucesso!");
    }

    static void ExibirRelatorios()
    {
        int opcao;

        do
        {
            Console.WriteLine("Escolha o relatório:");
            Console.WriteLine("1 - Médicos com idade entre dois valores.");
            Console.WriteLine("2 - Pacientes com idade entre dois valores.");
            Console.WriteLine("3 - Pacientes do sexo informado pelo usuário.");
            Console.WriteLine("4 - Pacientes em ordem alfabética.");
            Console.WriteLine("5 - Pacientes cujos sintomas contenham texto informado pelo usuário.");
            Console.WriteLine("6 - Médicos e Pacientes aniversariantes do mês informado.");
            Console.WriteLine("7 - Atendimentos em aberto (sem finalizar) em ordem decrescente pela data de início.");
            Console.WriteLine("8 - Médicos em ordem decrescente da quantidade de atendimentos concluídos.");
            Console.WriteLine("9 - Atendimentos cuja suspeita ou diagnóstico final contenham determinada palavra.");
            Console.WriteLine("10 - Os 10 exames mais utilizados nos atendimentos.");
            Console.WriteLine("11 - Voltar para o menu anterior.");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                RelatorioMedicosPorIdade();
            }
            else if (opcao == 2)
            {
                RelatorioPacientesPorIdade();
            }
            else if (opcao == 3)
            {
                RelatorioPacientesPorSexo();
            }
            else if (opcao == 4)
            {
                RelatorioPacientesOrdemAlfabetica();
            }
            else if (opcao == 5)
            {
                RelatorioPacientesPorSintomas();
            }
            else if (opcao == 6)
            {
                RelatorioAniversariantesDoMes();
            }
            else if (opcao == 7)
            {
                RelatorioAtendimentosEmAberto();
            }
            else if (opcao == 8)
            {
                RelatorioMedicosPorAtendimentosConcluidos();
            }
            else if (opcao == 9)
            {
                RelatorioAtendimentosPorPalavraChave();
            }
            else if (opcao == 10)
            {
                RelatorioExamesMaisUtilizados();
            }
            else if (opcao == 11)
            {
                Console.WriteLine("Menu principal:");
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
            }

        } while (opcao != 11);
    }

    static void RelatorioMedicosPorIdade()
    {
        Console.WriteLine("Informe a idade mínima: ");
        int idadeMinima = int.Parse(Console.ReadLine());

        Console.WriteLine("Informe a idade máxima: ");
        int idadeMaxima = int.Parse(Console.ReadLine());

        var medicosFiltrados = medicos.Where(m => (DateTime.Now.Year - m.DataDeNascimento.Year) >= idadeMinima && (DateTime.Now.Year - m.DataDeNascimento.Year) <= idadeMaxima);

        Console.WriteLine("Médicos com idade entre " + idadeMinima + " e " + idadeMaxima + " anos:");
        foreach (var medico in medicosFiltrados)
        {
            Console.WriteLine($"Nome: {medico.Nome}, Idade: {DateTime.Now.Year - medico.DataDeNascimento.Year} anos");
        }
    }

    static void RelatorioPacientesPorIdade()
    {
        Console.WriteLine("Informe a idade mínima: ");
        int idadeMinima = int.Parse(Console.ReadLine());

        Console.WriteLine("Informe a idade máxima: ");
        int idadeMaxima = int.Parse(Console.ReadLine());

        var pacientesFiltrados = pacientes.Where(p => (DateTime.Now.Year - p.DataDeNascimento.Year) >= idadeMinima && (DateTime.Now.Year - p.DataDeNascimento.Year) <= idadeMaxima);

        Console.WriteLine("Pacientes com idade entre " + idadeMinima + " e " + idadeMaxima + " anos:");
        foreach (var paciente in pacientesFiltrados)
        {
            Console.WriteLine($"Nome: {paciente.Nome}, Idade: {DateTime.Now.Year - paciente.DataDeNascimento.Year} anos");
        }
    }

    static void RelatorioPacientesPorSexo()
    {
        Console.WriteLine("Informe o sexo (M ou F): ");
        string sexo = Console.ReadLine().ToUpper();

        var pacientesFiltrados = pacientes.Where(p => p.Sexo.ToUpper() == sexo);

        Console.WriteLine("Pacientes do sexo " + sexo + ":");
        foreach (var paciente in pacientesFiltrados)
        {
            Console.WriteLine($"Nome: {paciente.Nome}, Sexo: {paciente.Sexo}");
        }
    }

    static void RelatorioPacientesOrdemAlfabetica()
    {
        var pacientesOrdenados = pacientes.OrderBy(p => p.Nome);

        Console.WriteLine("Pacientes em ordem alfabética:");
        foreach (var paciente in pacientesOrdenados)
        {
            Console.WriteLine($"Nome: {paciente.Nome}");
        }
    }

    static void RelatorioPacientesPorSintomas()
    {
        Console.WriteLine("Informe o texto dos sintomas: ");
        string textoSintomas = Console.ReadLine();

        var pacientesFiltrados = pacientes.Where(p => p.Sintomas.Contains(textoSintomas));

        Console.WriteLine($"Pacientes cujos sintomas contenham \"{textoSintomas}\":");
        foreach (var paciente in pacientesFiltrados)
        {
            Console.WriteLine($"Nome: {paciente.Nome}, Sintomas: {paciente.Sintomas}");
        }
    }

    static void RelatorioAniversariantesDoMes()
    {
        Console.WriteLine("Informe o mês (1 a 12): ");
        int mes = int.Parse(Console.ReadLine());

        var aniversariantes = medicos.Select(m => (Pessoa)m).Where(p => p.DataDeNascimento.Month == mes).Concat(pacientes.Select(p => (Pessoa)p).Where(p => p.DataDeNascimento.Month == mes));

        Console.WriteLine($"Aniversariantes do mês {mes}:");
        foreach (var pessoa in aniversariantes)
        {
            Console.WriteLine($"Nome: {pessoa.Nome}, Data de Nascimento: {pessoa.DataDeNascimento.ToShortDateString()}");
        }
    }

    static void RelatorioAtendimentosEmAberto()
    {
        var atendimentosEmAberto = atendimentos.Where(a => !a.Fim.HasValue).OrderByDescending(a => a.Inicio);

        Console.WriteLine("Atendimentos em aberto em ordem decrescente pela data de início:");
        foreach (var atendimento in atendimentosEmAberto)
        {
            Console.WriteLine($"Paciente: {atendimento.Paciente.Nome}, Médico: {atendimento.MedicoResponsavel.Nome}, Início: {atendimento.Inicio}");
        }
    }

    static void RelatorioMedicosPorAtendimentosConcluidos()
    {
        var medicosOrdenados = medicos.OrderByDescending(m => atendimentos.Count(a => a.MedicoResponsavel == m && a.Fim.HasValue));

        Console.WriteLine("Médicos em ordem decrescente da quantidade de atendimentos concluídos:");
        foreach (var medico in medicosOrdenados)
        {
            int atendimentosConcluidos = atendimentos.Count(a => a.MedicoResponsavel == medico && a.Fim.HasValue);
            Console.WriteLine($"Médico: {medico.Nome}, Atendimentos Concluídos: {atendimentosConcluidos}");
        }
    }

    static void RelatorioAtendimentosPorPalavraChave()
    {
        Console.WriteLine("Informe a palavra chave:");
        string palavraChave = Console.ReadLine().ToLower();

        var atendimentosFiltrados = atendimentos.Where(a => a.SuspeitaInicial.ToLower().Contains(palavraChave) || a.DiagnosticoFinal.ToLower().Contains(palavraChave));

        Console.WriteLine($"Atendimentos cuja suspeita ou diagnóstico final contenham \"{palavraChave}\":");
        foreach (var atendimento in atendimentosFiltrados)
        {
            Console.WriteLine($"Paciente: {atendimento.Paciente.Nome}, Médico: {atendimento.MedicoResponsavel.Nome}, Início: {atendimento.Inicio}");
        }
    }

    static void RelatorioExamesMaisUtilizados()
    {
        var examesMaisUtilizados = examesUtilizados
            .GroupBy(e => e.Titulo)
            .Select(group => new { Titulo = group.Key, Quantidade = group.Count() })
            .OrderByDescending(e => e.Quantidade)
            .Take(10);

        Console.WriteLine("Os 10 exames mais utilizados nos atendimentos:");
        foreach (var exame in examesMaisUtilizados)
        {
            Console.WriteLine($"Exame: {exame.Titulo}, Quantidade de Utilizações: {exame.Quantidade}");
        }
    }
}
