namespace Day04;

internal class Card
{
    private readonly int id;
    private readonly int[] winningNumbers;
    private readonly int[] ownNumbers;

    public Card(int id, int[] winningNumbers, int[] ownNumbers)
    {
        this.id = id;
        this.winningNumbers = winningNumbers ?? throw new ArgumentNullException(nameof(winningNumbers));
        this.ownNumbers = ownNumbers ?? throw new ArgumentNullException(nameof(ownNumbers));
    }

    public int Id => id;
    public double CalcWorth()
    {
        var count = winningNumbers.Intersect(ownNumbers).Count();
        if (count < 1)
            return 0.0;
        return Math.Pow(2, count - 1);
    }

    internal int CalcWinCount() => winningNumbers.Intersect(ownNumbers).Count();
}
