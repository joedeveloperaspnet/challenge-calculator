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
            decimal expectedResult = 0;

            // act
            decimal actualResult = calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void TestAdd_NullInput_ReturnsZero()
        {
            //arrange
            Calculator calculator = new Calculator();
            string input = null;
            Decimal expectedResult = 0;

            // act
            decimal actualResult = calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void TestAdd_OneInput_ReturnsInputValue()
        {
            //arrange
            Calculator calculator = new Calculator();
            string input = "20";
            decimal expectedResult = 20;

            // act
            decimal actualResult = calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void TestAdd_TwoInput_ReturnsInputSum()
        {
            //arrange
            Calculator calculator = new Calculator();
            string input = "1,500";
            Decimal expectedResult = 501;

            // act
            Decimal actualResult = calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void TestAdd_InvalidInput_ReturnsZeroAndInputSum()
        {
            //arrange
            Calculator calculator = new Calculator();
            string input = "5,tytyt";
            decimal expectedResult = 5;

            // act
            decimal actualResult = calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void TestAdd_MoreThanTwoInput_ReturnsInputSum()
        {
            //arrange
            Calculator calculator = new Calculator();
            string input = "1,2,3,4,5,6,7,8,9,10,11,12";
            decimal expectedResult = 78;

            // act
            decimal actualResult = calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void TestAdd_NewLineDelimitedInput_ReturnsInputSum()
        {
            //arrange
            Calculator calculator = new Calculator();
            string input = "1\n2,3";
            decimal expectedResult = 6;

            // act
            decimal actualResult = calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
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
            Assert.ThrowsException<ArgumentException>(() => calculator.Calculate(input));
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
            Assert.ThrowsException<ArgumentException>(() => calculator.Calculate(input));
        }

        [TestMethod()]
        public void TestAdd_NegativeNumbers_ThrowsException_Message()
        {
            //arrange
            Calculator calculator = new Calculator();
            calculator.Arguements.DenyNegativeNumbers = true;

            string input = "1,-2,3,40,-42,-53,67";
            string expectedErrorMessage = Constants.NegativeNumberErrorMessage + "-2, -42, -53";
            string actualErrorMessage = String.Empty;

            //act
            try
            {
                calculator.Calculate(input);
            }
            catch (Exception e)
            {
                actualErrorMessage = e.Message;
            }

            //assert
            Assert.AreEqual( expectedErrorMessage, actualErrorMessage);
        }

        [TestMethod()]
        public void TestAdd_InputValueGreaterThanUpperBound_ReturnsInputSum()
        {
            //arrange           
            Calculator calculator = new Calculator();
            string input = "1,2," + (calculator.Arguements.UpperBound + 1) + ",4,5,";
            decimal expectedResult = 12;  // numbers above max allowable will be converted to 0

            // act
            decimal actualResult = calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void TestAdd_InputWithCustomDelimiter_ReturnsInputSum()
        {
            //arrange           
            Calculator calculator = new Calculator();
            string input = "//#\n2#5";
            decimal expectedResult = 7;  // # is the custom delimiter

            // act
            decimal actualResult = calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void TestAdd_InputWithCustomDelimiterComma_ReturnsInputSum()
        {
            //arrange           
            Calculator calculator = new Calculator();
            string input = "//,\n2,ff,100";
            decimal expectedResult = 102;  // , is the custom delimiter

            // act
            decimal actualResult = calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void TestAdd_InputWithCustomDelimiterAnyLength_ReturnsInputSum()
        {
            //arrange           
            Calculator calculator = new Calculator();
            string input = "//[***]\n11***22***33";
            decimal expectedResult = 66;  // *** is the custom delimiter

            // act
            decimal actualResult = calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void TestAdd_InputWithMultipleCustomDelimiterAnyLength_ReturnsInputSum()
        {
            //arrange           
            Calculator calculator = new Calculator();
            string input = "//[*][!!][r9r]\n11r9r22*hh*33!!44";
            decimal expectedResult = 110;  // multiple custom delimiters

            // act
            decimal actualResult = calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void TestAdd_InputWithMultipleCustomDelimiterAnyLength_DisplayFormula()
        {
            //arrange           
            Calculator calculator = new Calculator();
            string input = "//[*][!!][r9r]\n11r9r22*hh*33!!44";
            string expectedFormula = "11 + 22 + 33 + 44";

            // act
            calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedFormula, calculator.Formula);
        }

        /// <summary>
        /// added for stretch goal #4 - Use DI
        /// </summary>
        [TestMethod()]
        public void TestAdd_InputWithMultipleCustomDelimiterAnyLength_DisplayFormula_UsingDI()
        {
            //arrange           
            FakeCalculatorArguements_Addition mockArguements = new FakeCalculatorArguements_Addition();
            Calculator calculator = new Calculator(mockArguements);   //  UpperBound = 100 and AlturnateDelimiter = "???"
            string input = "//[*][!!][r9r]\n11r9r22*hh*33!!44???55???101";
            string expectedFormula = "11 + 22 + 33 + 44 + 55 + 0";  

            // act
            calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedFormula, calculator.Formula);
        }

        /// <summary>
        /// added for stretch goal #4 - Use DI
        /// </summary>
        [TestMethod()]
        public void TestAdd_InputWithCustomDelimiterAnyLength_ReturnsInputSum_UsingDI()
        {
            //arrange           
            FakeCalculatorArguements_Subtraction mockArguements = new FakeCalculatorArguements_Subtraction();
            Calculator calculator = new Calculator(mockArguements);   //  UpperBound = 600 and AlturnateDelimiter = "!!"
            string input = "//[***]\n500,10***20***30!!40!!50,601";
            decimal expectedResult = 350;  // *** is the custom delimiter

            // act
            decimal actualResult = calculator.Calculate(input);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
