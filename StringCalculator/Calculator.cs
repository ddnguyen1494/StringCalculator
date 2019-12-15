using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class Calculator
    {
        private string[] _delimiters = { ",", "\n" };

        public Calculator()
        {
                
        }

        public int Calculate(string input)
        {
            List<int> operands = Parse(input);

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

        private List<int> Parse(string input)
        {
            string[] tokens = Tokenize(input);

            return ConvertTokensToNumbers(tokens);
        }

        private void ThrowExceptionIfContainsNegativeNumber(List<int> numbers)
        {
            var negativeNumbers = numbers.Where(number => number < 0);

            if (negativeNumbers.Any())
            {
                StringBuilder builder = new StringBuilder("Invalid negative numbers:");

                foreach(int number in negativeNumbers)
                {
                    builder.Append(" " + number);
                }

                throw new ArgumentException(builder.ToString());
            }
        }

        private string[] Tokenize(string input)
        {
            return input.Split(_delimiters, StringSplitOptions.None);
        }

        private List<int> ConvertTokensToNumbers(string[] tokens)
        {
            List<int> numbers = new List<int>();

            foreach(string token in tokens)
            {
                int number;
                if (int.TryParse(token, out number))
                {
                    if (number > 1000)
                    {
                        numbers.Add(0);
                    }
                    else
                    {
                        numbers.Add(number);
                    }
                }
                else
                {
                    numbers.Add(0);
                }
            }

            return numbers;
        }
    }
}
