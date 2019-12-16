using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class Calculator
    {
        private IInputParser _parser;

        public bool AllowNegativeNumbers { get; set; } = false;

        public Calculator(IInputParser parser)
        {
            _parser = parser;
        }

        public CalculatorOutput Calculate(string input, string oper)
        {
            List<int> operands = _parser.Parse(input);

            if (!AllowNegativeNumbers)
                ThrowExceptionIfContainsNegativeNumber(operands);

            var result = PerformMathOperation(operands, oper);

            return new CalculatorOutput(operands, result , oper);
        }

        private double PerformMathOperation(List<int> operands, string oper)
        {
            if (oper == "+")
            {
                return Add(operands);
            }
            else if (oper == "-")
            {
                return Subtract(operands);
            }
            else if (oper == "*")
            {
                return Multiply(operands);
            }
            else if (oper == "/")
            {
                return Divide(operands);
            }
            else
            {
                throw new ArgumentException($"Unknown operator: {oper}");
            }
        }

        private int Add(List<int> operands)
        {
            int sum = 0;

            foreach (int operand in operands)
            {
                sum += operand;
            }

            return sum;
        }

        private double Subtract(List<int> operands)
        {
            double result = operands[0];

            for (int i = 1; i < operands.Count; i++)
            {
                result -= operands[i];
            }

            return result;
        }

        private double Multiply(List<int> operands)
        {
            double result = 1;

            foreach (int operand in operands)
            {
                result *= operand;
            }

            return result;
        }

        private double Divide(List<int> operands)
        {
            double result = operands[0];

            for (int i = 1; i < operands.Count; i++)
            {
                if(operands[i] == 0)
                {
                    throw new DivideByZeroException();
                }
                result /= operands[i];
            }

            return result;
        }

        private void ThrowExceptionIfContainsNegativeNumber(List<int> numbers)
        {
            var negativeNumbers = numbers.Where(number => number < 0);

            if (negativeNumbers.Any())
            {
                StringBuilder builder = new StringBuilder("Invalid negative numbers:");

                foreach (int number in negativeNumbers)
                {
                    builder.Append(" " + number);
                }

                throw new ArgumentException(builder.ToString());
            }
        }
    }
}
