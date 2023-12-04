namespace Day02;

internal record Draw(IReadOnlyCollection<Gem> Gems)
{
    public int GetRedCount()
        => Gems.Where(e => e.Color == Color.Red).FirstOrDefault()?.Count ?? 0;

    public int GetGreenCount()
        => Gems.Where(e => e.Color == Color.Green).FirstOrDefault()?.Count ?? 0;

    public int GetBlueCount()
        => Gems.Where(e => e.Color == Color.Blue).FirstOrDefault()?.Count ?? 0;
}
