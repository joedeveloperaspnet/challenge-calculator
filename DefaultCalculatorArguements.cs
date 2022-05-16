
namespace calculatorChallenge
{
    /// <summary>
    /// default arguements used by the calculator
    /// </summary>
    public class DefaultCalculatorArguements : ICalculatorArguements
    {
        public OperationType Operation { get; set; }
        public bool DenyNegativeNumbers { get; set; }
        public int UpperBound { get; set; }
        public string AlturnateDelimiter { get; set; }

        public DefaultCalculatorArguements()
        {
            Operation = OperationType.Addition;
            DenyNegativeNumbers = false;
            UpperBound = 1000;
        }
    }
}
