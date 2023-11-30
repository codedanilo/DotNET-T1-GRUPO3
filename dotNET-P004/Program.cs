using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaMedico;

public class Pessoa
{
    public string? Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string? CPF { get; set; }
}

public class Medico : Pessoa
{
    public string? CRM { get; set; }
}

public class Paciente : Pessoa
{
    public string? Sexo { get; set; }
    public string? Sintomas { get; set; }
}

public class Validacoes
{
    public static bool ValidarCPF(string cpf)
    {
    if (cpf.Length != 11 || !cpf.All(char.IsDigit))
    {
        return false;
    }

    int[] multiplicadoresPrimeiroDigito = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
    int[] multiplicadoresSegundoDigito = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

    string cpfSemDigitos = cpf.Substring(0, 9);

    int CalculaDigito(string cpfParcial, int[] multiplicadores)
    {
        int soma = 0;
        for (int i = 0; i < cpfParcial.Length; i++)
        {
            soma += int.Parse(cpfParcial[i].ToString()) * multiplicadores[i];
        }

        int resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }

    int primeiroDigito = CalculaDigito(cpfSemDigitos, multiplicadoresPrimeiroDigito);
    cpfSemDigitos += primeiroDigito;
    int segundoDigito = CalculaDigito(cpfSemDigitos, multiplicadoresSegundoDigito);

    return cpf.EndsWith($"{primeiroDigito}{segundoDigito}");
 }
}

public class Exame
{
    public string? Titulo { get; set; }
    public float Valor { get; set; }
    public string? Descricao { get; set; }
    public string? Local { get; set; }
}

public class Atendimento
{

    public int Id { get; set; } // Adição da propriedade Id
    public DateTime Inicio { get; set; }
    public string? SuspeitaInicial { get; set; }
    public List<(Exame, string)> ListaExamesResultado { get; set; } = new List<(Exame, string)>();
    public float Valor { get; set; }
    public DateTime Fim { get; set; }
    public Medico? MedicoResponsavel { get; set; }
    public Paciente? Paciente { get; set; }
    public string? DiagnosticoFinal { get; set; }
    
    public void IniciarAtendimento(string suspeitaInicial, DateTime inicio, Medico medico, Paciente paciente)
    {
        SuspeitaInicial = suspeitaInicial;
        Inicio = inicio;
        MedicoResponsavel = medico;
        Paciente = paciente;
    }

    public void FinalizarAtendimento(string diagnosticoFinal, DateTime fim)
    {
        DiagnosticoFinal = diagnosticoFinal;
        Fim = fim;
    }
}


class Program
{
    static void Main(string[] args)
    {
        List<Medico> medicos = new List<Medico>();
        List<Paciente> pacientes = new List<Paciente>();
        List<Atendimento> atendimentos = new List<Atendimento>();

        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("======== Menu Principal =========|");
            Console.WriteLine("|     1. Cadastrar Médico        |");
            Console.WriteLine("|     2. Cadastrar Paciente      |");
            Console.WriteLine("|     3. Iniciar Atendimento     |");
            Console.WriteLine("|     4. Finalizar Atendimento   |");
            Console.WriteLine("|     5. Relatórios              |");
            Console.WriteLine("|     6. Sair                    |");
            Console.WriteLine("|================================|");
            Console.Write("Escolha uma opção: ");

            if (int.TryParse(Console.ReadLine(), out int opcao))
            {
                switch (opcao)
                {
                    case 1:
                            CadastrarMedico(medicos);
                            break;
                        case 2:
                            CadastrarPaciente(pacientes);
                            break;
                        case 3:
                            IniciarAtendimento(atendimentos, medicos, pacientes);
                            break;
                        case 4:
                            FinalizarAtendimento(atendimentos);
                            break;
                        case 5:
                            MenuRelatorios(medicos, pacientes, atendimentos);
                            break;
                        case 6:
                            sair = true;
                            break;
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
            }

            Console.WriteLine();
        }
    }


public static bool ValidarCPF(string cpf)
    {
    if (cpf.Length != 11 || !cpf.All(char.IsDigit))
    {
        return false;
    }

    int[] multiplicadoresPrimeiroDigito = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
    int[] multiplicadoresSegundoDigito = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

    string cpfSemDigitos = cpf.Substring(0, 9);

    int CalculaDigito(string cpfParcial, int[] multiplicadores)
    {
        int soma = 0;
        for (int i = 0; i < cpfParcial.Length; i++)
        {
            soma += int.Parse(cpfParcial[i].ToString()) * multiplicadores[i];
        }

        int resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }

    int primeiroDigito = CalculaDigito(cpfSemDigitos, multiplicadoresPrimeiroDigito);
    cpfSemDigitos += primeiroDigito;
    int segundoDigito = CalculaDigito(cpfSemDigitos, multiplicadoresSegundoDigito);

    return cpf.EndsWith($"{primeiroDigito}{segundoDigito}");
 }

