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
            var calculator = CreateCalculator(args);

            do
            {
                Console.Write("Enter string: ");
                string input = Console.ReadLine();
                try
                {
                    CalculatorOutput output = calculator.Calculate(input);

                    Console.WriteLine(CreateFormulaForDisplay(output));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (true);
        }

        static Calculator CreateCalculator(string[] args)
        {
            string altDelim = null;
            bool? allowNegative = null;
            int? upperBound = null;

            ProcessArguments(args, ref altDelim, ref allowNegative, ref upperBound);

            var inputParser = new InputParser(altDelim);

            if (upperBound.HasValue)
                inputParser.UpperBound = upperBound.Value;

            var calculator = new Calculator(inputParser);

            if (allowNegative.HasValue)
                calculator.AllowNegativeNumbers = allowNegative.Value;

            return calculator;
        }

        static void ProcessArguments(string[] args, ref string altDelim, ref bool? allowNegative, ref int? upperBound)
        {
            foreach (string arg in args)
            {
                if (arg.Contains("-d"))
                    altDelim = arg.Substring(2);
                else if (arg.Contains("-n"))
                    allowNegative = true;
                else if (arg.Contains("-u"))
                {
                    int tempUpperBound;
                    if (int.TryParse(arg.Substring(2), out tempUpperBound))
                    {
                        upperBound = tempUpperBound;
                    }
                }
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
