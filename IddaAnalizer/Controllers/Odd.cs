namespace IddaAnalyser
{
    public class Odd
    {
        public Odd()
        {
        }
        public Odd(OddItem oddItem)
        {
            SetOddName(oddItem);
        }
        public Odd(OddItem oddItem, double doubleValue)
        {
            SetOddName(oddItem);
            SetOddValue(doubleValue);
        }
        public int IntName { get; set; }

        public string StrValue
        {
            get
            {
                return ((OddItem)IntName) + ":" + DoubleValue;
            }
        }

        public double DoubleValue;

        public OddItem OddItemName
        {
            get
            {
                return (OddItem)IntName;
            }
        }

        public SpecialSim OddSelectionName
        {
            get
            {
                return ((SpecialSim)IntName);
            }
        }

        public void SetOddValue(double value)
        {
            this.DoubleValue = value;
        }

        public void SetOddName(OddItem OddItemName)
        {
            this.IntName = (int)OddItemName;
        }
        public void SetOddName(SpecialSim oddSelectionName)
        {
            this.IntName = (int)oddSelectionName;
        }
        public void SetOddName(int intName)
        {
            this.IntName = intName;
        }

    }
}
