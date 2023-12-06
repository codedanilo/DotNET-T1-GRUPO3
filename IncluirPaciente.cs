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

        Console.WriteLine("Plano de saúde do paciente:");
        foreach (var plano in planosDeSaude)
        {
            Console.WriteLine($"{planosDeSaude.IndexOf(plano) + 1} - {plano.Titulo}");
        }

        int indicePlano = int.Parse(Console.ReadLine()) - 1;
        if (indicePlano >= 0 && indicePlano < planosDeSaude.Count)
        {
            PlanoSaude planoSelecionado = planosDeSaude[indicePlano];

            Paciente paciente = new Paciente(_nome, _dataDeNascimento, _cpf, _sexo, _sintomas, planoSelecionado);
            pacientes.Add(paciente);

            Console.WriteLine("Paciente cadastrado com sucesso!\n");
        }
        else
        {
            Console.WriteLine("Opção de plano de saúde inválida.");
        }

        Console.WriteLine("Paciente cadastrado com sucesso!!\n");
    }
