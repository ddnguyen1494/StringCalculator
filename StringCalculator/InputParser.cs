using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringCalculator
{
    class InputParser : IInputParser
    {
        private List<string> _delimiters = new List<string> { ",", "\n" };
        private string singleCharDelimPattern = @"//(.)\n";

        public InputParser()
        {

        }

        public List<int> Parse(string input)
        {
            ParseAndRemoveCustomDelimiters(ref input);

            string[] tokens = Tokenize(input);

            return ConvertTokensToNumbers(tokens);
        }

        private void ParseAndRemoveCustomDelimiters(ref string input)
        {
            Match m = Regex.Match(input, singleCharDelimPattern, RegexOptions.Singleline);
            if (m.Success)
            {
                string delimiter = m.Groups[1].Value;
                _delimiters.Add(delimiter);
                input = Regex.Replace(input, singleCharDelimPattern, "");
            }
        }


        private string[] Tokenize(string input)
        {
            return input.Split(_delimiters.ToArray(), StringSplitOptions.None);
        }

        private List<int> ConvertTokensToNumbers(string[] tokens)
        {
            List<int> numbers = new List<int>();

            foreach (string token in tokens)
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
