namespace CalculadoraWeb;
using System;
using System.Collections.Generic;

public class Calculator
{
    public double Calculate(string expression)
    {
        var tokens = TokenSeparation(expression);
        var rpn = ConvertToRPN(tokens);
        return CalculateRPN(rpn);
    }

    private List<string> TokenSeparation(string expression)
    {
        List<string> tokens = [];
        string number = string.Empty;
        int count = 0;

        foreach (char ch in expression)
        {
            //Se for um valor numérico ou ponto, acumula em "number"
            if (char.IsDigit(ch) || ch == ',' || ch == '-' && count == 0)
            {
                number += ch;
            }
            else if ("+-*/".Contains(ch) && count != 0)
            {
                // Adiciona o valor númerico completo como token
                if (!string.IsNullOrEmpty(number))
                {
                    tokens.Add(number);
                    number = string.Empty;
                }
                // Adiciona o operador
                tokens.Add(ch.ToString()); 
            }
            count++;
        }

        // Após fim do laço foreach, se ainda sobrar algum número, adicionar aos tokens
        if (!string.IsNullOrEmpty(number))
            tokens.Add(number);

        return tokens;
    }

    // RPN = Reverse Polish Notation (algoritmo de Shunting Yard de Dijkstra)
    private List<string> ConvertToRPN(List<string> tokens)
    {
        List<string> output = [];
        Stack<string> operators = [];
        Dictionary<string, int> priority = new() { { "+", 1 }, { "-", 1 }, { "*", 2 }, { "/", 2 } }; // Define qual operação será realizada primeiro

        foreach (var token in tokens)
        {
            if (double.TryParse(token, out _))
            {
                output.Add(token); // Adiciona números diretamente à saída
            }
            else
            {
                while (operators.Count > 0 && priority[operators.Peek()] >= priority[token])
                {
                    output.Add(operators.Pop()); // Remove operador do topo da pilha e adiciona a saída
                }
                operators.Push(token); // Adiciona operador ao topo da pilha
            }
        }

        while (operators.Count > 0)
        {
            output.Add(operators.Pop()); // Adiciona o restante dos operadores à saída
        }

        return output;
    }

    private double CalculateRPN(List<string> rpn)
    {
        Stack<double> stack = new();

        foreach (var token in rpn)
        {
            if (double.TryParse(token, out double number))
            {
                stack.Push(number);
            }
            else
            {
                var b = stack.Pop();
                var a = stack.Pop();

                stack.Push(token switch // Devolve resultado ao topo da pilha
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => a / b,
                    _ => throw new InvalidOperationException("Operador inválido")
                });
            }
        }

        return stack.Pop();
    }
}
