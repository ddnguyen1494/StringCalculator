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
        private string customDelimPattern = @"^//((?<singleChar>.)?(?=\n)|\[(?<singleCustomLength>.+)?\])\n";

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
            Match match = Regex.Match(input, customDelimPattern, RegexOptions.Singleline);
            if (match.Success)
            {
                if (!string.IsNullOrWhiteSpace(match.Groups["singleChar"].Value))
                {
                    _delimiters.Add(match.Groups["singleChar"].Value);
                }
                else if (!string.IsNullOrWhiteSpace(match.Groups["singleCustomLength"].Value))
                {
                    _delimiters.Add(match.Groups["singleCustomLength"].Value);
                }
                else
                {
                    throw new ArgumentException("Missing custom delimiter");
                }
                input = input.Replace(match.Value, "");
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
