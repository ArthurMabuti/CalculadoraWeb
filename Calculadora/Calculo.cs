namespace Calculadora;
internal class Calculo(double total, double novoNumero, string operacao)
{
    public double Total { get; set; } = total;
    public double NovoNumero
    {
        get => novoNumero;
        set
        {
            if (PrimeiraOperacao == true && Total == 0)
            {
                Total = value;
            }
            else
            {
                novoNumero = value;
            }
        }
    }
    public string? Operacao { get; set; } = operacao;
    public bool PrimeiraOperacao { get; set; } = true;

    public void Resultado()
    {
        var operacaoFunc = ObterFuncaoDeCalculo(Operacao!);

        Total = operacaoFunc(Total, NovoNumero);
    }

    private static Func<double, double, double> ObterFuncaoDeCalculo(string operacao)
    {
        return operacao switch
        {
            "+" => (total, valor) => total + valor,
            "-" => (total, valor) => total - valor,
            "*" => (total, valor) => total * valor,
            "/" => (total, valor) => valor != 0 ? total / valor : throw new DivideByZeroException("Divisão por zero não permitida"),
            _ => throw new ArgumentException("Operação inválida")
        };
    }

    public void ZerarResultado()
    {
        Total = 0;
        PrimeiraOperacao = true;
        Console.WriteLine("Resultados Zerados");
    }
}
