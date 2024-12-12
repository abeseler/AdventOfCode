namespace AdventOfCode.Solutions;

internal sealed class Day07_Part2 : PuzzleSolution
{
    private const string DAY = "07";
    public static string FileName { get; } = $"Data/{DAY}.input";
    public static string TestFileName { get; } = $"Data/{DAY}.sample";
    public static string TestOutputExpected { get; } = "5905";

    private static readonly CardHandComparer s_comparer = new();

    public static string Solve(StreamReader reader)
    {
        var hands = new List<CardHand>();
        while (reader.ReadLine() is { } line)
        {
            var parts = line.Split(' ').Select(s => s.Trim(',')).ToArray();
            hands.Add(new CardHand(parts[0], int.Parse(parts[1])));
        }

        var totalWinnings = 0;
        foreach (var (index, hand) in hands.Order(s_comparer).Index())
        {
            totalWinnings += (index + 1) * hand.Bid;
        }

        return totalWinnings.ToString();
    }

    private sealed record CardHand
    {
        public CardHand(string hand, int bid)
        {
            Bid = bid;
            Hand = hand;

            var cardCounts = new Dictionary<CardValue, int>();

            foreach (var ch in hand)
            {
                var chValue = ch switch
                {
                    'J' => CardValue.Joker,
                    '2' => CardValue.Two,
                    '3' => CardValue.Three,
                    '4' => CardValue.Four,
                    '5' => CardValue.Five,
                    '6' => CardValue.Six,
                    '7' => CardValue.Seven,
                    '8' => CardValue.Eight,
                    '9' => CardValue.Nine,
                    'T' => CardValue.Ten,
                    'Q' => CardValue.Queen,
                    'K' => CardValue.King,
                    'A' => CardValue.Ace,
                    _ => throw new ArgumentException("Invalid card value")
                };

                Cards.Add(chValue);

                if (cardCounts.ContainsKey(chValue))
                {
                    cardCounts[chValue]++;
                }
                else
                {
                    cardCounts.Add(chValue, 1);
                }
            }

            var jokerCount = cardCounts.GetValueOrDefault(CardValue.Joker, 0);
            if (jokerCount == 5)
            {
                (Type, HighCard) = (HandType.FiveOfAKind, CardValue.Joker);
                return;
            }

            var (value, count) = cardCounts.Where(c => c.Key != CardValue.Joker)
                                            .OrderByDescending(c => c.Value)
                                            .ThenByDescending(c => c.Key)
                                            .First();

            (Type, HighCard) = (count, jokerCount) switch
            {
                (4, 1) => (HandType.FiveOfAKind, value),
                (4, 0) => (HandType.FourOfAKind, value),
                (3, 2) => (HandType.FiveOfAKind, value),
                (3, 1) => (HandType.FourOfAKind, value),
                (3, 0) when cardCounts.Count == 2 => (HandType.FullHouse, value),
                (3, 0) => (HandType.ThreeOfAKind, value),
                (2, 3) => (HandType.FiveOfAKind, value),
                (2, 2) => (HandType.FourOfAKind, value),
                (2, 1) when cardCounts.Count == 3 => (HandType.FullHouse, value),
                (2, 1) => (HandType.ThreeOfAKind, value),
                (2, 0) when cardCounts.Count == 3 => (HandType.TwoPair, value),
                (2, 0) => (HandType.OnePair, value),
                (1, 4) => (HandType.FiveOfAKind, value),
                (1, 3) => (HandType.FourOfAKind, value),
                (1, 2) => (HandType.ThreeOfAKind, value),
                (1, 1) => (HandType.OnePair, value),
                _ => (HandType.HighCard, value)
            };
        }

        public string Hand { get; }
        public List<CardValue> Cards { get; } = [];
        public int Bid { get; }
        public CardValue HighCard { get; }
        public HandType Type { get; }
    }

    private sealed class CardHandComparer : IComparer<CardHand>
    {
        public int Compare(CardHand? x, CardHand? y)
        {
            if (x is null || y is null)
            {
                return 0;
            }
            if (x.Type != y.Type)
            {
                return x.Type.CompareTo(y.Type);
            }

            for (var i = 0; i < x.Cards.Count; i++)
            {
                if (x.Cards[i] != y.Cards[i])
                {
                    return x.Cards[i].CompareTo(y.Cards[i]);
                }
            }
            return 0;
        }
    }

    private enum HandType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }

    private enum CardValue
    {
        Joker = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Queen = 12,
        King = 13,
        Ace = 14
    }
}
