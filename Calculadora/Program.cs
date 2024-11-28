using Calculadora;
using System.Globalization;

Calculo calculo = new(0, 0, null!);

while (true)
{
    if (calculo.PrimeiraOperacao == true)
    {
        ExecutarPrimeiraOperacao(calculo);
    }
    while (calculo.PrimeiraOperacao == false)
    {
        SolicitarOperacao(calculo);
        if (calculo.Operacao!.ToUpper().Equals("C"))
        {
            calculo.ZerarResultado();
            break;
        }
        SolicitarNumero(calculo);
        DemonstrarResultados(calculo);
    }
}

static void ExecutarPrimeiraOperacao(Calculo calculo)
{
    Console.WriteLine("Executando Primeira Operação");
    SolicitarNumero(calculo);
    calculo.PrimeiraOperacao = false;
}
static void SolicitarOperacao(Calculo calculo)
{
    Console.WriteLine("Escreva a operação a ser feita ( + ; - ; * ; /; %, C (Zerar) )");
    calculo.Operacao = Console.ReadLine()!;
}
static void SolicitarNumero(Calculo calculo)
{
    Console.WriteLine("Escreva um número");
    calculo.NovoNumero = double.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);
}
static void DemonstrarResultados(Calculo calculo)
{
    calculo.Resultado();
    Console.WriteLine($"Total: {calculo.Total}");
}

