using System;
using System.Collections.Generic;

class BettingGame
{
    static void Main(string[] args)
    {
        int totalPlayers = 4;
        List<string> players = new List<string> { "Player 1", "Player 2", "Player 3", "Player 4" };
        List<int> bets = new List<int>(new int[totalPlayers]); // Tracks current bets (-1 = no action, 0 = passed, otherwise = bet amount)
        HashSet<int> placedBets = new HashSet<int>(); // Tracks unique bets
        HashSet<int> activePlayers = new HashSet<int>(); // Tracks players who can still act
        bool[] hasBet = new bool[totalPlayers]; // Tracks if a player has ever placed a bet
        int passCount = 0; // Tracks players who passed
        bool bettingEnded = false; // Flag for end of betting

        // Initialize bets and active players
        for (int i = 0; i < totalPlayers; i++)
        {
            bets[i] = -1;
            activePlayers.Add(i);
        }

        Console.WriteLine("Welcome to the Betting Round!");
        Console.WriteLine("Players can bet between 50 and 100 (in intervals of 5), or choose to pass.\n");

        while (!bettingEnded)
        {
            // Iterate through active players
            List<int> currentRoundPlayers = new List<int>(activePlayers); // Copy to avoid modification during iteration
            foreach (int currentPlayerIndex in currentRoundPlayers)
            {
                Console.WriteLine($"\n{players[currentPlayerIndex]}'s turn:");
                bool validInput = false;

                while (!validInput)
                {
                    Console.WriteLine("Enter a bet (50-100, intervals of 5) or 'pass': ");
                    string input = Console.ReadLine()?.Trim().ToLower();

                    if (input == "pass")
                    {
                        Console.WriteLine($"{players[currentPlayerIndex]} passed.");
                        bets[currentPlayerIndex] = 0; // Mark as passed
                        activePlayers.Remove(currentPlayerIndex); // Remove from active players
                        passCount++;
                        validInput = true;

                        // End the betting round if three players pass
                        if (passCount == totalPlayers - 1)
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
                            hasBet[currentPlayerIndex] = true; // Mark as having bet
                            validInput = true;

                            // End the betting round if a player bets 100
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

                // Stop iterating if the betting round has ended
                if (bettingEnded) break;
            }

            // End the betting round if all active players have acted in this round
            if (activePlayers.Count == 1)
            {
                Console.WriteLine("Only one player remains. Betting round ends.");
                bettingEnded = true;
            }
        }

        // Display the results
        Console.WriteLine("\nBetting Round Complete! Results:");
        for (int i = 0; i < totalPlayers; i++)
        {
            string result;
            if (bets[i] == 0 || bets[i] == -1)
            {
                result = hasBet[i] ? "Passed after betting" : "Passed"; // Combine "No Action" and "Passed"
            }
            else
            {
                result = $"Bet {bets[i]}";
            }

            Console.WriteLine($"{players[i]}: {result}");
        }
    }
}
