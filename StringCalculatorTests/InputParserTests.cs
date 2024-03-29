﻿using System;
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

        [DataTestMethod]
        [DataRow("//[***]\n11***22***33", new[] { 11, 22, 33 })]
        [DataRow("//[[]]\n11[]22[]33", new[] { 11, 22, 33 })]
        [DataRow("//[//]\n11//22//33", new[] { 11, 22, 33 })]
        public void Parse_InputWithOneCustomLengthDelimiter_CorrectNumberList(string input, int[] expectedNumbers)
        {
            var parser = new InputParser();

            List<int> numbers = parser.Parse(input);

            CollectionAssert.AreEquivalent(expectedNumbers, numbers);
        }

        [DataTestMethod]
        [DataRow("//[]\n11***22***33")]
        [DataRow("//\n2#5")]
        [DataRow("//[][!!][r9r]\n11r9r22*hh*33!!44")]
        public void Parse_InputMissingCustomDelimiter_ArgumentExceptionThrown(string input)
        {
            var parser = new InputParser();

            var ex = Assert.ThrowsException<ArgumentException>(() => parser.Parse(input));
        }

        [DataTestMethod]
        [DataRow("//[*][!!][r9r]\n11r9r22*hh*33!!44", new[] { 11, 22, 0, 33, 44 })]
        [DataRow("//[[]][[][//]\n11//22[]hh[]33[44", new[] { 11, 22, 0, 33, 44 })]
        public void Parse_InputWithMultipleCustomDelimiters_CorrectNumberList(string input, int[] expectedNumbers)
        {
            var parser = new InputParser();

            List<int> numbers = parser.Parse(input);

            CollectionAssert.AreEquivalent(expectedNumbers, numbers);
        }

        [DataTestMethod]
        [DataRow("\t", "1\t2,3", new[] { 1, 2, 3 })]
        [DataRow(";", "1;2;3", new[] { 1, 2, 3 })]
        [DataRow("-", "-", new[] { 0, 0 })]
        public void Parse_AlternateDelimiterParser_CorrectNumberList(string alternateDelim, 
            string input, int[] expectedNumbers)
        {
            var parser = new InputParser(alternateDelim);

            List<int> numbers = parser.Parse(input);

            CollectionAssert.AreEquivalent(expectedNumbers, numbers);
        }

        [DataTestMethod]
        [DataRow("1001,1001", 1002, new[] { 1001, 1001 })]
        [DataRow("2,1001,6", 8, new[] { 2, 0, 6 })]
        [DataRow("2,1001,6", 0, new[] { 0, 0, 0 })]
        public void Parse_DifferentUpperbound_CorrectNumberList(string input, int upperBound, int[] expectedNumbers)
        {
            var parser = new InputParser()
            {
                UpperBound = upperBound
            };

            List<int> numbers = parser.Parse(input);

            CollectionAssert.AreEquivalent(expectedNumbers, numbers);
        }
    }
}
