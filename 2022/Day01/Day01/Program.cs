internal class Program
{
    static int currentMax;
    static int currentSecondMax;
    static int currentThirdMax;

    private static void Main(string[] args)
    {
        var current = 0;
        foreach (var line in File.ReadLines("../../../Input.txt"))
        {
            if (string.IsNullOrEmpty(line))
            {
                SetCurrentMax(current);
                current = 0;
                continue;
            }
            current += int.Parse(line);
        }

        Console.WriteLine(currentMax);
        Console.WriteLine(currentSecondMax);
        Console.WriteLine(currentThirdMax);
        Console.WriteLine(currentMax + currentSecondMax + currentThirdMax);
    }

    private static void SetCurrentMax(int value)
    {
        if(value > currentMax)
        {
            currentThirdMax = currentSecondMax;
            currentSecondMax = currentMax;
            currentMax = value;
        }
        else if (value < currentMax && value > currentSecondMax)
        {
            currentThirdMax = currentSecondMax;
            currentSecondMax = value;
        }
        else if(value < currentSecondMax && value > currentThirdMax)
        {
            currentThirdMax = value;
        }
    }
}