using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace calculatorChallenge
{
    /// <summary>
    /// Test cases for the calculator
    /// </summary>
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
            Assert.AreEqual(actualResult, expectedResult);
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
            calculator.Arguements.DenyNegativeNumbers = true;

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
            calculator.Arguements.DenyNegativeNumbers = true;

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
            calculator.Arguements.DenyNegativeNumbers = true;

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
            string input = "1,2," + (calculator.Arguements.UpperBound + 1) + ",4,5,";
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

        /// <summary>
        /// added for stretch goal #4 - Use DI
        /// </summary>
        [TestMethod()]
        public void TestAdd_InputWithMultipleCustomDelimiterAnyLength_DisplayFormula_UsingDI()
        {
            //arrange           
            FakeCalculatorArguements mockArguements = new FakeCalculatorArguements();
            Calculator calculator = new Calculator(mockArguements);   //  UpperBound = 100 and AlturnateDelimiter = "???"
            string input = "//[*][!!][r9r]\n11r9r22*hh*33!!44???55???101";
            string expectedFormula = "11 + 22 + 33 + 44 + 55 + 0";  

            // act
            calculator.Add(input);

            //assert
            Assert.AreEqual(calculator.Formula, expectedFormula);
        }

        [TestMethod()]
        public void TestAdd_InputWithCustomDelimiterAnyLength_ReturnsInputSum_UsingDI()
        {
            //arrange           
            FakeCalculatorArguements mockArguements = new FakeCalculatorArguements();
            Calculator calculator = new Calculator(mockArguements);   //  UpperBound = 100 and AlturnateDelimiter = "???"
            string input = "//[***]\n11***22***33???44???55,123,456";
            int expectedResult = 165;  // *** is the custom delimiter

            // act
            int actualResult = calculator.Add(input);

            //assert
            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
