using StringCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculatorTests
{
    class MockInputParser : IInputParser
    {
        public List<int> Parse(string input)
        {
            string[] tokens = input.Split(',');

            List<int> numbers = new List<int>();
            foreach(string token in tokens)
            {
                numbers.Add(int.Parse(token));
            }

            return numbers;
        }
    }
}
