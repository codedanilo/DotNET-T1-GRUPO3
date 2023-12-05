using System.ComponentModel.Design.Serialization;

namespace dotNET_P004;

public static class App
{
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
            if (Service.listaMedicos.Count > 0 || Service.listaPacientes.Count > 0)
            {
                Console.Write("Nome \t Data Nascimento \t CPF \t Sexo ");

                if (ehMedico && Service.listaMedicos.Count > 0)
                {
                    Console.WriteLine("\t CRM");
                    Service.ListarMedicos();
                }
                else if (!ehMedico && Service.listaPacientes.Count > 0)
                {
                    Console.WriteLine("");
                    Service.ListarPacientes();
                }
                else
                {
                    throw new System.Exception("Não existem dados cadastrados para serem exibidos");
                }
            }
            else
            {
                throw new System.Exception("Não existem dados cadastrados para serem exibidos");
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static void Remover(bool ehMedico)
    {
        string titulo =  ehMedico ? "Medico" : "Paciente";
        Console.WriteLine($"============================== Remover {titulo} ==============================\n");

        try
        {
            if (Service.listaMedicos.Count > 0 || Service.listaPacientes.Count > 0)
            {
                Console.Write("Nome \t Data Nascimento \t CPF \t Sexo ");

                if (ehMedico && Service.listaMedicos.Count > 0)
                {
                    Console.WriteLine("\t CRM");
                    for (int i = 0; i < Service.listaMedicos.Count; i++)
                    {
                        Console.WriteLine("[ " + (i + 1) + " ] " + Service.listaMedicos[i].ToString() + "\n");
                    }
                }
                else if (!ehMedico && Service.listaPacientes.Count > 0)
                {
                    Console.WriteLine("");
                    for (int i = 0; i < Service.listaPacientes.Count; i++)
                    {
                        Console.WriteLine("[ " + (i + 1) + " ] " + Service.listaPacientes[i].ToString() + "\n");
                    }
                }
                else
                {
                    throw new System.Exception("Não existem dados cadastrados para serem exibidos");
                }
            }
            else
            {
                throw new System.Exception("Não existem dados cadastrados para serem exibidos");
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}