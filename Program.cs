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
/// Created: May 13, 2022
/// Updated: May 14, 2022
/// </summary>
namespace calculatorChallenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            Calculator calculator = new Calculator();

            DisplayAvailableArguements();

            // added for stretch goal #3

            SetAndDisplayConsoleArguements(args, calculator);

            GetAndDisplayCommandLineArguements(args, calculator);

            GetAndProcessCalculatorAddition(calculator);

            RunUnitTests();

            Console.WriteLine();
            Console.WriteLine("Press enter key to exit");

            Console.ReadLine();

             Console.CancelKeyPress -= new ConsoleCancelEventHandler(Console_CancelKeyPress);
        }

        /// <summary>
        /// Display the available arguements (console and command line
        /// (1) Deny nagative numbers, (2) upper bound, or (3) alturnate delimiter
        /// </summary>
        static void DisplayAvailableArguements()
        {
            Console.WriteLine();
            Console.WriteLine("Available Arguements");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Deny negative numbers: boolean (true, false)");
            Console.WriteLine("Upper bound: integer");
            Console.WriteLine("Alturnate delimiter: string");
            Console.WriteLine("For example:  |?| 100 true");
            Console.WriteLine("---------------------------------------------");
        }

        /// <summary>
        /// Allow console command line arguements to be specified for
        /// (1) Deny nagative numbers, (2) upper bound, or (3) alturnate delimiter
        /// and set the calculator properties if specified
        /// </summary>
        /// <param name="args"></param>
        /// <param name="calculator"></param>
        static void SetAndDisplayConsoleArguements(string[] args, Calculator calculator)
        {
            string input = String.Empty;
            bool denyNegativeNumbers = false;
            int upperBound = 0;
            string alternateDelimiter = String.Empty;

            if (args.Length == 0)
            {
                Console.WriteLine("No console arguements specified");
            }
            else
            {
                Console.WriteLine("Specified Console Arguements");
                Console.WriteLine("---------------------------------------------");

                foreach (string arg in args)
                {
                    if (bool.TryParse(arg, out denyNegativeNumbers))
                    {
                        calculator.DenyNegativeNumbers = denyNegativeNumbers;
                        Console.WriteLine("Deny negative numbers = " + denyNegativeNumbers.ToString());
                    }
                    else if (int.TryParse(arg, out upperBound))
                    {
                        calculator.UpperBound = upperBound;
                        Console.WriteLine("Upper bound = " + upperBound.ToString());
                    }
                    else
                    {
                        calculator.AlturnateDelimiter = arg;
                        Console.WriteLine("Alturnate delimiter = " + arg);
                    }
                }
            }

            Console.WriteLine("---------------------------------------------");
        }

        /// <summary>
        /// Allow the user to specify up to 3 command line arguements for
        /// (1) Deny nagative numbers, (2) upper bound, or (3) alturnate delimiter
        /// and set the calculator properties if specified
        /// </summary>
        /// <param name="args"></param>
        /// <param name="calculator"></param>
        static void GetAndDisplayCommandLineArguements(string[] args, Calculator calculator)
        {
            string input = String.Empty;
            bool denyNegativeNumbers = false;
            int upperBound = 0;
            string alternateDelimiter = String.Empty;

            Console.WriteLine();
            Console.Write("Enter arguements (optional): ");
            input = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine();
                args = input.Split(' ');

                foreach (string arg in args)
                {
                    if (bool.TryParse(arg, out denyNegativeNumbers))
                    {
                        calculator.DenyNegativeNumbers = denyNegativeNumbers;
                        Console.WriteLine("Deny negative numbers = " + denyNegativeNumbers.ToString());
                    }
                    else if (int.TryParse(arg, out upperBound))
                    {
                        calculator.UpperBound = upperBound;
                        Console.WriteLine("Upper bound = " + upperBound.ToString());
                    }
                    else
                    {
                        calculator.AlturnateDelimiter = arg;
                        Console.WriteLine("Alturnate delimiter = " + arg);
                    }
                }

                Console.WriteLine("---------------------------------------------");
            }
        }

        /// <summary>
        /// Get numbers to add or Ctrl + C when finished
        /// </summary>
        /// <param name="calculator"></param>
        static void GetAndProcessCalculatorAddition(Calculator calculator)
        {
            string input = String.Empty;

            Console.WriteLine();
            Console.WriteLine("Enter input to add. Enter Ctrl + C when finished.");

            try
            {
                do
                {
                    Console.WriteLine();
                    Console.Write("Enter Integer: ");

                    input = Console.ReadLine();

                    if (input == null)
                    {
                        // Ctrl + C was entered
                        break;
                    }

                    calculator.Add(input);

                    Console.WriteLine("Total: " + calculator.TotalResult);

                } while (true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.ReadLine();
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Handle the Ctrl + C event to allow continued execution
        /// Code borrowed from http://www.blackwasp.co.uk/CaptureConsoleBreak.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("Finished");
            Console.WriteLine();

            if (e.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Run the calculator unit tests
        /// </summary>
        static void RunUnitTests()
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
                testCalculator.TestAdd_InputValueGreaterThanUpperBound_ReturnsInputSum();

                // added for requirement #6
                testCalculator.TestAdd_InputWithCustomDelimiter_ReturnsInputSum();
                testCalculator.TestAdd_InputWithCustomDelimiterComma_ReturnsInputSum();

                // added for requirement #7
                testCalculator.TestAdd_InputWithCustomDelimiterAnyLength_ReturnsInputSum();

                // added for requirement #8
                testCalculator.TestAdd_InputWithMultipleCustomDelimiterAnyLength_ReturnsInputSum();

                // added for stretch goal #1
                testCalculator.TestAdd_InputWithMultipleCustomDelimiterAnyLength_DisplayFormula();

                Console.WriteLine("All Test Cases Passed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Test Case Failed: " + e.Message);
            }

        }
    }

    public class Calculator
    {
        public const string NegativeNumberErrorMessage = "Input contains negative numbers:  ";
        public const int DefaultMaxAllowableInputValue = 1000;

        private int _upperBound = DefaultMaxAllowableInputValue;
        public int UpperBound { 
            get
            {
                return _upperBound; 
            }

            set
            {
                _upperBound = value;
            }
        }       

        public bool DenyNegativeNumbers { get; set; }

        public string AlturnateDelimiter { get; set; }

        public string Formula { get; set; }
        public int Total { get; set; }
        public string  TotalResult { 
            get
            {
                if (Total.ToString() == Formula)
                {
                    return Formula;
                }

                return Formula + " = " + Total.ToString();
            }
        }

        /// <summary>
        /// Supports a maximum of 2 numbers using a comma or newline delimiter. 
        ///      examples: 20 will return 20; 1,500 will return 501,
        ///      1,2,3,4,5,6,7,8,9,10,11,12 will return 78,  1\n2,3 will return 6
        /// Empty input or missing numbers should be converted to 0
        /// Invalid numbers should be converted to 0 e.g. 5,tytyt will return 5
        /// Negative numbers will throw an exception that includes all of the negative numbers provided
        /// Any value greater than 1000 is an invalid number and will be converted to 0
        /// Supports 1 custom delimiter of a single character using the format: //{delimiter}\n{numbers}
        ///      examples: //#\n2#5 will return 7; //,\n2,ff,100 will return 102
        /// Supports 1 custom delimiter of any length using the format: //[{delimiter}]\n{numbers}
        ///     example: //[***]\n11***22***33 will return 66
        /// Supports multiple delimiters of any length using the format: //[{delimiter1}][{delimiter2}]...\n{numbers}
        ///      example: //[*][!!][r9r]\n11r9r22*hh*33!!44 will return 110
        /// Displays the formula used to calculate the result 
        ///      example:  2,,4,rrrr,1001,6 will return 2+0+4+0+0+6 = 12
        /// NOTE: whitespace between the input values will be ignored
        /// </summary>
        /// <param name="input"></param>
        /// <returns>the sum of the delimited input values</returns>
        public int Add(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            string customDelimiter = String.Empty ;
            List<string> customDelimiterList = new List<string>();

            // look for custom delimiter of a single character using the format: //{delimiter}\n{numbers}
            if (input.Length > 5 && input.StartsWith("//") && input.Contains("\n"))
            {
                int indexOfNewline = input.IndexOf("\n");

                string customDelimiterItentifier = input.Substring(0, indexOfNewline + 1);

                // check for valid custom delimiter identifier and custom delimiter is not a comma (already in delimiter list)
                if (customDelimiterItentifier.EndsWith("\n") && customDelimiterItentifier[2] != ',')
                {
                    if (customDelimiterItentifier.StartsWith("//[") && customDelimiterItentifier.EndsWith("]\n"))
                    {
                        string multipleDelimiterIdentifiersCSV = customDelimiterItentifier.Replace("][", ",").Replace("//[", "").Replace("]\n", "");

                        string[] multipleDelimiterIdentifiers = multipleDelimiterIdentifiersCSV.Split(',');

                        foreach (string cd in multipleDelimiterIdentifiers)
                        {
                            customDelimiterList.Add(cd);
                        }

                        input = input.Substring(indexOfNewline + 1);
                    }
                    else
                    {
                        customDelimiter = customDelimiterItentifier.Substring(2, indexOfNewline - 2);
                        customDelimiterList.Add(customDelimiter);

                        input = input.Substring(indexOfNewline + 1);
                    }
                }
            }

            // add default delimeters if they have not already been added
            if (!customDelimiterList.Contains(","))
            {
                customDelimiterList.Add(",");
            }

            if (!customDelimiterList.Contains("\n"))
            {
                customDelimiterList.Add("\n");
            }

            // add custom delimeter if one was provided as an arguement and if they have not already been added it
            if ( !String.IsNullOrWhiteSpace(AlturnateDelimiter) && !customDelimiterList.Contains(AlturnateDelimiter))
            {
                customDelimiterList.Add(AlturnateDelimiter);
            }
            
            int returnValue = 0;
            string[] delimiters = customDelimiterList.ToArray();

            string[] splitInput = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string negativeNumbers = String.Empty;

            // 1 or more input values exist
            foreach (var splitInputString in splitInput)
            {
                int splitInputValue = 0;

                if (int.TryParse(splitInputString, out splitInputValue))
                {
                    if (DenyNegativeNumbers && splitInputValue < 0)
                    {
                        negativeNumbers += String.IsNullOrEmpty(negativeNumbers) ? splitInputValue.ToString() : ", " + splitInputValue.ToString();
                    }
                    else if (splitInputValue > UpperBound)
                    {
                        splitInputValue = 0;
                    }

                    returnValue += splitInputValue;

                    Formula += string.IsNullOrEmpty(Formula) ? splitInputValue.ToString() : " + " + splitInputValue.ToString();

                    Total += splitInputValue;
                }
            }

            if (DenyNegativeNumbers && !String.IsNullOrEmpty(negativeNumbers))
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
            Calculator calculator = new Calculator();
            string input = "";
            int expectedResult = 0;

            // act
            int actualResult = calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult );
        }

        [TestMethod()]
        public void TestAdd_NullInput_ReturnsZero()
        {
            //arrange
            Calculator calculator = new Calculator();
            string input = null;
            int expectedResult = 0;

            // act
            int actualResult = calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_OneInput_ReturnsInputValue()
        {
            //arrange
            Calculator calculator = new Calculator();
            string input = "20";
            int expectedResult = 20;

            // act
            int actualResult = calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_TwoInput_ReturnsInputSum()
        {
            //arrange
            Calculator calculator = new Calculator();
            string input = "1,500";
            int expectedResult = 501;

            // act
            int actualResult = calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_InvalidInput_ReturnsZeroAndInputSum()
        {
            //arrange
            Calculator calculator = new Calculator();
            string input = "5,tytyt";
            int expectedResult = 5;

            // act
            int actualResult = calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_MoreThanTwoInput_ReturnsInputSum()
        {
            //arrange
            Calculator calculator = new Calculator();
            string input = "1,2,3,4,5,6,7,8,9,10,11,12";
            int expectedResult = 78;

            // act
            int actualResult = calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_NewLineDelimitedInput_ReturnsInputSum()
        {
            //arrange
            Calculator calculator = new Calculator();
            string input = "1\n2,3";
            int expectedResult = 6;

            // act
            int actualResult = calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_NegativeNumber_ThrowsException()
        {
            //arrange
            Calculator calculator = new Calculator();
            calculator.DenyNegativeNumbers = true;

            string input = "1,-2,3";
            // expected result = an Argument Exception exception will be thrown with a message that will contain the negative number provided

            //act + assert
            Assert.ThrowsException<ArgumentException>(() => calculator.Add(input));
        }

        [TestMethod()]
        public void TestAdd_NegativeNumbers_ThrowsException()
        {
            //arrange
            Calculator calculator = new Calculator();
            calculator.DenyNegativeNumbers = true;

            string input = "1,-2,3,40,-42,-53,67";
            // expected result = an Argument Exception exception will be thrown with a message that lists all of the negative numbers provided

            //act + assert
            Assert.ThrowsException<ArgumentException>(() => calculator.Add(input));
        }

        [TestMethod()]
        public void TestAdd_NegativeNumbers_ThrowsException_Message()
        {
            //arrange
            Calculator calculator = new Calculator();
            calculator.DenyNegativeNumbers = true;

            string input = "1,-2,3,40,-42,-53,67";
            string expectedErrorMessage = Calculator.NegativeNumberErrorMessage + "-2, -42, -53";
            string actualErrorMessage = String.Empty;

            //act
            try
            {
                int actualResult = calculator.Add(input);
            }
            catch (Exception e)
            {
                actualErrorMessage = e.Message;
            }
            
            //assert
            Assert.AreEqual(actualErrorMessage, expectedErrorMessage);
        }

        [TestMethod()]
        public void TestAdd_InputValueGreaterThanUpperBound_ReturnsInputSum()
        {
            //arrange           
            Calculator calculator = new Calculator();
            string input = "1,2," + (calculator.UpperBound + 1) + ",4,5,";
            int expectedResult = 12;  // numbers above max allowable will be converted to 0

            // act
            int actualResult = calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_InputWithCustomDelimiter_ReturnsInputSum()
        {
            //arrange           
            Calculator calculator = new Calculator();
            string input = "//#\n2#5";
            int expectedResult = 7;  // # is the custom delimiter

            // act
            int actualResult = calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_InputWithCustomDelimiterComma_ReturnsInputSum()
        {
            //arrange           
            Calculator calculator = new Calculator();
            string input = "//,\n2,ff,100";
            int expectedResult = 102;  // , is the custom delimiter

            // act
            int actualResult = calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_InputWithCustomDelimiterAnyLength_ReturnsInputSum()
        {
            //arrange           
            Calculator calculator = new Calculator();
            string input = "//[***]\n11***22***33";
            int expectedResult = 66;  // *** is the custom delimiter

            // act
            int actualResult = calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_InputWithMultipleCustomDelimiterAnyLength_ReturnsInputSum()
        {
            //arrange           
            Calculator calculator = new Calculator();
            string input = "//[*][!!][r9r]\n11r9r22*hh*33!!44";
            int expectedResult = 110;  // multiple custom delimiters

            // act
            int actualResult = calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod()]
        public void TestAdd_InputWithMultipleCustomDelimiterAnyLength_DisplayFormula()
        {
            //arrange           
            Calculator calculator = new Calculator();
            string input = "//[*][!!][r9r]\n11r9r22*hh*33!!44";
            string expectedFormula = "11 + 22 + 33 + 44";

            // act
            calculator.Add(input);

            //assert
            Assert.AreEqual(calculator.Formula, expectedFormula);
        }
    }
}
