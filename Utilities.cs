using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculatorChallenge
{
    internal class Utilities
    {
        public static bool TryParse(char input, out OperationType value)
        {
            bool isValid = false;

            value = OperationType.Unknown;

            switch (input)
            {
                case '+':
                    value = OperationType.Addition;
                    isValid = true;
                    break;

                case '-':
                    value = OperationType.Subtraction;
                    isValid = true;
                    break;

                case '*':
                    value = OperationType.Multiplication;
                    isValid = true;
                    break;

                case '/':
                    value = OperationType.Division;
                    isValid = true;
                    break;

                default:
                    break;
            }

            return isValid;
        }

        public static string GetOperationTypeChar(OperationType operation)
        {
            string operationChar = String.Empty;

            switch (operation)
            {
                case OperationType.Unknown:
                    operationChar = "?";
                    break;
                case OperationType.Addition:
                    operationChar = "+";
                    break;
                case OperationType.Subtraction:
                    operationChar = "-";
                    break;
                case OperationType.Multiplication:
                    operationChar = "*";
                    break;
                case OperationType.Division:
                    operationChar = "/";
                    break;
                default:
                    operationChar = "?";
                    break;
            }

            return operationChar;
        }

        public static string GetOperationTypeName(OperationType operation)
        {
            return Enum.GetName(typeof(OperationType), operation);
        }
    }
}
