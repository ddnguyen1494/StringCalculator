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
        public const string DEFAULT_DELIMITER = "\n";

        public const int DEFAULT_UPPERBOUND = 1000;

        private const string CUSTOM_DELIM_PATTERN = @"^//((?<singleChar>.)?(?=\n)|(\[(?<singleCustomLength>.*?)\])+)\n";

        private List<string> _delimiters = new List<string> { "," };

        public int UpperBound { get; set; } = DEFAULT_UPPERBOUND;

        public InputParser()
        {
            _delimiters.Add(DEFAULT_DELIMITER);
        }

        public InputParser(string alternateDelimiter = DEFAULT_DELIMITER)
        {
            if (!string.IsNullOrEmpty(alternateDelimiter))
            {
                _delimiters.Add(alternateDelimiter);
            }
        }

        public List<int> Parse(string input)
        {
            ParseAndRemoveCustomDelimiters(ref input);

            string[] tokens = Tokenize(input);

            return ConvertTokensToNumbers(tokens);
        }

        private void ParseAndRemoveCustomDelimiters(ref string input)
        {
            Match match = Regex.Match(input, CUSTOM_DELIM_PATTERN, RegexOptions.Singleline);

            if (match.Success)
            {
                if (!string.IsNullOrWhiteSpace(match.Groups["singleChar"].Value))
                {
                    _delimiters.Add(match.Groups["singleChar"].Value);
                }
                else if (match.Groups["singleCustomLength"].Captures.Count > 0)
                {
                    foreach (var capture in match.Groups["singleCustomLength"].Captures)
                    {
                        if (!string.IsNullOrWhiteSpace(capture.ToString()))
                        {
                            _delimiters.Add(capture.ToString());
                        }
                        else
                        {
                            throw new ArgumentException("Missing custom delimiter");
                        }
                    }
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
                    if (number > UpperBound)
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
