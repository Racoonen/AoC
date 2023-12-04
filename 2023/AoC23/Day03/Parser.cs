namespace Day03;

internal class Parser
{
    public Matrix Parse(string[] lines) => new Matrix(lines.Select(ParseLine).ToArray());

    private Entry[] ParseLine(string line) => line.Select(e => new Entry(e)).ToArray();
}
