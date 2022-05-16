using System;
using System.Collections.Generic;

namespace calculatorChallenge
{
    public class Calculator
    {
        public readonly ICalculatorArguements Arguements;

        public string Formula { get; set; }
        public decimal Total { get; set; }
        public string TotalResult
        {
            get
            {
                return Formula + " = " + Total.ToString();
            }
        }

        public Calculator()
        {
            Arguements = new DefaultCalculatorArguements();
        }

        public Calculator(ICalculatorArguements arguements)
        {
            Arguements = arguements ?? throw new ArgumentNullException(nameof(arguements));
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
        ///  Support addition, subtraction, multiplication, and division operations
        /// NOTE: whitespace between the input values will be ignored
        /// </summary>
        /// <param name="input"></param>
        /// <returns>the calculated result of the delimited input values basedon the operation type</returns>
        public decimal Calculate (string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

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
                        string customDelimiter = customDelimiterItentifier.Substring(2, indexOfNewline - 2);
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
            if (!String.IsNullOrWhiteSpace(Arguements.AlturnateDelimiter) && !customDelimiterList.Contains(Arguements.AlturnateDelimiter))
            {
                customDelimiterList.Add(Arguements.AlturnateDelimiter);
            }

            decimal returnValue = 0;
            string[] delimiters = customDelimiterList.ToArray();

            string[] splitInput = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string negativeNumbers = String.Empty;

            Total = 0;
            Formula = String.Empty;
            int inputCount = 0;

            // 1 or more input values exist
            foreach (var splitInputString in splitInput)
            {
                int splitInputValue = 0;

                if (int.TryParse(splitInputString, out splitInputValue))
                {
                    if (Arguements.DenyNegativeNumbers && splitInputValue < 0)
                    {
                        negativeNumbers += String.IsNullOrEmpty(negativeNumbers) ? splitInputValue.ToString() : ", " + splitInputValue.ToString();
                    }
                    else if (splitInputValue > Arguements.UpperBound)
                    {
                        splitInputValue = 0;
                    }

                    if (inputCount++ == 0)
                    {
                        returnValue = splitInputValue;
                        Formula = splitInputValue.ToString();
                        continue;
                    }

                    switch (Arguements.Operation)
                    {
                        case OperationType.Addition:
                            returnValue += splitInputValue;
                            break;
                        case OperationType.Subtraction:
                            returnValue -= splitInputValue;
                            break;
                        case OperationType.Multiplication:
                            returnValue *= splitInputValue;
                            break;
                        case OperationType.Division:
                            returnValue /= splitInputValue;
                            break;
                    }

                    string operationChar = Utilities.GetOperationTypeChar(Arguements.Operation);

                    Formula += " "  + operationChar + " " + splitInputValue.ToString();
                }
            }

            if (Arguements.DenyNegativeNumbers && !String.IsNullOrEmpty(negativeNumbers))
            {
                throw new ArgumentException(Constants.NegativeNumberErrorMessage + negativeNumbers);
            }
            
            Total = returnValue;

            return returnValue;
        }
    }
}
