internal class Program
{
    interface IHand 
    { 
        int Value { get; }
        GameState Play(IHand other);
        IHand GetHandToGetResult(GameState result);
    }

    class Paper : IHand
    {
        public int Value => 2;

        public IHand GetHandToGetResult(GameState result) 
            => result switch
            {
                GameState.Loss => new Rock(),
                GameState.Win => new Scissor(),
                GameState.Tie => new Paper(),
                _ => throw new Exception(),
            };

        public GameState Play(IHand other)
            => other switch
            {
                Paper => GameState.Tie,
                Rock => GameState.Win,
                Scissor => GameState.Loss,
                _ => throw new Exception(),
            };
    }

    class Rock : IHand
    {
        public int Value => 1;

        public IHand GetHandToGetResult(GameState result)
            => result switch
            {
                GameState.Loss => new Scissor(),
                GameState.Win => new Paper(),
                GameState.Tie => new Rock(),
                _ => throw new Exception(),
            };

        public GameState Play(IHand other)
            => other switch
            {
                Scissor => GameState.Win,
                Rock => GameState.Tie,
                Paper => GameState.Loss,
                _ => throw new Exception(),
            };
    }

    class Scissor : IHand
    {
        public int Value => 3;

        public IHand GetHandToGetResult(GameState result)
            => result switch
            {
                GameState.Loss => new Paper(),
                GameState.Win => new Rock(),
                GameState.Tie => new Scissor(),
                _ => throw new Exception(),
            };

        public GameState Play(IHand other) 
            => other switch
            {
                Scissor => GameState.Tie,
                Rock => GameState.Loss,
                Paper => GameState.Win,
                _ => throw new Exception(),
            };
    }

    enum GameState : int
    {
        Loss = 0,
        Win = 6,
        Tie = 3
    }

    private static int firstRound;
    private static int secondRound;

    private static void Main(string[] args)
    {
        foreach(var input in File.ReadLines("../../../Input.txt"))
        {
            FirstRound(input);
            SecondRound(input);
        }

        Console.WriteLine(firstRound);
        Console.WriteLine(secondRound);
    }

    private static void FirstRound(string input)
    {
        var opponentHand = GetOpponentHand(input[0]);
        var playerHand = GetPlayerHand(input[2]);
        var gameState = playerHand.Play(opponentHand);
        firstRound += (int)gameState + playerHand.Value;
    }

    private static void SecondRound(string input) 
    {
        var expectedResult = GetExcpectedGameState(input[2]);
        var opponentHand = GetOpponentHand(input[0]);
        var playerhand = opponentHand.GetHandToGetResult(expectedResult);
        secondRound += (int)expectedResult + playerhand.Value;
    }

    private static IHand GetPlayerHand(char opponentHand)
        => opponentHand switch
        {
            'Z' => new Scissor(),
            'Y' => new Paper(),
            'X' => new Rock(),
            _ => throw new Exception(),
        };

    private static GameState GetExcpectedGameState(char input)
        => input switch
        {
            'Z' => GameState.Win,
            'Y' => GameState.Tie,
            'X' => GameState.Loss,
            _ => throw new Exception(),
        };

    private static IHand GetOpponentHand(char input)
        => input switch
        {
            'A' => new Rock(),
            'B' => new Paper(),
            'C' => new Scissor(),
            _ => throw new Exception(),
        };
}