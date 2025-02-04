using System;
using System.Linq;
using System.Collections.Generic;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // ... (existing code for initialization, shuffling, dealing, and betting)

            /* SECTION 3: TRUMP SUIT SELECTION */

            // Determine the winning bet and player
            int maxBet = bets.Max();
            int winningPlayerIndex = bets.IndexOf(maxBet);
            string winningPlayer = players[winningPlayerIndex];

            Console.WriteLine($"\n{winningPlayer} won the bet with a bet of {maxBet}. Please choose the trump suit (clubs, diamonds, hearts, spades): ");
            string trumpSuit = Console.ReadLine().ToLower();

            while (trumpSuit != "clubs" && trumpSuit != "diamonds" && trumpSuit != "hearts" && trumpSuit != "spades")
            {
                Console.WriteLine("Invalid suit. Please choose the trump suit (clubs, diamonds, hearts, spades): ");
                trumpSuit = Console.ReadLine().ToLower();
            }

            Console.WriteLine($"\nTrump suit is {trumpSuit}.\n");

            /* SECTION 4: ROUND PLAYING */

            // Initialize variables for tracking points and tricks
            int teamOnePoints = 0;
            int teamTwoPoints = 0;
            List<Card>[] playerDecks = { playerOneDeck, playerTwoDeck, playerThreeDeck, playerFourDeck };

            // Determine the starting player (the one who won the bet)
            int currentPlayerIndex = winningPlayerIndex;

            // Play 10 tricks (since there are 40 cards and 4 players)
            for (int trick = 0; trick < 10; trick++)
            {
                Console.WriteLine($"\nTrick {trick + 1}:");

                // Initialize the trick
                List<Card> currentTrick = new List<Card>();
                string leadSuit = null;

                // Each player plays a card
                for (int i = 0; i < 4; i++)
                {
                    int playerIndex = (currentPlayerIndex + i) % 4;
                    List<Card> playerDeck = playerDecks[playerIndex];

                    Console.WriteLine($"{players[playerIndex]}, choose a card to play (enter index 0-{playerDeck.Count - 1}):");
                    for (int j = 0; j < playerDeck.Count; j++)
                    {
                        Console.WriteLine($"{j}: {playerDeck[j]}");
                    }

                    int cardIndex = -1;
                    bool validInput = false;

                    while (!validInput)
                    {
                        string input = Console.ReadLine();

                        if (int.TryParse(input, out cardIndex) && cardIndex >= 0 && cardIndex < playerDeck.Count)
                        {
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid index.");
                        }
                    }

                    Card playedCard = playerDeck[cardIndex];
                    playerDeck.RemoveAt(cardIndex);

                    // Set the lead suit if it's the first card
                    if (i == 0)
                    {
                        leadSuit = playedCard.cardSuit;
                    }

                    currentTrick.Add(playedCard);
                    Console.WriteLine($"{players[playerIndex]} played {playedCard}");
                }

                // Determine the winner of the trick
                Card winningCard = currentTrick[0];
                int winningCardIndex = 0;

                for (int i = 1; i < 4; i++)
                {
                    Card currentCard = currentTrick[i];

                    // Check if the current card is a trump card and the winning card is not
                    if (currentCard.cardSuit == trumpSuit && winningCard.cardSuit != trumpSuit)
                    {
                        winningCard = currentCard;
                        winningCardIndex = i;
                    }
                    // Check if both cards are trump cards or both are not trump cards
                    else if (currentCard.cardSuit == winningCard.cardSuit)
                    {
                        if (currentCard.cardFaceValue > winningCard.cardFaceValue)
                        {
                            winningCard = currentCard;
                            winningCardIndex = i;
                        }
                    }
                }

                // Determine the winning player and update points
                int trickWinnerIndex = (currentPlayerIndex + winningCardIndex) % 4;
                Console.WriteLine($"{players[trickWinnerIndex]} wins the trick with {winningCard}");

                // Add points to the winning team
                int trickPoints = currentTrick.Sum(card => card.cardPointValue);
                if (trickWinnerIndex == 0 || trickWinnerIndex == 2)
                {
                    teamOnePoints += trickPoints;
                }
                else
                {
                    teamTwoPoints += trickPoints;
                }

                // The winner of the trick leads the next trick
                currentPlayerIndex = trickWinnerIndex;
            }

            // End of round scoring
            Console.WriteLine("\nEnd of round scoring:");
            Console.WriteLine($"Team One (Player 1 & Player 3) points: {teamOnePoints}");
            Console.WriteLine($"Team Two (Player 2 & Player 4) points: {teamTwoPoints}");

            // Check if the betting team made their bet
            if (winningPlayerIndex == 0 || winningPlayerIndex == 2)
            {
                if (teamOnePoints >= maxBet)
                {
                    Console.WriteLine($"Team One made their bet of {maxBet} and gains {teamOnePoints} points.");
                }
                else
                {
                    Console.WriteLine($"Team One did not make their bet of {maxBet} and loses {maxBet} points.");
                }
            }
            else
            {
                if (teamTwoPoints >= maxBet)
                {
                    Console.WriteLine($"Team Two made their bet of {maxBet} and gains {teamTwoPoints} points.");
                }
                else
                {
                    Console.WriteLine($"Team Two did not make their bet of {maxBet} and loses {maxBet} points.");
                }
            }

            Console.ReadKey();
        }

        // ... (existing methods for ShuffleDeck and TrickWinner)
    }

    // ... (existing Card class)
}
