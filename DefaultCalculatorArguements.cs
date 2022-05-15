
namespace calculatorChallenge
{
    /// <summary>
    /// default arguements used by the calculator
    /// </summary>
    public class DefaultCalculatorArguements : ICalculatorArguements
    {
        public bool DenyNegativeNumbers { get; set; }
        public int UpperBound { get; set; }
        public string AlturnateDelimiter { get; set; }

        public DefaultCalculatorArguements()
        {
            DenyNegativeNumbers = false;
            UpperBound = 1000;
        }
    }
}
