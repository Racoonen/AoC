namespace Day02
{
    internal record Game(int Id, IReadOnlyCollection<Draw> Draws)
    {
        public bool IsPossible(int red, int green, int blue)
        {
            if (Draws.Any(e => e.GetRedCount() > red))
                return false;
            if (Draws.Any(e => e.GetGreenCount() > green))
                return false;
            if (Draws.Any(e => e.GetBlueCount() > blue))
                return false;
            return true;
        }

        public int GetPower()
        {
            var red = Draws.Where(e => e.GetRedCount() > 0).Max(e => e.GetRedCount());
            var green = Draws.Where(e => e.GetGreenCount() > 0).Max(e => e.GetGreenCount());
            var blue = Draws.Where(e => e.GetBlueCount() > 0).Max(e => e.GetBlueCount());
            return red * green * blue;
        }
    }
}
