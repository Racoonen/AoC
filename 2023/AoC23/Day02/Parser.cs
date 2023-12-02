namespace Day02
{
    internal class Parser
    {
        private readonly char[] validNumbers = new[]
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

        public Game ParseLine(string line)
        {
            var indexOfDouble = line.IndexOf(':');
            var id = GetNumberInLine(line.Substring(0, indexOfDouble));
            var draws = GetDrawsOfGame(line.Substring(indexOfDouble + 1));
            return new Game(id, draws);
        }

        private IReadOnlyCollection<Draw> GetDrawsOfGame(string line) 
            => line.Split(";").Select(GetDrawOfLine).ToArray();
        private Draw GetDrawOfLine(string line) 
            => new Draw(line.Split(',').Select(GetGemOfLine).ToArray());

        private Gem GetGemOfLine(string line) 
            => new Gem(GetNumberInLine(line), GetColorOfGem(line));

        private Color GetColorOfGem(string value)
        {
            var colorAsString = new string(value.Where(e=> !validNumbers.Contains(e)).ToArray());
            switch(colorAsString.Trim())
            {
                case "red":
                    return Color.Red;
                case "blue":
                    return Color.Blue;
                case "green":
                    return Color.Green;
                default:
                    throw new ArgumentException($"No known color {colorAsString.Trim()}");
            }
        }

        private int GetNumberInLine(string value)
        {
            var idAsString = new string(value.Where(validNumbers.Contains).ToArray());
            return int.Parse(idAsString);
        }
    }
}
