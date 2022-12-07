internal class Program
{
    private static int sequenceInCounter;
    private static int overlappingCounter; 
    private static void Main(string[] args)
    {
        foreach(var line in File.ReadAllLines("../../../Input.txt"))
        {
            var comparts = line.Split(',');
            var first = Parse(comparts[0]);
            var second = Parse(comparts[1]);
            if (SequenceIn(first, second))
            {
                sequenceInCounter++;
                overlappingCounter++;
            }
            else if (Overlapping(first, second))
                overlappingCounter++;
        }
        Console.WriteLine(sequenceInCounter);
        Console.WriteLine(overlappingCounter);
    }

    private static bool Overlapping((int start, int last) first, (int start, int last) second)
        => (first.start >= second.start && first.start <= second.last) || 
        (second.start >= first.start && second.start <= first.last);

    private static bool SequenceIn((int start, int last) first, (int start, int last) second)
        => (first.start >= second.start && first.last <= second.last) ||
        (second.start >= first.start && second.last <= first.last);

    private static (int start, int last) Parse(string input)
    {
        var minus = input.IndexOf('-');
        var first = int.Parse(input.Substring(0, minus));
        var second = int.Parse(input.Substring(minus + 1));
        return (first,second);
    }
}