using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculatorChallenge
{
    public static class Constants
    {
        public const string NegativeNumberErrorMessage = "Input contains negative number(s):  ";
    }

    public enum OperationType
    {
        Unknown,
        Addition,
        Subtraction,
        Multiplication,
        Division
    }
}