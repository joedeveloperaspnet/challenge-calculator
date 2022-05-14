using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// Restarurant365 Code Challenge: String Calculator
/// Candidate: Joseph DiPierro
/// Date: May 13, 2022
/// </summary>
namespace calculatorChallenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CalculatorTestCases testCalculator = new CalculatorTestCases();

            try
            {
                testCalculator.TestAdd_EmptyInput_ReturnsZero();

                testCalculator.TestAdd_NullInput_ReturnsZero();

                testCalculator.TestAdd_OneInput_ReturnsInputValue();

                testCalculator.TestAdd_TwoInput_ReturnsInputSum();

                testCalculator.TestAdd_InvalidInput_ReturnsZeroAndInputSum();

                // added for requirement #2
                testCalculator.TestAdd_MoreThanTwoInput_ReturnsInputSum();

                // added for requirement #3
                testCalculator.TestAdd_NewLineDelimitedInput_ReturnsInputSum();

                Console.WriteLine("All Test Cases Passed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Test Case Failed: " + e.Message);
            }

            Console.ReadLine();
       }
    }

    static class Calculator
    {
        /// <summary>
        /// Support a maximum of 2 numbers using a comma delimiter. Throw an exception when more than 2 numbers are provided
        /// examples: 20 will return 20; 1,5000 will return 5001; 4,-3 will return 1
        /// empty input or missing numbers should be converted to 0
        /// invalid numbers should be converted to 0 e.g. 5,tytyt will return 5
        /// NOTE: whitespace between the input values will be ignored
        /// </summary>
        /// <param name="input"></param>
        /// <returns>the sum of the delimited input values</returns>
        public static int Add(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            int returnValue = 0;
            char[] delimiter = { ',', '\n' };
            string[] splitInput = input.Split(delimiter);

            if (splitInput.Count() == 1)
            {
                int.TryParse(splitInput[0], out returnValue);
            }
            else
            {
                // 2 or more input values exist
                foreach (var splitInputString in splitInput)
                {
                    int splitInputValue = 0;

                    if (int.TryParse(splitInputString, out splitInputValue))
                    {
                        returnValue += splitInputValue;
                    }
                }
            }

            return returnValue;
        }
    }

    internal class CalculatorTestCases
    {
        [TestMethod()]
        public void TestAdd_EmptyInput_ReturnsZero()
        {
            //arrange
            string input = "";
            int expectedResult = 0;

            // act
            int actualResult = Calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult );
        }

        [TestMethod()]
        public void TestAdd_NullInput_ReturnsZero()
        {
            //arrange
            string input = null;
            int expectedResult = 0;

            // act
            int actualResult = Calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_OneInput_ReturnsInputValue()
        {
            //arrange
            string input = "20";
            int expectedResult = 20;

            // act
            int actualResult = Calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_TwoInput_ReturnsInputSum()
        {
            //arrange
            string input = "1,5000";
            int expectedResult = 5001;

            // act
            int actualResult = Calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_InvalidInput_ReturnsZeroAndInputSum()
        {
            //arrange
            string input = "5,tytyt";
            int expectedResult = 5;

            // act
            int actualResult = Calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_MoreThanTwoInput_ReturnsInputSum()
        {
            //arrange
            string input = "1,2,3,4,5,6,7,8,9,10,11,12";
            int expectedResult = 78;

            // act
            int actualResult = Calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_NewLineDelimitedInput_ReturnsInputSum()
        {
            //arrange
            string input = "1\n2,3";
            int expectedResult = 6;

            // act
            int actualResult = Calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

    }

}
