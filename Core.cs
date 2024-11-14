namespace BlackJackGame;

public class Game
{
    private static Random rand = new Random();
    public static Deck? deck;
    public static int money;
    public static int bid;
    public static int placedbet;
    public static bool state;

    public Game()
    {
        //Initializing the deck
        deck = new Deck();
        // Setting up the starting capital
        money = 500;
        bid = 10;
        //game loop
        state = true;

    }
    public static void PlaceBets(Keys key)
    {
        if (key != Keys.Bid) { state = false; }
        bool loop = true;
        do
        {
            MakeBets(loop);
        } while (loop == true);
    }
    public static void MakeBets(bool loop)
    {
        if (money < 10) { state = false; }
        if (key != Keys.Enter || key != Keys.Space)
        {
            if (key == Keys.Left || key == Keys.Up)
            {
                if (bid > money) { throw new Exception("You cant bid more than you have!"); }
                bid += 10;
            }
            else if (key == Keys.Right || key == Keys.Down)
            {
                if (bid < 10) { throw new Exception("The minimum bet is 10."); }
                bid -= 10;
            }
        }
        loop = false;
        money -= bid;
        placedbet = bid;
        bid = 10;
    }

    public static void dealing(Player player, Dealer dealer)
    {
        // Each player gets two cards
        player.Add(get_card(deck));
        dealer.Add(get_card(deck));
    }

    public static Card get_card(Deck? deck)
    {
        if (deck == null) { throw new InvalidOperationException("Deck is not initialized."); }
        int size = deck.CountTheDeck();
        if (size == 0)
        {
            throw new InvalidOperationException("No more cards in the deck.");
        }

        int index = rand.Next(0, size);
        Card card = deck.GetValueFromTheDeck(index);
        deck.RemoveAt(index);
        return card;
    }
}
public class Deck
{
    private List<Card> deck;

    public Deck()
    {
        deck = new List<Card>();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                Card card = new Card() { suit = suit, rank = rank };
                deck.Add(card);
            }
        }
    }
    public void RemoveAt(int index)
    {
        deck.RemoveAt(index);
    }
    public Card GetValueFromTheDeck(int index)
    {
        return deck[index];
    }

    public int CountTheDeck()
    {
        return deck.Count;
    }
}
public class Player
{
    private List<Card> hand;

    public Player()
    {
        hand = new List<Card>();
    }
    public void Add(Card card)
    {
        hand.Add(card);
    }
    public List<Card> get_hand()
    {
        return hand;
    }
}

public class Dealer
{
    private List<Card> hand;

    public Dealer()
    {
        hand = new List<Card>();
    }
    public void Add(Card card)
    {
        hand.Add(card);
    }
    public List<Card> get_hand()
    {
        return hand;
    }
}

public struct Card
{
    public Suit suit;
    public Rank rank;

    public override string ToString()
    {
        return $"[{ToChar(rank)}{ToChar(suit)}]";  // Provide a readable output for the card
    }

    public char ToChar(Suit suit)
    {
        return suit switch
        {
            Suit.Clubs => '♣',
            Suit.Spades => '♠',
            Suit.Hearts => '♥',
            Suit.Diamonds => '♦',
            _ => throw new ArgumentOutOfRangeException(nameof(suit), "Invalid suit value")
        };
    }
    public char ToChar(Rank rank)
    {
        return rank switch
        {
            Rank.Two => '2',
            Rank.Three => '3',
            Rank.Four => '4',
            Rank.Five => '5',
            Rank.Six => '6',
            Rank.Seven => '7',
            Rank.Eight => '8',
            Rank.Nine => '9',
            Rank.Ten => 'T',
            Rank.Jack => 'J',
            Rank.Queen => 'Q',
            Rank.King => 'K',
            Rank.Ace => 'A',
            _ => throw new ArgumentOutOfRangeException(nameof(rank), "Invalid rank value")
        };
    }
} //[A♠]   [10♦]   [K♣v]   [5♥]

public enum Rank
{
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace,
}

public enum Suit
{
    Clubs,
    Spades,
    Hearts,
    Diamonds
}
