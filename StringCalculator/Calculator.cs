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
        public Calculator(IInputParser parser)
        {
            _parser = parser;
        }

        public int Calculate(string input)
        {
            List<int> operands = _parser.Parse(input);

            ThrowExceptionIfContainsNegativeNumber(operands);

            return Add(operands);
        }

        private int Add(List<int> operands)
        {
            int sum = 0;

            foreach(int operand in operands)
            {
                sum += operand;
            }

            return sum;
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
