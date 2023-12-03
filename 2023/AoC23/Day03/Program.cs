// See https://aka.ms/new-console-template for more information
using Day03;

var parser = new Parser();
var matrix = parser.Parse(File.ReadAllLines("./Input.txt"));

Console.WriteLine("First Part.");
Console.WriteLine($"Sum of valid Parts is {matrix.CalcSumOfValidEntries()}");


Console.WriteLine("Second Part.");
Console.WriteLine($"Sum of Gear Ratio is {matrix.CalcGearRatio()}");