    static void CadastrarMedico(List<Medico> medicos)
    {
        Medico novoMedico = new Medico();

        Console.WriteLine("===== Cadastro de Médico =====");
        Console.Write("Nome: ");
        novoMedico.Nome = Console.ReadLine();

        Console.Write("Data de Nascimento (dd/mm/aaaa): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNascimento))
        {
            novoMedico.DataNascimento = dataNascimento;
        }
        else
        {
            Console.WriteLine("Data de nascimento inválida.");
            return;
        }

        string cpf = string.Empty;
        do
        {
           Console.Write("CPF: ");
           cpf = Console.ReadLine() ?? string.Empty; // Utiliza coalescência nula para evitar valor nulo

           if (cpf != null && !ValidarCPF(cpf)) // Verifica se o CPF não é nulo antes de validar
        {
            Console.WriteLine("CPF inválido. Digite um CPF válido.");
        }

        } while (cpf != null && !ValidarCPF(cpf)); // Verifica se o CPF não é nulo antes de validar

    if (!string.IsNullOrEmpty(cpf)) // Verifica se o CPF não é nulo ou vazio antes de atribuir
    {
            novoMedico.CPF = cpf;
        }

        Console.Write("CRM: ");
        novoMedico.CRM = Console.ReadLine();

        medicos.Add(novoMedico);
        Console.WriteLine("Médico cadastrado com sucesso!");
    }

    static void CadastrarPaciente(List<Paciente> pacientes)
    {
        Paciente novoPaciente = new Paciente();

        Console.WriteLine("===== Cadastro de Paciente =====");
        Console.Write("Nome: ");
        novoPaciente.Nome = Console.ReadLine();

        Console.Write("Data de Nascimento (dd/mm/aaaa): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNascimento))
        {
            novoPaciente.DataNascimento = dataNascimento;
        }
        else
        {
            Console.WriteLine("Data de nascimento inválida.");
            return;
        }

       string? cpf = string.Empty; // Alteração para permitir valor nulo
    do
    {
        Console.Write("CPF: ");
        cpf = Console.ReadLine();

        if (cpf != null && !ValidarCPF(cpf)) // Verificação para evitar valor nulo ao chamar ValidarCPF
        {
            Console.WriteLine("CPF inválido. Digite um CPF válido.");
        }

    } while (cpf != null && !ValidarCPF(cpf)); // Verificação para evitar valor nulo ao chamar ValidarCPF

    if (!string.IsNullOrEmpty(cpf)) // Verificação para evitar valor nulo ao atribuir novoPaciente.CPF
    {
        novoPaciente.CPF = cpf;
    }

        Console.Write("Sexo: ");
        novoPaciente.Sexo = Console.ReadLine();

        Console.Write("Sintomas: ");
        novoPaciente.Sintomas = Console.ReadLine();

        pacientes.Add(novoPaciente);
        Console.WriteLine("Paciente cadastrado com sucesso!");
    }

    

static void IniciarAtendimento(List<Atendimento> atendimentos, List<Medico> medicos, List<Paciente> pacientes)
{
    Console.WriteLine("===== Iniciar Atendimento =====");

    Console.Write("ID do Atendimento: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("ID inválido.");
        return;
    }

    
    Console.Write("Suspeita Inicial: ");
    string suspeita = Console.ReadLine() ?? string.Empty; // Coalescência nula para evitar valor nulo
    
    Console.Write("Data de Início (dd/mm/aaaa HH:mm:ss): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime inicio))
    {
        Console.WriteLine("Data de início inválida.");
        return;
    }
    
    Console.Write("Nome do Médico Responsável: ");
    string nomeMedico = Console.ReadLine() ?? string.Empty; // Coalescência nula para evitar valor nulo
    var medico = medicos.FirstOrDefault(m => m.Nome == nomeMedico);
    if (medico == null)
    {
        Console.WriteLine("Médico não encontrado.");
        return;
    }
    
    Console.Write("Nome do Paciente: ");
    string nomePaciente = Console.ReadLine() ?? string.Empty; // Coalescência nula para evitar valor nulo
    var paciente = pacientes.FirstOrDefault(p => p.Nome == nomePaciente);
    if (paciente == null)
    {
        Console.WriteLine("Paciente não encontrado.");
        return;
    }
    
    Atendimento novoAtendimento = new Atendimento();
    novoAtendimento.Id = id; // Atribuição do ID informado
    novoAtendimento.IniciarAtendimento(suspeita, inicio, medico, paciente);
    atendimentos.Add(novoAtendimento);
    
    Console.WriteLine("Atendimento iniciado com sucesso!");
}

static void FinalizarAtendimento(List<Atendimento> atendimentos)
{
    Console.WriteLine("===== Finalizar Atendimento =====");
    
    Console.Write("ID do Atendimento a finalizar: ");
    if (!int.TryParse(Console.ReadLine(), out int idAtendimento))
    {
        Console.WriteLine("ID inválido.");
        return;
    }
    
    var atendimento = atendimentos.FirstOrDefault(a => a.Id == idAtendimento);
    if (atendimento == null)
    {
        Console.WriteLine("Atendimento não encontrado.");
        return;
    }
    
    Console.Write("Diagnóstico Final: ");
    string diagnosticoFinal = Console.ReadLine() ?? string.Empty; // Coalescência nula para evitar valor nulo
    
    Console.Write("Data de Fim (dd/mm/aaaa HH:mm:ss): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime fim))
    {
        Console.WriteLine("Data de fim inválida.");
        return;
    }
    
    atendimento.FinalizarAtendimento(diagnosticoFinal, fim);
    
    Console.WriteLine("Atendimento finalizado com sucesso!");
}



// MENU DE RELATÓRIOS
    static void MenuRelatorios(List<Medico> medicos, List<Paciente> pacientes, List<Atendimento> atendimentos)
    {
        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("|==================================== Menu de Relatórios ================================== |");
            Console.WriteLine("|        1. Médicos com idade entre dois valores                                            |");
            Console.WriteLine("|        2. Pacientes com idade entre dois valores|                                         |");
            Console.WriteLine("|        3. Pacientes do sexo informado pelo usuário                                        |");
            Console.WriteLine("|        4. Pacientes em ordem alfabética                                                   |");
            Console.WriteLine("|        5. Pacientes cujos sintomas contenham texto informado pelo usuário                 |");
            Console.WriteLine("|        6. Médicos e Pacientes aniversariantes do mês informado                            |");
            Console.WriteLine("|        7. Atendimentos em aberto (sem finalizar) em ordem decrescente pela data de início.|");
            Console.WriteLine("|        8. Médicos em ordem decrescente da quantidade de atendimentos concluídos.          |");
            Console.WriteLine("|        9. Atendimentos cuja suspeita ou diagnóstico final contenham determinada palavra.  |");
            Console.WriteLine("|        10. Os 10 exames mais utilizados nos atendimentos.                                 |");
            Console.WriteLine("|         0. Voltar ao Menu Principal                                                       |");
            Console.WriteLine("============================================================================================|");
            Console.Write("Escolha um relatório (1-10 ou 0 para voltar ao Menu Principal): ");

             if (int.TryParse(Console.ReadLine(), out int opcaoRelatorio))
        {
            switch (opcaoRelatorio)
            {
                case 1:
                    RelatorioMedicosIdade(medicos);
                    break;
                case 2:
                    RelatorioPacientesIdade(pacientes);
                    break;
                case 3:
                    RelatorioPacientesPorSexo(pacientes);
                    break;
                case 4:
                    RelatorioPacientesOrdemAlfabetica(pacientes);
                    break;
                case 5:
                    RelatorioPacientesPorSintomas(pacientes);
                    break;
                case 6:
                    RelatorioAniversariantes(medicos, pacientes);
                    break;
                case 7:
                    AtendimentosEmAberto(atendimentos);
                    break;
                case 8:
                    MedicosOrdenadosPorQuantidadeAtendimentos(medicos, atendimentos);
                    break;
                case 9:
                    AtendimentosPorPalavraChave(atendimentos);
                    break;
                case 10:
                    Top10ExamesUtilizados(atendimentos);
                    break;
                case 0:
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Opção inválida. Tente novamente.");
        }

            Console.WriteLine();
        }
    }

                    // RELATÓRIOS
    static void RelatorioMedicosIdade(List<Medico> medicos)
    {
    Console.Write("Idade mínima: ");
    if (!int.TryParse(Console.ReadLine(), out int idadeMinima))
    {
        Console.WriteLine("Valor inválido para idade mínima.");
        return;
    }

    Console.Write("Idade máxima: ");
    if (!int.TryParse(Console.ReadLine(), out int idadeMaxima))
    {
        Console.WriteLine("Valor inválido para idade máxima.");
        return;
    }

    var medicosFiltrados = medicos.Where(m =>
    {
        int idade = DateTime.Now.Year - m.DataNascimento.Year;
        if (DateTime.Now.Month < m.DataNascimento.Month || (DateTime.Now.Month == m.DataNascimento.Month && DateTime.Now.Day < m.DataNascimento.Day))
        {
            idade--;
        }
        return idade >= idadeMinima && idade <= idadeMaxima;
    }).ToList();

    if (medicosFiltrados.Any())
    {
        Console.WriteLine("Médicos com idade entre os valores informados:");
        foreach (var medico in medicosFiltrados)
        {
            Console.WriteLine($"Nome: {medico.Nome}, Idade: {DateTime.Now.Year - medico.DataNascimento.Year}");
        }
    }
    else
    {
        Console.WriteLine("Nenhum médico encontrado com a faixa de idade especificada.");
    }
}

static void RelatorioPacientesIdade(List<Paciente> pacientes)
{
    Console.Write("Idade mínima: ");
    if (!int.TryParse(Console.ReadLine(), out int idadeMinima))
    {
        Console.WriteLine("Valor inválido para idade mínima.");
        return;
    }

    Console.Write("Idade máxima: ");
    if (!int.TryParse(Console.ReadLine(), out int idadeMaxima))
    {
        Console.WriteLine("Valor inválido para idade máxima.");
        return;
    }

    var pacientesFiltrados = pacientes.Where(p =>
    {
        int idade = DateTime.Now.Year - p.DataNascimento.Year;
        if (DateTime.Now.Month < p.DataNascimento.Month || (DateTime.Now.Month == p.DataNascimento.Month && DateTime.Now.Day < p.DataNascimento.Day))
        {
            idade--;
        }
        return idade >= idadeMinima && idade <= idadeMaxima;
    }).ToList();

    if (pacientesFiltrados.Any())
    {
        Console.WriteLine("Pacientes com idade entre os valores informados:");
        foreach (var paciente in pacientesFiltrados)
        {
            Console.WriteLine($"Nome: {paciente.Nome}, Idade: {DateTime.Now.Year - paciente.DataNascimento.Year}");
        }
    }
    else
    {
        Console.WriteLine("Nenhum paciente encontrado com a faixa de idade especificada.");
    }
}

static void RelatorioPacientesPorSexo(List<Paciente> pacientes)
{
    Console.Write("Informe o sexo (M/F): ");
    string sexo = Console.ReadLine()?.ToUpper() ?? string.Empty;

    var pacientesFiltrados = pacientes.Where(p => p.Sexo != null && p.Sexo.ToUpper() == sexo).ToList();

    if (pacientesFiltrados.Any())
    {
        Console.WriteLine($"Pacientes do sexo {sexo}:");
        foreach (var paciente in pacientesFiltrados)
        {
            Console.WriteLine($"Nome: {paciente.Nome}, Sexo: {paciente.Sexo}");
        }
    }
    else
    {
        Console.WriteLine($"Nenhum paciente encontrado do sexo {sexo}.");
    }
}


static void RelatorioPacientesOrdemAlfabetica(List<Paciente> pacientes)
{
    var pacientesOrdenados = pacientes.OrderBy(p => p.Nome).ToList();

    Console.WriteLine("Pacientes em ordem alfabética:");
    foreach (var paciente in pacientesOrdenados)
    {
        Console.WriteLine($"Nome: {paciente.Nome}");
    }
}

static void RelatorioPacientesPorSintomas(List<Paciente> pacientes)
{
    Console.Write("Digite os sintomas a serem pesquisados: ");
    string termoPesquisa = Console.ReadLine()?.ToUpper()?? string.Empty;

    var pacientesFiltrados = pacientes.Where(p => p.Sintomas?.ToUpper().Contains(termoPesquisa) ?? false).ToList();

    if (pacientesFiltrados.Any())
    {
        Console.WriteLine($"Pacientes com sintomas contendo '{termoPesquisa}':");
        foreach (var paciente in pacientesFiltrados)
        {
            Console.WriteLine($"Nome: {paciente.Nome}, Sintomas: {paciente.Sintomas}");
        }
    }
    else
    {
        Console.WriteLine($"Nenhum paciente encontrado com sintomas contendo '{termoPesquisa}'.");
    }
}

static void RelatorioAniversariantes(List<Medico> medicos, List<Paciente> pacientes)
{
    Console.Write("Informe o mês (1-12): ");
    if (!int.TryParse(Console.ReadLine(), out int mes))
    {
        Console.WriteLine("Valor inválido para o mês.");
        return;
    }

    var aniversariantes = medicos.Select(m => new Pessoa { Nome = m.Nome, DataNascimento = m.DataNascimento })
                                 .Where(p => p.DataNascimento.Month == mes)
                                 .Concat(pacientes.Where(p => p.DataNascimento.Month == mes))
                                 .ToList();

    if (aniversariantes.Any())
    {
        Console.WriteLine($"Aniversariantes do mês {mes}:");
        foreach (var pessoa in aniversariantes)
        {
            Console.WriteLine($"Nome: {pessoa.Nome}, Data de Nascimento: {pessoa.DataNascimento.ToShortDateString()}");
        }
    }
    else
    {
        Console.WriteLine($"Nenhum aniversariante encontrado no mês {mes}.");
    }
}

static void AtendimentosEmAberto(List<Atendimento> atendimentos)
{
    var atendimentosAbertos = atendimentos.Where(a => a.Fim == DateTime.MinValue).OrderByDescending(a => a.Inicio).ToList();
    
    if (atendimentosAbertos.Any())
    {
        Console.WriteLine("Atendimentos em aberto (sem finalizar) em ordem decrescente pela data de início:");
        foreach (var atendimento in atendimentosAbertos)
        {
            Console.WriteLine($"ID: {atendimento.Id}, Início: {atendimento.Inicio}, Paciente: {atendimento.Paciente?.Nome}");
        }
    }
    else
    {
        Console.WriteLine("Nenhum atendimento em aberto encontrado.");
    }
}

static void MedicosOrdenadosPorQuantidadeAtendimentos(List<Medico> medicos, List<Atendimento> atendimentos)
{
    var medicosComAtendimentos = medicos.Select(m => new
    {
        Medico = m,
        QuantidadeAtendimentos = atendimentos.Count(a => a.MedicoResponsavel == m && a.Fim != DateTime.MinValue)
    }).OrderByDescending(m => m.QuantidadeAtendimentos).ToList();
    
    if (medicosComAtendimentos.Any())
    {
        Console.WriteLine("Médicos em ordem decrescente da quantidade de atendimentos concluídos:");
        foreach (var medico in medicosComAtendimentos)
        {
            Console.WriteLine($"Nome: {medico.Medico.Nome}, Quantidade de Atendimentos: {medico.QuantidadeAtendimentos}");
        }
    }
    else
    {
        Console.WriteLine("Nenhum médico com atendimentos concluídos encontrado.");
    }
}

static void AtendimentosPorPalavraChave(List<Atendimento> atendimentos)
{
    Console.Write("Informe a palavra-chave: ");
    string palavraChave = Console.ReadLine()?.ToUpper() ?? string.Empty;

    var atendimentosFiltrados = atendimentos.Where(a =>
        (a.SuspeitaInicial != null && a.SuspeitaInicial.ToUpper().Contains(palavraChave)) ||
        (a.DiagnosticoFinal != null && a.DiagnosticoFinal.ToUpper().Contains(palavraChave)))
        .ToList();

    if (atendimentosFiltrados.Any())
    {
        Console.WriteLine($"Atendimentos cuja suspeita ou diagnóstico final contenham '{palavraChave}':");
        foreach (var atendimento in atendimentosFiltrados)
        {
            Console.WriteLine($"ID: {atendimento.Id}, Suspeita Inicial: {atendimento.SuspeitaInicial}, Diagnóstico Final: {atendimento.DiagnosticoFinal}");
        }
    }
    else
    {
        Console.WriteLine($"Nenhum atendimento encontrado com a palavra-chave '{palavraChave}'.");
    }
}


static void Top10ExamesUtilizados(List<Atendimento> atendimentos)
{
    var examesUtilizados = atendimentos.SelectMany(a => a.ListaExamesResultado.Select(e => e.Item1)).GroupBy(e => e.Titulo)
                                      .Select(g => new { Titulo = g.Key, Quantidade = g.Count() })
                                      .OrderByDescending(e => e.Quantidade).Take(10).ToList();

    if (examesUtilizados.Any())
    {
        Console.WriteLine("Os 10 exames mais utilizados nos atendimentos:");
        foreach (var exame in examesUtilizados)
        {
            Console.WriteLine($"Título: {exame.Titulo}, Quantidade de Utilização: {exame.Quantidade}");
        }
    }
    else
    {
        Console.WriteLine("Nenhum exame utilizado encontrado.");
    }
}

}


