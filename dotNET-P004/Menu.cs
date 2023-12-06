namespace dotNET_P004;

public static class Menu
{
    public static void MenuPrincipal()
    {
        List<string> listaItens = new List<string> { "[ 1 ] Pacientes", "[ 2 ] Medicos", "[ 3 ] Atendimentos", "[ 0 ] Sair" };
        int opcao;
        
        while (true)
        {
            //Console.Clear();
            CtrlMenu.MontaMenu(listaItens, "Sistema de Gerenciamento de Med Tech");
            opcao = CtrlMenu.ObterOpcao(listaItens.Count);

            switch (opcao)
            {
                case 1:
                    MenuMedicosPacientes(false);
                    break;
                case 2:
                    MenuMedicosPacientes(true);
                    break;
                case 3:
                    Console.WriteLine("Em construção...");
                    break;
                case 0:
                    Console.WriteLine("\nFinalizando programa...");
                    Environment.Exit(0);
                    break;
            }
        }
    }

    public static void MenuMedicosPacientes(bool ehMedico)
    {
        List<string> listaItens = new List<string> { "[ 1 ] Cadastrar", "[ 2 ] Listar", "[ 3 ] Remover", "[ 0 ] Menu Principal" };
        string titulo = ehMedico ? "Medicos" : "Pacientes";
        int opcao;
        
        while (true)
        {
            // Console.Clear();
            CtrlMenu.MontaMenu(listaItens, titulo);
            opcao = CtrlMenu.ObterOpcao(listaItens.Count);

            switch (opcao)
            {
                case 1:
                    App.Cadastrar(ehMedico);
                    break;
                case 2:
                    App.Listar(ehMedico);
                    break;
                case 3:
                    App.Remover(ehMedico);
                    break;
                case 0:
                    MenuPrincipal();
                    break;
            }
        }
    }
}