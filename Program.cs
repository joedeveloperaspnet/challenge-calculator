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

                // added for requirement #4
                testCalculator.TestAdd_NegativeNumber_ThrowsException();
                testCalculator.TestAdd_NegativeNumbers_ThrowsException_Message();

                // added for requirement #5
                testCalculator.TestAdd_InputValueGreaterThan1000_ReturnsInputSum();

                // added for requirement #6
                testCalculator.TestAdd_InputWithCustomDelimiter_ReturnsInputSum();
                testCalculator.TestAdd_InputWithCustomDelimiterComma_ReturnsInputSum();

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
        public const string NegativeNumberErrorMessage = "Input contains negative numbers:  ";
        public const int MaxAllowableInputValue = 1000;

        /// <summary>
        /// Support a maximum of 2 numbers using a comma or newline delimiter. 
        /// examples: 20 will return 20; 1,500 will return 501,
        ///      1,2,3,4,5,6,7,8,9,10,11,12 will return 78,  1\n2,3 will return 6
        /// empty input or missing numbers should be converted to 0
        /// invalid numbers should be converted to 0 e.g. 5,tytyt will return 5
        /// negative numbers will throw an exception that includes all of the negative numbers provided
        /// any value greater than 1000 is an invalid number and will be converted to 0
        /// Support 1 custom delimiter of a single character using the format: //{delimiter}\n{numbers}
        ///      examples: //#\n2#5 will return 7; //,\n2,ff,100 will return 102
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

            char? customDelimiter = null ;

            // look for custom delimiter of a single character using the format: //{delimiter}\n{numbers}
            if (input.Length > 5 && input.StartsWith("//"))
            {
                string customDelimiterItentifier = input.Substring(0, 4);

                // check for valid custom delimiter identifier and custom delimiter is not a comma (already in delimiter list)
                if (customDelimiterItentifier.EndsWith("\n") && customDelimiterItentifier[2] != ',')
                {
                    customDelimiter = customDelimiterItentifier[2];
                    input = input.Substring(4);
                }
            }

            int returnValue = 0;
            char[] standardDelimiters =  { ',', '\n' };
            char[] customDelimiters =  { ',', '\n', customDelimiter.HasValue ? customDelimiter.Value : default(char) };
            char[] delimiters = customDelimiter.HasValue ? customDelimiters : standardDelimiters;

            string[] splitInput = input.Split(delimiters);
            string negativeNumbers = String.Empty;

            // 1 or more input values exist
            foreach (var splitInputString in splitInput)
            {
                int splitInputValue = 0;

                if (int.TryParse(splitInputString, out splitInputValue))
                {
                    if (splitInputValue < 0)
                    {
                        negativeNumbers += String.IsNullOrEmpty(negativeNumbers) ? splitInputValue.ToString() : ", " + splitInputValue.ToString();
                    }
                    else if (splitInputValue > MaxAllowableInputValue)
                    {
                        splitInputValue = 0;
                    }

                    returnValue += splitInputValue;
                }
            }

            if (!String.IsNullOrEmpty(negativeNumbers))
            {
                throw new ArgumentException(NegativeNumberErrorMessage + negativeNumbers);
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
            string input = "1,500";
            int expectedResult = 501;

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

        [TestMethod()]
        public void TestAdd_NegativeNumber_ThrowsException()
        {
            //arrange
            string input = "1,-2,3";
            // expected result = an Argument Exception exception will be thrown with a message that will contain the negative number provided

            //act + assert
            Assert.ThrowsException<ArgumentException>(() => Calculator.Add(input));
        }

        [TestMethod()]
        public void TestAdd_NegativeNumbers_ThrowsException()
        {
            //arrange
            string input = "1,-2,3,40,-42,-53,67";
            // expected result = an Argument Exception exception will be thrown with a message that lists all of the negative numbers provided

            //act + assert
            Assert.ThrowsException<ArgumentException>(() => Calculator.Add(input));
        }

        [TestMethod()]
        public void TestAdd_NegativeNumbers_ThrowsException_Message()
        {
            //arrange
            string input = "1,-2,3,40,-42,-53,67";
            string expectedErrorMessage = Calculator.NegativeNumberErrorMessage + "-2, -42, -53";
            string actualErrorMessage = String.Empty;

            //act
            try
            {
                int actualResult = Calculator.Add(input);
            }
            catch (Exception e)
            {
                actualErrorMessage = e.Message;
            }
            
            //assert
            Assert.AreEqual(actualErrorMessage, expectedErrorMessage);
        }

        [TestMethod()]
        public void TestAdd_InputValueGreaterThan1000_ReturnsInputSum()
        {
            //arrange           
            string input = "1,2," + (Calculator.MaxAllowableInputValue + 1) + ",4,5,";
            int expectedResult = 12;  // numbers above max allowable will be converted to 0

            // act
            int actualResult = Calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_InputWithCustomDelimiter_ReturnsInputSum()
        {
            //arrange           
            string input = "//#\n2#5";
            int expectedResult = 7;  // # is the custom delimiter

            // act
            int actualResult = Calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_InputWithCustomDelimiterComma_ReturnsInputSum()
        {
            //arrange           
            string input = "//,\n2,ff,100";
            int expectedResult = 102;  // , is the custom delimiter

            // act
            int actualResult = Calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
