namespace dotNET_P004;

public static class CtrlMenu
{
    public static void MontaMenu(List<string> listaItens, string titulo)
    {
        Console.WriteLine($"============================== {titulo} ==============================\n");
        foreach (var item in listaItens)
        {
            Console.WriteLine(item);
        }
    }

    public static int ObterOpcao(int qtdeOpcoes)
    {
        int opcao = -1;

        while ((opcao < 1) || (opcao > qtdeOpcoes))
        {
            try
            {
                Console.Write("\nSelecione uma opção: ");
                opcao = int.Parse(Console.ReadLine()!); 
                
                if (opcao < 1 || opcao > qtdeOpcoes)
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