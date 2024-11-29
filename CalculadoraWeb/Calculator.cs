namespace CalculadoraWeb;
using System;
using System.Collections.Generic;

public class Calculator
{
    public double Evaluate(string expression)
    {
        var tokens = Tokenize(expression);
        var rpn = ConvertToRPN(tokens);
        return CalculateRPN(rpn);
    }

    private List<string> Tokenize(string expression)
    {
        var tokens = new List<string>();
        string number = string.Empty;

        foreach (char ch in expression)
        {
            if (char.IsDigit(ch) || ch == '.')
            {
                number += ch; // Acumula o número
            }
            else if ("+-*/".Contains(ch))
            {
                if (!string.IsNullOrEmpty(number))
                {
                    tokens.Add(number);
                    number = string.Empty;
                }
                tokens.Add(ch.ToString()); // Adiciona operador
            }
        }

        if (!string.IsNullOrEmpty(number))
            tokens.Add(number);

        return tokens;
    }

    private List<string> ConvertToRPN(List<string> tokens)
    {
        var output = new List<string>();
        var operators = new Stack<string>();
        var precedence = new Dictionary<string, int> { { "+", 1 }, { "-", 1 }, { "*", 2 }, { "/", 2 } };

        foreach (var token in tokens)
        {
            if (double.TryParse(token, out _))
            {
                output.Add(token); // Adiciona números diretamente à saída
            }
            else
            {
                while (operators.Count > 0 && precedence[operators.Peek()] >= precedence[token])
                {
                    output.Add(operators.Pop());
                }
                operators.Push(token);
            }
        }

        while (operators.Count > 0)
        {
            output.Add(operators.Pop());
        }

        return output;
    }

    private double CalculateRPN(List<string> rpn)
    {
        var stack = new Stack<double>();

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

                stack.Push(token switch
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
