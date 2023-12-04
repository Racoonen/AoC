namespace Day04;

internal class Parser
{
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

    public Game Parse(string[] lines) => new Game(lines.Select(ParseLine).ToArray());

    private Card ParseLine(string line)
    {
        var parts = line.Split(':');
        var id = ParseId(parts[0]);
        var numbers = ParseNumbers(parts[1]);

        return new(id, numbers.win, numbers.own);
    }

    private (int[] win, int[] own) ParseNumbers(string line)
    {
        var parts = line.Split('|');
        return (ParseNumbersCore(parts[0]), ParseNumbersCore(parts[1]));
    }

    private int[] ParseNumbersCore(string line) => line.Split(' ').Where(e => !string.IsNullOrEmpty(e)).Select(int.Parse).ToArray();

    private int ParseId(string line) => int.Parse(new string(line.Where(e => numbers.Contains(e)).ToArray()));
}
