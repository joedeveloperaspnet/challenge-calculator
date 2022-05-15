using System;

namespace calculatorChallenge
{
    static internal class UnitTests
    {
        /// <summary>
        /// Run the calculator unit tests
        /// </summary>
        public static void RunUnitTests()
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

                // added for stretch goal #4
                testCalculator.TestAdd_InputWithMultipleCustomDelimiterAnyLength_DisplayFormula_UsingDI();
                testCalculator.TestAdd_InputWithCustomDelimiterAnyLength_ReturnsInputSum_UsingDI();

                Console.WriteLine("All Test Cases Passed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Test Case Failed: " + e.Message);
            }
        }
    }
}
