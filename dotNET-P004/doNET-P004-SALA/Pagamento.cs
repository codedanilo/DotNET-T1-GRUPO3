using System;

public enum TipoPagamento
{
    CartaoCredito,
    BoletoBancario,
    Dinheiro
}

public class Pagamento
{
    public TipoPagamento Tipo { get; set; }
    public string Descricao { get; set; }
    public double ValorBruto { get; set; }
    public double Desconto { get; set; }
    public DateTime DataHora { get; set; }

    public double CalcularValorFinal()
    {
    
        return ValorBruto - Desconto;
    }

    public void RealizarPagamento()
    {
        switch (Tipo)
        {
            case TipoPagamento.CartaoCredito:
                Console.WriteLine($"Pagamento realizado com cartão de crédito no valor de {CalcularValorFinal()}.");
                break;
            case TipoPagamento.BoletoBancario:
                Console.WriteLine($"Emitindo boleto bancário para o pagamento no valor de {CalcularValorFinal()}.");
                break;
            case TipoPagamento.Dinheiro:
                Console.WriteLine($"Pagamento em dinheiro recebido no valor de {CalcularValorFinal()}.");
                break;
            default:
                Console.WriteLine("Tipo de pagamento inválido.");
                break;
        }
    }
}
