using System;
using System.Collections.Generic;

class BettingGame
{
    static void Main(string[] args)
    {
        int totalPlayers = 4;
        List<string> players = new List<string> { "Player 1", "Player 2", "Player 3", "Player 4" };
        List<int> bets = new List<int>(new int[totalPlayers]); // Bets initialized to -1 (not yet acted)
        HashSet<int> placedBets = new HashSet<int>(); // Tracks unique bets
        int passCount = 0; // Tracks number of players who passed
        bool bettingEnded = false; // Tracks if the betting round is over

        Console.WriteLine("Welcome to the Betting Round!");
        Console.WriteLine("Players can bet between 50 and 100 (in intervals of 5), or choose to pass.\n");

        // Initialize bets to -1 for all players
        for (int i = 0; i < totalPlayers; i++)
        {
            bets[i] = -1;
        }

        while (!bettingEnded)
        {
            for (int i = 0; i < totalPlayers; i++)
            {
                // Skip players who already passed
                if (bets[i] != -1)
                    continue;

                // End the round if three players have passed
                if (passCount == 3)
                {
                    Console.WriteLine($"Three players have passed. {players[i]}'s bet defaults to 50.");
                    bets[i] = 50;
                    bettingEnded = true;
                    break;
                }

                // Automatically pass remaining players if someone bets 100
                if (bettingEnded)
                {
                    Console.WriteLine($"{players[i]} automatically passes because a player bet 100.");
                    bets[i] = -1;
                    continue;
                }

                Console.WriteLine($"\n{players[i]}'s turn:");
                Console.WriteLine("Enter a bet (50-100, intervals of 5) or 'pass': ");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "pass")
                {
                    Console.WriteLine($"{players[i]} passed.");
                    bets[i] = -1;
                    passCount++;
                }
                else if (int.TryParse(input, out int bet))
                {
                    // Validate the bet
                    if (bet >= 50 && bet <= 100 && bet % 5 == 0 && !placedBets.Contains(bet))
                    {
                        Console.WriteLine($"{players[i]} bets {bet}.");
                        bets[i] = bet;
                        placedBets.Add(bet);

                        if (bet == 100)
                        {
                            Console.WriteLine("A player has bet 100. Betting round ends, and all remaining players automatically pass.");
                            bettingEnded = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid bet. Ensure it's between 50-100, in intervals of 5, and not already taken.");
                        i--; // Retry this player's turn
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Type 'pass' or a valid bet.");
                    i--; // Retry this player's turn
                }
            }
        }

        Console.WriteLine("\nBetting Round Complete! Results:");
        for (int i = 0; i < totalPlayers; i++)
        {
            string result = bets[i] == -1 ? "Passed" : $"Bet {bets[i]}";
            Console.WriteLine($"{players[i]}: {result}");
        }
    }
}
