namespace Day03
{
    internal record Entry(char Value)
    {
        private readonly char gearMarker = '*';
        private readonly char skip = '.';
        private readonly char[] numbers = new[]
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
        };

        public bool IsGearMarker => Value.Equals(gearMarker);
        public bool IsSkip => Value.Equals(skip);
        public bool IsSymbol => !IsNumber && !IsSkip;
        public bool IsNumber => numbers.Contains(Value);
        public override string ToString() => Value.ToString();
    }
}
