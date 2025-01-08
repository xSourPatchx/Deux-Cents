using System;
using System.Collections.Generic;

class BettingGame
{
    static void Main(string[] args)
    {
        int totalPlayers = 4;
        List<string> players = new List<string> { "Player 1", "Player 2", "Player 3", "Player 4" };
        List<int> bets = new List<int>(new int[totalPlayers]); // Tracks bets (-1 = not acted, 0 = passed, otherwise = bet amount)
        HashSet<int> placedBets = new HashSet<int>(); // Tracks unique bets
        int passCount = 0; // Tracks players who passed
        bool bettingEnded = false; // Flag for end of betting

        // Initialize bets to -1 (no action yet)
        for (int i = 0; i < totalPlayers; i++) bets[i] = -1;

        Console.WriteLine("Welcome to the Betting Round!");
        Console.WriteLine("Players can bet between 50 and 100 (in intervals of 5), or choose to pass.\n");

        while (!bettingEnded)
        {
            for (int currentPlayerIndex = 0; currentPlayerIndex < totalPlayers; currentPlayerIndex++)
            {
                // Skip players who have already passed
                if (bets[currentPlayerIndex] == 0) continue;

                bool validInput = false; // Flag to track valid input from the current player

                while (!validInput) // Re-prompt the current player until valid input is provided
                {
                    Console.WriteLine($"\n{players[currentPlayerIndex]}'s turn:");
                    Console.WriteLine("Enter a bet (50-100, intervals of 5) or 'pass': ");
                    string input = Console.ReadLine()?.Trim().ToLower();

                    if (input == "pass")
                    {
                        Console.WriteLine($"{players[currentPlayerIndex]} passed.");
                        bets[currentPlayerIndex] = 0; // Mark as passed
                        passCount++;
                        validInput = true; // End current player's turn

                        // End the betting round if three players have passed
                        if (passCount == 3)
                        {
                            Console.WriteLine("Three players have passed. Betting round ends.");
                            bettingEnded = true;
                        }
                    }
                    else if (int.TryParse(input, out int bet))
                    {
                        // Validate the bet
                        if (bet >= 50 && bet <= 100 && bet % 5 == 0 && !placedBets.Contains(bet))
                        {
                            Console.WriteLine($"{players[currentPlayerIndex]} bets {bet}.");
                            bets[currentPlayerIndex] = bet; // Record the bet
                            placedBets.Add(bet); // Track the bet as placed
                            validInput = true; // End current player's turn

                            // End betting round if a player bets 100
                            if (bet == 100)
                            {
                                Console.WriteLine("A player has bet 100. Betting round ends.");
                                bettingEnded = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid bet. Ensure it's between 50-100, in intervals of 5, and not already taken.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Type 'pass' or a valid bet.");
                    }
                }

                // Check if betting should end after each player's turn
                if (bettingEnded) break;
            }

            // Check if all remaining players have passed
            if (!bettingEnded && passCount == totalPlayers - 1)
            {
                Console.WriteLine("Only one player remains. Betting round ends.");
                bettingEnded = true;
            }
        }

        // Display the results
        Console.WriteLine("\nBetting Round Complete! Results:");
        for (int i = 0; i < totalPlayers; i++)
        {
            string result = bets[i] == 0 ? "Passed" : (bets[i] == -1 ? "No Action" : $"Bet {bets[i]}");
            Console.WriteLine($"{players[i]}: {result}");
        }
    }
}
