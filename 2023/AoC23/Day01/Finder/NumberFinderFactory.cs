namespace Day01.Finder
{
    internal class NumberFinderFactory
    {
        private Dictionary<string, int> possibleNumbers = new()
        {
            { "zero", 0 },
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
        };

        public NumberFinderFactory()
        {
            reversedPossibleNumbers = possibleNumbers.ToDictionary(e => e.Key.Reverse(), v => v.Value);
        }

        private Dictionary<string, int> reversedPossibleNumbers;

        public INumberFinder Build(NumberFinderStrategy strategy)
            => strategy switch
            {
                NumberFinderStrategy.Simple => new SimpleNumberFinder(),
                NumberFinderStrategy.Complex => new ComplexNumberFinder(possibleNumbers),
                NumberFinderStrategy.ReversedComplex => new ComplexNumberFinder(reversedPossibleNumbers),
                _ => throw new ArgumentException($"Unknown Strategy {strategy}"),
            };
    }
}
