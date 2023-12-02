using Day01;

Console.WriteLine("First Part.");
var firstgame = new Riddle(File.ReadAllLines("Input.txt").Select(e => new Line(e, true)).ToArray());
Console.WriteLine($"Result is {firstgame.Solve()}");

Console.WriteLine("Second Part.");
var scondgame = new Riddle(File.ReadAllLines("Input.txt").Select(e => new Line(e, false)).ToArray());
Console.WriteLine($"Result is {scondgame.Solve()}");