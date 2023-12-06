namespace SistemaMedico
{
    public class ListaDePlanosDeSaude
    {
        private List<PlanoDeSaude> planosDeSaude;

        public ListaDePlanosDeSaude()
        {
            planosDeSaude = new List<PlanoDeSaude>();
        }

        public void AdicionarPlanoDeSaude(PlanoDeSaude plano)
        {
            planosDeSaude.Add(plano);
        }

        public void ExibirPlanosDeSaude()
        {
            Console.WriteLine("---- Lista de Planos de Saúde ----");
            foreach (var plano in planosDeSaude)
           
            {
                Console.WriteLine(plano.ToString());
                Console.WriteLine("-----------------------------");
           
            }
        }

    }
}
