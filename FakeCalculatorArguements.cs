
namespace calculatorChallenge
{
    /// <summary>
    /// fake arguements used by the calculator for unit testing
    /// </summary>
    public class FakeCalculatorArguements_Addition : ICalculatorArguements
    {
        public OperationType Operation { get; set; }
        public bool DenyNegativeNumbers { get; set; }
        public int UpperBound { get; set; }
        public string AlturnateDelimiter { get; set; }

        public FakeCalculatorArguements_Addition()
        {
            Operation = OperationType.Addition;
            DenyNegativeNumbers = true;
            UpperBound = 100;
            AlturnateDelimiter = "???";
        }
    }

    /// <summary>
    /// fake arguements used by the calculator for unit testing
    /// </summary>
    public class FakeCalculatorArguements_Subtraction : ICalculatorArguements
    {
        public OperationType Operation { get; set; }
        public bool DenyNegativeNumbers { get; set; }
        public int UpperBound { get; set; }
        public string AlturnateDelimiter { get; set; }

        public FakeCalculatorArguements_Subtraction()
        {
            Operation = OperationType.Subtraction;
            DenyNegativeNumbers = true;
            UpperBound = 600;
            AlturnateDelimiter = "!!";
        }
    }
}
