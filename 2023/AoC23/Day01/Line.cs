using Day01.Finder;

namespace Day01
{
    internal record Line(string Value, bool IsFirstGame)
    {
        private readonly NumberFinderFactory numberFinderFactory = new();

        public int GetNumber() => int.Parse($"{GetFirstNumber()}{GetSecondNumber()}");

        private string GetFirstNumber() => GenerateNumberFinder(false).FindNumber(Value);

        private string GetSecondNumber() => GenerateNumberFinder(true).FindNumber(Value.Reverse());

        private INumberFinder GenerateNumberFinder(bool isSecond)
        {
            if (IsFirstGame)
            {
                return numberFinderFactory.Build(NumberFinderStrategy.Simple);
            }
            else if (isSecond)
            {
                return numberFinderFactory.Build(NumberFinderStrategy.ReversedComplex);
            }
            return numberFinderFactory.Build(NumberFinderStrategy.Complex);
        }
    }
}
