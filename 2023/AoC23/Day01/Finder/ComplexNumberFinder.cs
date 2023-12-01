using System.Text;

namespace Day01.Finder
{
    internal class ComplexNumberFinder : INumberFinder
    {
        private readonly Dictionary<string, int> possibleNumbers;

        public ComplexNumberFinder(Dictionary<string, int> possibleNumbers)
        {
            this.possibleNumbers = possibleNumbers ?? throw new ArgumentNullException(nameof(possibleNumbers));
        }

        public string FindNumber(string line)
        {
            var builder = new StringBuilder();
            foreach (char c in line)
            {
                var current = c.ToString();
                if (int.TryParse(current, out var _))
                {
                    return current;
                }
                builder.Append(c);
                if (IsNumber(builder.ToString(), out var res))
                {
                    return res;
                }
            }
            return string.Empty;
        }

        private bool IsNumber(string line, out string res)
        {
            res = string.Empty;
            if (possibleNumbers.TryGetValue(line, out var number))
            {
                res = number.ToString();
                return true;
            }
            else
            {
                var possible = possibleNumbers.FirstOrDefault(e => line.Contains(e.Key));
                if (possible.Key != null)
                {
                    res = possible.Value.ToString();
                    return true;
                }
            }
            return false;
        }
    }
}
