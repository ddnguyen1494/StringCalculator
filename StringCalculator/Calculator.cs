using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class Calculator
    {
        public Calculator()
        {
                
        }

        public int Calculate(string input)
        {
            List<int> operands = Parse(input);

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

            if (tokens.Length > 2)
            {
                throw new ArgumentException("Invalid number of operands. Maximum: 2");
            }

            return ConvertTokensToNumbers(tokens);
        }

        private string[] Tokenize(string input)
        {
            return input.Split(',');
        }

        private List<int> ConvertTokensToNumbers(string[] tokens)
        {
            List<int> numbers = new List<int>();

            foreach(string token in tokens)
            {
                int number;
                if (int.TryParse(token, out number))
                {
                    numbers.Add(number);
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
