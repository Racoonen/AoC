using Day02;

Console.WriteLine("First Part.");
var parser = new Parser();
var games = File.ReadAllLines("FirstInput.txt").Select(parser.ParseLine).ToList();
var possiblegames = games.Where(e => e.IsPossible(12, 13, 14)).ToList();
foreach (var possible in possiblegames)
{
    Console.WriteLine($"Game wih ID {possible.Id} is possible");
}
Console.WriteLine($"Sum of Ids is {possiblegames.Sum(e=> e.Id)}");


Console.WriteLine("Second Part.");
foreach (var possible in games)
{
    Console.WriteLine($"Game wih ID {possible.Id} has the power {possible.GetPower()}");
}
Console.WriteLine($"Sum of Power is {games.Sum(e => e.GetPower())}");