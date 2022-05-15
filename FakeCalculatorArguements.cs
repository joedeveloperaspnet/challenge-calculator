
namespace calculatorChallenge
{
    /// <summary>
    /// fake arguements used by the calculator for uint testing
    /// </summary>
    public class FakeCalculatorArguements : ICalculatorArguements
    {
        public bool DenyNegativeNumbers { get; set; }
        public int UpperBound { get; set; }
        public string AlturnateDelimiter { get; set; }

        public FakeCalculatorArguements()
        {
            DenyNegativeNumbers = true;
            UpperBound = 100;
            AlturnateDelimiter = "???";
        }
    }
}
