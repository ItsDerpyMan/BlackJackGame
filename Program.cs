using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace BlackJackGame;

class Program
{

    //Initializing the StringBuilder
    static StringBuilder str = new StringBuilder();

    //Initializing the Input class
    static I inp = new I(); // fix

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
            // A simple menu where the user can start quit and set some settings.
            // The user is going to met this screen each time he opens the program or ends a game.
            // todo - menu

            //Game logic - new game
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

        bool loop = Game.state;

        //Place bets
        Game.PlaceBets();
        if (loop == true)
        {
            Game.dealing(player, dealer);
            Game.dealing(player, dealer);
        }
        while (loop == true)
        {
            //Stop
            if (I.OnKey(I.Keys.Stop) && loop == true)
            {
                loop = false; break;
            }

            //Hit
            if (I.OnKey(I.Keys.Hit) && loop == true)
            {
                //Gives a card to the player from the deck
                player.Add(Game.get_card(Game.deck));
                // Checks if the player card sum excide the 21 sum
                if (player.sum() > 21)
                {
                    loop = false;
                    break;
                }
            }

            //Split
            //If player has the same rank's card in his hand
            if (player.CanSplit() && I.OnKey(I.Keys.Split) && loop == true)
            {
                //New Hand
                player.NewHand(1);
                //Splits the cards
                player.Add(player.give(), 1);
                // gives One-One card to each hand
                player.Add(Game.get_card(Game.deck));
                player.Add(Game.get_card(Game.deck), 1);
            }
        }
        //deciding who won      Which player hands or the house
        int payout = 0;
        foreach (var hand in player.Hands)
        {
            if (hand < dealer.hand)
            {
                //get bet and divide from the player's money
            }
            if (hand == dealer.hand)
            {
                // No one gets nothing
            }
            if (hand > dealer.hand)
            {
                // This hand wins
                // player's bet will be multiplied and assigned to his money
                //
            }
        }
        //making payout to the player

        // Ending the game - the players money stay in memory
        // till the user chooses to stop playing Blackjack
        // (isnt going to start a new game)

        // The last draw is drawn out
        // The user will see if he won or lost
        // and the amount of money he won
        // On <Space> or <Enter> the user can get back to the menu
    }
}
