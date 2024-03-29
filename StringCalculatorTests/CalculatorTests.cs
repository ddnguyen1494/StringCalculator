﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculator;

namespace StringCalculatorTests
{
    [TestClass]
    public class CalculatorTests
    {
        [DataTestMethod]
        [DataRow("4,3", 7)]
        [DataRow("0,1", 1)]
        [DataRow("1000,1000", 2000)]
        [DataRow("1,2,3,4,5,6,7,8,9,10,11,12", 78)]
        public void Calculate_AddPositiveNumbers_CorrectSum(string input, int expectedSum)
        {
            var mockParser = new MockInputParser();
            var calculator = new Calculator(mockParser);

            var output = calculator.Calculate(input, "+");

            Assert.AreEqual(expectedSum, output.Result);
        }

        [DataTestMethod]
        [DataRow("-1,2,3", "Invalid negative numbers: -1")]
        [DataRow("-1,-2,-3", "Invalid negative numbers: -1 -2 -3")]
        public void Calculate_NegativeNumbers_ExceptionThrownWithCorrectMessage(string input, string expectedMessage)
        {
            var mockParser = new MockInputParser();
            var calculator = new Calculator(mockParser);

            var ex = Assert.ThrowsException<ArgumentException>(() => calculator.Calculate(input, "+"));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [DataTestMethod]
        [DataRow("-1,2,3", 4)]
        [DataRow("-1,-2,-3", -6)]
        public void Calculate_AllowNegativeNumbers_CorrectResult(string input, int expectedSum)
        {
            var mockParser = new MockInputParser();
            var calculator = new Calculator(mockParser)
            {
                AllowNegativeNumbers = true
            };

            var output = calculator.Calculate(input, "+");

            Assert.AreEqual(expectedSum, output.Result);
        }

        [DataTestMethod]
        [DataRow("4,3", 1)]
        [DataRow("0,1", -1)]
        [DataRow("1000,1000", 0)]
        public void Calculate_SubtractNumber_CorrectResult(string input, double expectedResult)
        {
            var mockParser = new MockInputParser();
            var calculator = new Calculator(mockParser);

            var output = calculator.Calculate(input, "-");

            Assert.AreEqual(expectedResult, output.Result);
        }

        [DataTestMethod]
        [DataRow("4,3", 12)]
        [DataRow("0,1", 0)]
        [DataRow("1000,1000", 1000000)]
        public void Calculate_MultiplyNumbers_CorrectResult(string input, double expectedResult)
        {
            var mockParser = new MockInputParser();
            var calculator = new Calculator(mockParser);

            var output = calculator.Calculate(input, "*");

            Assert.AreEqual(expectedResult, output.Result);
        }

        [DataTestMethod]
        [DataRow("4,2", 2)]
        [DataRow("0,1", 0)]
        [DataRow("1000,1000", 1)]
        [DataRow("5,2", 2.5)]
        public void Calculate_DivideNumbers_CorrectResult(string input, double expectedResult)
        {
            var mockParser = new MockInputParser();
            var calculator = new Calculator(mockParser);

            var output = calculator.Calculate(input, "/");

            Assert.AreEqual(expectedResult, output.Result);
        }

        [DataTestMethod]
        [DataRow("4,0")]
        [DataRow("5,2,0")]
        public void Calculate_DivideByZero_DivideByZeroExceptionThrown(string input)
        {
            var mockParser = new MockInputParser();
            var calculator = new Calculator(mockParser);

            var ex = Assert.ThrowsException<DivideByZeroException>(() => calculator.Calculate(input, "/"));
        }
    }
}
