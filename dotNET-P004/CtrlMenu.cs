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
            Console.Write("\nSelecione uma opção: ");
            opcao = int.Parse(Console.ReadLine()!); 
            
            if (opcao < 1 || opcao > qtdeOpcoes)
            {
                Console.WriteLine("Opção inválida.");
            }
        }

        return opcao;
    }
}