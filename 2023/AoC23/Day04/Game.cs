namespace Day04;

internal class Game
{
    private readonly IReadOnlyCollection<Card> cards;

    public Game(IReadOnlyCollection<Card> cards)
    {
        this.cards = cards ?? throw new ArgumentNullException(nameof(cards));
    }

    public double CalcWorth() => cards.Sum(e => e.CalcWorth());

    public double CalcCountOfCards()
    {
        var cardCounts = cards.ToDictionary(e => e.Id, v => 1);
        for (int i = 0; i < cards.Count; i++)
        {
            var card = cards.ElementAt(i);
            var count = card.CalcWinCount();
            for (int k = 1; k <= count; k++)
            {
                var idOfNextCard = k + card.Id;
                if (idOfNextCard <= cardCounts.Count)
                {
                    cardCounts[idOfNextCard] += cardCounts[card.Id];
                }
            }
        }

        return cardCounts.Sum(e => e.Value);
    }
}
