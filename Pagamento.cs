// Implementação da classe Pagamento
class Pagamento
{
    public enum TipoPagamento
    {
        CartaoCredito,
        BoletoBancario,
        Dinheiro
    }

    public TipoPagamento Tipo { get; set; }
    public string Descricao { get; set; }
    public double ValorBruto { get; set; }
    public double Desconto { get; set; }
    public DateTime DataHora { get; set; }

    public Pagamento(TipoPagamento tipo, string descricao, double valorBruto, double desconto, DateTime dataHora)
    {
        Tipo = tipo;
        Descricao = descricao;
        ValorBruto = valorBruto;
        Desconto = desconto;
        DataHora = dataHora;
    }
}
