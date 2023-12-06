namespace dotNET_P004;

public static class CtrlMenu
{
    public static void MontaMenu(List<string> listaItens, string titulo, string subtitulo = "")
    {
        if (titulo != "")
        {
            Console.WriteLine($"============================== {titulo} ==============================\n");
        }

        if (subtitulo != "")
        {
            Console.WriteLine(subtitulo);
        }

        foreach (var item in listaItens)
        {
            Console.WriteLine(item);
        }
    }

    public static int ObterOpcao(int qtdeOpcoes)
    {
        int opcao = -1;

        while ((opcao < 0) || (opcao >= qtdeOpcoes))
        {
            try
            {
                Console.Write("\nSelecione uma opção: ");
                opcao = int.Parse(Console.ReadLine()!); 
                
                if (opcao < 0 || opcao >= qtdeOpcoes)
                {
                    throw new System.Exception("Opção inválida.");
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                continue;
            }
        }

        return opcao;
    }
}