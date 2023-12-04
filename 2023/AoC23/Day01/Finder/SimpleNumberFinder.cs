namespace Day01.Finder;

internal class SimpleNumberFinder : INumberFinder
{
    public string FindNumber(string line)
    {
        foreach (char c in line)
        {
            var current = c.ToString();
            if (int.TryParse(current, out var _))
            {
                return current;
            }
        }
        return string.Empty;
    }
}
