
namespace calculatorChallenge
{
    /// <summary>
    /// arguements used by the calculator
    /// </summary>
    public interface ICalculatorArguements
    {
        OperationType Operation { get; set; }
        bool DenyNegativeNumbers { get; set; }
        int UpperBound { get; set; }
        string AlturnateDelimiter { get; set; }
    }
}
