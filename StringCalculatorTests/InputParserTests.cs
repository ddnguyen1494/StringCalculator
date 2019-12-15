using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculator;

namespace StringCalculatorTests
{
    [TestClass]
    public class InputParserTests
    {
        [DataTestMethod]
        [DataRow("4,3", new[] { 4, 3 })]
        [DataRow("0,1", new[] { 0, 1 })]
        [DataRow("1000,1000", new[] { 1000, 1000 })]
        public void Parse_InputWithTwoNumbers_CorrectNumberList(string input, int[] expectedNumbers)
        {
            var parser = new InputParser();

            List<int> numbers = parser.Parse(input);

            CollectionAssert.AreEquivalent(expectedNumbers, numbers);
        }

        [DataTestMethod]
        [DataRow("1,2,5", new[] { 1, 2, 5})]
        [DataRow("1,2,abc", new[] { 1, 2, 0 })]
        [DataRow("a,b,c", new[] { 0, 0, 0 })]
        [DataRow("1,2,3,4,5,6,7,8,9,10,11,12", new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 })]
        public void Parse_InputWithMoreThanTwoNumbers_CorrectNumberList(string input, int[] expectedNumbers)
        {
            var parser = new InputParser();

            List<int> numbers= parser.Parse(input);

            CollectionAssert.AreEquivalent(expectedNumbers, numbers);
        }

        [DataTestMethod]
        [DataRow("5, tytyt", new[] { 5, 0 })]
        [DataRow(",", new[] { 0, 0 })]
        [DataRow("1,", new[] { 1, 0 })]
        [DataRow("", new[] { 0 })]
        [DataRow("a,b", new[] { 0, 0})]
        [DataRow("1,123456789123456", new[] { 1, 0 })]
        public void Parse_InputWithInvalidNumbers_CorrectNumberList(string input, int[] expectedNumbers)
        {
            var parser = new InputParser();

            List<int> numbers= parser.Parse(input);

            CollectionAssert.AreEquivalent(expectedNumbers, numbers);
        }

        [DataTestMethod]
        [DataRow("1\n2,3", new[] { 1, 2, 3 })]
        [DataRow("1\n2\n3", new[] { 1, 2, 3 })]
        [DataRow("\n", new[] { 0, 0 })]
        public void Parse_InputWithNewlineDelimiter_CorrectNumberList(string input, int[] expectedNumbers)
        {
            var parser = new InputParser();

            List<int> numbers= parser.Parse(input);

            CollectionAssert.AreEquivalent(expectedNumbers, numbers);
        }

        [DataTestMethod]
        [DataRow("1001,1001", new[] { 0, 0 })]
        [DataRow("2,1001,6", new[] { 2, 0, 6 })]
        public void Parse_InputWithNumberGreaterThan1000_CorrectNumberList(string input, int[] expectedNumbers)
        {
            var parser = new InputParser();

            List<int> numbers= parser.Parse(input);

            CollectionAssert.AreEquivalent(expectedNumbers, numbers);
        }

        [DataTestMethod]
        [DataRow("//#\n2#5", new[] { 2, 5 })]
        [DataRow("//,\n2,ff,100", new[] { 2, 0, 100 })]
        public void Parse_InputWithCustomSingleCharDelim_CorrectNumberList(string input, int[] expectedNumbers)
        {
            var parser = new InputParser();

            List<int> numbers = parser.Parse(input);

            CollectionAssert.AreEquivalent(expectedNumbers, numbers);
        }
    }
}
