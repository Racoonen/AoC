// See https://aka.ms/new-console-template for more information
using Day04;

var parser = new Parser();
var game = parser.Parse(File.ReadAllLines("./Input.txt"));

Console.WriteLine("First Part.");
Console.WriteLine($"Worth of game is {game.CalcWorth()}");


Console.WriteLine("Second Part.");
Console.WriteLine($"Count of Cards of game is {game.CalcCountOfCards()}");