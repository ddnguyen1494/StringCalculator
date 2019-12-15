using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculator;

namespace StringCalculatorTests
{
    [TestClass]
    public class CalculatorTests
    {
        [DataTestMethod]
        [DataRow("1,5000", 5001)]
        [DataRow("4,-3", 1)]
        [DataRow("0,-1", -1)]
        [DataRow("1000,2000", 3000)]
        public void Calculate_InputWithTwoNumbers_CorrectSum(string input, int expectedSum)
        {
            var calculator = new Calculator();

            int sum = calculator.Calculate(input);

            Assert.AreEqual(expectedSum, sum);
        }

        [DataTestMethod]
        [DataRow("1,2,5")]
        [DataRow("1,2,abc")]
        [DataRow("a,b,c")]
        public void Calculate_InputWithMoreThanTwoNumbers_ArgumentExceptionThrown(string input)
        {
            var calculator = new Calculator();

            Assert.ThrowsException<ArgumentException>(() => calculator.Calculate(input));
        }

        [DataTestMethod]
        [DataRow("5, tytyt", 5)]
        [DataRow(",", 0)]
        [DataRow("1,", 1)]
        [DataRow("", 0)]
        [DataRow("a,b", 0)]
        [DataRow("1,123456789123456", 1)]
        public void Calculate_InputWithInvalidNumbers_CorrectSum(string input , int expectedSum)
        {
            var calculator = new Calculator();

            int sum = calculator.Calculate(input);

            Assert.AreEqual(expectedSum, sum);
        }
    }
}
