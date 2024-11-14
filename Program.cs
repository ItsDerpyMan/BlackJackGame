using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace BlackJackGame;

class Program
{
    // Initializing the decks
    static Game game = new Game();

    // Initialize the player and dealer
    static Player player = new Player();
    static Dealer dealer = new Dealer();

    //Game state    running or not
    static bool gameloop = Game.state;

    //Initializing the StringBuilder
    static StringBuilder str = new StringBuilder();

    //Initializing the Input class
    static Input inp = new Input(gameloop);

    static void Main()
    {
        //window settings
        int width = 50;
        int height = 80;
        Console.Title = String.Format("BlackJack Game --- W:{0} H:{1}", width, height);
        Console.CursorVisible = false;

        //Starting a new thread
        inp.

        // Game loop
        while (true)
        {
            //Game logic
            Start();
        }
    }
    static void Start()
    {
        //new game
        //initialing draw
        //todo
        //Place bets
        Game.PlaceBets(key); //Getting key input from the thread
        if (gameloop == true)
        {
            Game.dealing(player, dealer);
            Game.dealing(player, dealer);
        }
        while (gameloop == true)
        {
            //Hit
            if (OnKey(Keys.Hit))
            {
                //Gives a card to the player from the deck
                player.Add(Game.get_card(Game.deck));
                // Checks if the player card sum excide the 21 sum

            }

            //Split
            //stop
        }
    }
}
