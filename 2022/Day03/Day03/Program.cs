using System.Linq;

internal class Program
{

    private static int TwoElfGroup = 0;
    private static int ThreeElfGroup = 0;

    private static void Main(string[] args)
    {
        foreach(var line in File.ReadAllLines("../../../Input.txt"))
        {
            DoTwoElfGroup(line);
            DoThreeGroup(line);
        }

        Console.WriteLine(TwoElfGroup);
        Console.WriteLine(ThreeElfGroup);
    }

    private static int threeElfGroupCounter = 0;
    private static string firstLine;
    private static string secondLine;

    private static void DoThreeGroup(string line)
    {
        switch(threeElfGroupCounter % 3)
        {
            case 0: firstLine = line; break;   
            case 1: secondLine = line;break;
            case 2: ThreeElfGroup += GetCommonItem(firstLine, secondLine, line); break;
        }
        threeElfGroupCounter++;
    }

    private static void DoTwoElfGroup(string line)
    {
        var lines = line.Chunk(line.Length / 2);
        var first = new string(lines.ElementAt(0));
        var second = new string(lines.ElementAt(1));

        TwoElfGroup += GetCommonItem(first, second);
    }

    private static int GetCommonItem(params string[] items)
    {
        var result = items[0];
        foreach(var line in items.Skip(1))
        {
            result = new string(result.Intersect(line).ToArray());
        }
        return GetValue(result[0]);
    }

    private static int GetValue(char item) =>
        item switch
        {
            >= 'a' and <= 'z' => item - 96,
            >= 'A' and <= 'Z' => item - 38,
            _ => throw new Exception(),
        };
}