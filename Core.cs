using System;
using System.Collections;
using System.Collections.Generic;

namespace BlackJackGame;

public class Game
{
    private static Random rand = new Random();
    public static Deck? deck;
    public static int bid;
    public static int placedbet;
    public static bool state;

    public Game()
    {
        //Initializing the deck
        deck = new Deck();

        // Setting up the starting capital
        bid = 10;

        //game loop
        state = true;

    }
    public static void PlaceBets()
    {
        if (!I.OnKey(I.Keys.Bid)) { state = false; }
        bool loop = true;
        do
        {
            MakeBets(loop);
        } while (loop == true);
    }

    public static void MakeBets(bool loop)
    {
        if (Player.money < 10) { state = false; }
        if (I.OnKey(I.Keys.Enter) || I.OnKey(I.Keys.Space))
        {
            if (I.OnKey(I.Keys.Left) || I.OnKey(I.Keys.Up))
            {
                if (bid > Player.money) { throw new Exception("You cant bid more than you have!"); }
                bid += 10;
            }
            else if (I.OnKey(I.Keys.Right) || I.OnKey(I.Keys.Down))
            {
                if (bid < 10) { throw new Exception("The minimum bet is 10."); }
                bid -= 10;
            }
        }
        loop = false;
        Player.money -= bid;
        placedbet = bid;
        bid = 10;
    }

    public static void dealing(Player player, Dealer dealer)
    {
        // Each player gets two cards
        player.Add(get_card(deck));
        dealer.Add(get_card(deck)); //
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
    public List<Hand> Hands;
    public static int money;

    public Player()
    {
        // Initializing the first Hand
        Hands = new List<Hand>
        {
            new Hand()
        };
        // Starting capital
        money = 500;
    }
    public void NewHand(int index)
    {
        if (index < 0 || index >= Hands.Count())
        {
            throw new ArgumentException("Invalid index usage!");
        }
        Hands[index] = new Hand();
    }

    public void Add(Card card, int index = 0)
    {
        if (index < 0 || index >= Hands.Count())
        {
            throw new ArgumentException("Invalid index usage!");
        }
        Hands[index].Add(card);
    }
    public Hand get_hand(int index = 0)
    {
        if (index < 0 || index >= Hands.Count())
        {
            throw new ArgumentException("Invalid index usage!");
        }
        return Hands[index];
    }
    public Card give(int index = 0)
    {
        if (index < 0 || index >= Hands.Count())
        {
            throw new ArgumentException("Invalid index usage!");
        }
        Card card = Hands[index].get_card_from_hand(out int pop_at);
        Hands[index].cards.RemoveAt(pop_at);
        return card;
    }
    public int sum(int index = 0)
    {
        if (index < 0 || index >= Hands.Count())
        {
            throw new ArgumentException("Invalid index usage!");
        }
        int sum = 0;
        foreach (var card in Hands[index])
        {
            if ((int)card.rank > 10) { sum += 10; }
            sum += (int)card.rank;
        }
        return sum;
    }
    public bool CanSplit(int index = 0)
    {
        if (index < 0 || index >= Hands.Count())
        {
            throw new ArgumentException("Invalid index usage!");
        }
        if (Hands[index].cards.Count() != 2)
        {
            return false;
        }
        return Hands[index].cards[0].rank == Hands[index].cards[1].rank;
    }
}

public class Dealer
{
    public Hand hand;

    public Dealer()
    {
        hand = new Hand();
    }
    public void Add(Card card)
    {
        hand.Add(card);
    }

}
//Hand type impl
public class Hand : IEnumerable<Card>
{
    public List<Card> cards { get; private set; }
    public int bet { get; private set; }

    public Hand()
    {
        //Init. a new hand
        cards = new List<Card>();
        //init the bet - later will be assigned a bet
        bet = 0;
    }

    public void Add(Card card)
    {
        cards.Add(card);
    }

    public Card get_card_from_hand(out int index)
    {
        if (cards.Count() != 2)
        {
            throw new Exception("The hand contains more than or less than two cards");
        }
        index = 0;
        return cards[index];
    }

    public IEnumerator<Card> GetEnumerator()
    {
        return cards.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public struct Card : IComparable<Card>
{
    public Suit suit { get; set; }
    public Rank rank { get; set; }

    public override string ToString()
    {
        return $"[{ToChar(rank)}{ToChar(suit)}]";  // Provide a readable output for the card
    }
    public int CompareTo(Card card)
    {
        return this.rank.CompareTo(card.rank);
    }

    public static char ToChar(Suit suit)
    {
        return suit switch
        {
            Suit.Clubs => '♣',
            Suit.Spades => '♠',
            Suit.Hearts => '♥',
            Suit.Diamonds => '♦',
            _ => '?'
        };
    }
    public static char ToChar(Rank rank)
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
            _ => (char)('0' + (int)rank)
        };
    }
} //[A♠]   [10♦]   [K♣v]   [5♥]

public enum Rank
{
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 14,
}

public enum Suit
{
    Clubs,
    Spades,
    Hearts,
    Diamonds
}
