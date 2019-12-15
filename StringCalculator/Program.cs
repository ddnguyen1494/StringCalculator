using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter string: ");
            string input = Console.ReadLine();

            IInputParser parser = new InputParser();
            var calculator = new Calculator(parser);

            try
            {
                CalculatorOutput output = calculator.Calculate(input);

                Console.WriteLine(CreateFormulaForDisplay(output));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        static string CreateFormulaForDisplay(CalculatorOutput output)
        {
            StringBuilder builder = new StringBuilder();

            List<int> operands = output.Operands;

            for (int i = 0; i < operands.Count; i++)
            {
                builder.Append(operands[i]);

                if (i != operands.Count - 1)
                    builder.Append("+");
            }

            builder.Append($"={output.Result}");

            return builder.ToString();
        }
    }
}
