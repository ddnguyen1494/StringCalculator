﻿using System;
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
        private string customDelimPattern = @"^//((?<singleChar>.)?(?=\n)|(\[(?<singleCustomLength>.*?)\])+)\n";

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
                System.Diagnostics.Debug.WriteLine("Match: '{0}'", match.Value);
                for (int ctr = 0; ctr < match.Groups.Count; ctr++)
                {
                    System.Diagnostics.Debug.WriteLine("   Group {0}: '{1}'", ctr, match.Groups[ctr].Value);
                    int capCtr = 0;
                    foreach (Capture capture in match.Groups[ctr].Captures)
                    {
                        System.Diagnostics.Debug.WriteLine("      Capture {0}: '{1}'", capCtr, capture.Value);
                        capCtr++;
                    }
                }

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
