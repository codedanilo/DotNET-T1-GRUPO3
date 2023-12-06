using System.ComponentModel.Design.Serialization;

namespace dotNET_P004;

public static class App
{
    public static void InicializarMockup()
    {
        foreach (var medico in Mock.CriarMedicosAleatorios())
        {
            Service.AddMedico(medico);  
        }

        foreach (var paciente in Mock.CriarPacientesAleatorios())
        {
            Service.AddPaciente(paciente);
        }
    }

    public static void Cadastrar(bool ehMedico)
    {
        string titulo = ehMedico ? "Medico" : "Paciente";

        while (true)
        {    
            // Console.Clear();
            Console.WriteLine($"============================== Cadastrar {titulo} ==============================\n");
            
            try
            {
                Console.Write("Nome: ");
                string nome = Console.ReadLine()!;

                Console.Write("Cpf: ");
                string cpf = Console.ReadLine()!;

                Console.Write("Sexo: ");
                string sexo = Console.ReadLine()!;

                Console.Write("Data de Nascimento (dd/MM/aaaa): ");
                string dtNascimento = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(dtNascimento))
                {
                    throw new System.Exception("A data de nascimento deve ser informada.");
                }

                if (ehMedico)
                {
                    Console.Write("CRM: ");
                    string crm = Console.ReadLine()!;

                    Medico medico = new Medico(nome, DateTime.Parse(dtNascimento), cpf, sexo, crm);
                    Service.AddMedico(medico);
                }
                else 
                {
                    Paciente paciente = new Paciente(nome, DateTime.Parse(dtNascimento), cpf, sexo);
                    Service.AddPaciente(paciente);
                }

                Console.WriteLine($"\n{titulo} cadastrado com sucesso.\n");
                break;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                continue;
            }
        }
    }

    public static void Listar(bool ehMedico)
    {
        string titulo =  ehMedico ? "Medicos" : "Pacientes";
        Console.WriteLine($"============================== Listar {titulo} ==============================\n");

        try
        {
            if ((!ehMedico && Service.listaPacientes.Count == 0) 
                || (ehMedico && Service.listaMedicos.Count == 0))
            {
                throw new System.Exception("Não existem dados cadastrados para serem exibidos");
            }

            Console.Write("Nome \t\t Data Nasc. \t CPF \t\t Sexo ");
            if (ehMedico)
            {
                Console.WriteLine("\t\t CRM");
                Service.ListarMedicos();
            }
            else
            {
                Console.WriteLine("");
                Service.ListarPacientes();
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static void Remover(bool ehMedico)
    {
        List<string> listaItens = new List<string>();
        string titulo =  ehMedico ? "Medico" : "Paciente";
        int selecao, opcao;

        try
        {
            if ((!ehMedico && Service.listaPacientes.Count == 0) 
                || (ehMedico && Service.listaMedicos.Count == 0))
            {
                throw new System.Exception("Não existem dados cadastrados para serem exibidos");
            }

            listaItens = ehMedico ? retornaListaItensMenu(Service.listaMedicos) 
                : listaItens = retornaListaItensMenu(Service.listaPacientes);

            string cabecalho = "      Nome \t\t Data Nasc. \t CPF \t\t Sexo ";
            cabecalho += ehMedico ? "\t\t CRM" : "";

            while (true)
            {
                CtrlMenu.MontaMenu(listaItens, "Remover " + titulo, cabecalho);
                selecao = CtrlMenu.ObterOpcao(listaItens.Count);

                if (selecao != 0)
                {
                    string nome = ehMedico ? Service.listaMedicos[selecao - 1].Nome : Service.listaPacientes[selecao - 1].Nome;
                    Console.WriteLine($"\nDeseja realmente remover o {titulo} {nome}?");
                    CtrlMenu.MontaMenu(new List<string> { "[ 1 ] Sim", "[ 2 ] Não" }, "");
                    opcao = CtrlMenu.ObterOpcao(2);
                }
                else
                {
                    break;
                }

                if (opcao == 1)
                {
                    if (ehMedico)
                    {
                        Service.listaMedicos.RemoveAt(selecao - 1);
                        listaItens = retornaListaItensMenu(Service.listaMedicos);

                    }
                    else
                    {
                        Service.listaPacientes.RemoveAt(selecao - 1);
                        listaItens = retornaListaItensMenu(Service.listaPacientes);
                    }
                        
                    Console.WriteLine($"\n{titulo} removido com sucesso.\n");
                }
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static List<string> retornaListaItensMenu<T>(List<T> listaObjetos)
    {
        List<string> listaItens = new List<string>();

        for (int i = 0; i < listaObjetos.Count; i++)
        {
            listaItens.Add("[ " + (i + 1) + " ] " + listaObjetos[i]!.ToString());
        }

        listaItens.Add("\n[ 0 ] Sair");

        return listaItens;
    }
}