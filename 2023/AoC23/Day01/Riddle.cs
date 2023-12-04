namespace Day01;

internal class Riddle
{
    private readonly IReadOnlyCollection<Line> lines;

    public Riddle(IReadOnlyCollection<Line> lines)
    {
        this.lines = lines ?? throw new ArgumentNullException(nameof(lines));
    }

    internal string Solve()
    {
        var result = 0;
        foreach (var line in lines)
        {
            result += line.GetNumber();
        }
        return result.ToString();
    }
}
