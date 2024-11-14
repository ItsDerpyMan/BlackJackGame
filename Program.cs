using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace BlackJackGame;

class Program
{
    //Game state    running or not
    static bool gameloop = Game.state;

    //Initializing the StringBuilder
    static StringBuilder str = new StringBuilder();

    //Initializing the Input class
    static I inp = new I(gameloop);

    static void Main()
    {
        //window settings
        int width = 50;
        int height = 80;
        Console.Title = String.Format("BlackJack Game --- W:{0} H:{1}", width, height);
        Console.CursorVisible = false;

        // Game loop
        while (true)
        {
            //Game logic
            Start();
        }
    }
    static void Start()
    {
        // Initializing the decks
        Game game = new Game();
        // Initialize the player and dealer
        Player player = new Player();
        Dealer dealer = new Dealer();

        //new game
        //initialing draw
        //todo

        //Place bets
        Game.PlaceBets();
        if (gameloop == true)
        {
            Game.dealing(player, dealer);
            Game.dealing(player, dealer);
        }
        while (gameloop == true)
        {
            //Player
            //Hit
            if (I.OnKey(I.Keys.Hit))
            {
                //Gives a card to the player from the deck
                player.Add(Game.get_card(Game.deck));
                // Checks if the player card sum excide the 21 sum
                if (player.sum() > 21)
                {
                    gameloop = false;
                    break;
                }
            }
            //Split
            //If player has the same rank's card in his hand
            if (player.CanSplit())
            {
                Player player2 = new Player();
                player2.Add(player.give());
                player.Add(Game.get_card(Game.deck));
                player2.Add(Game.get_card(Game.deck));
            }
            //stop
        }
    }
}
