using System;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Betting round\n");

            List<string> players = new List<string> { "Player One", "Player Two", "Player Three", "Player Four" };
            List<int> bets = new List<int>(); // store bets
            bool[] hasBet = new bool[players.Count]; // track if a player has ever place a bet
            int passCount = 0;
            bool bettingRoundEnded = false;
            //foreach (var bet in hasBet) { Console.WriteLine(bet); }

            while (!bettingRoundEnded)
            {
                for (int i = 0; i < (players.Count); i++)
                {
                    if (bets.Count > i && bets[i] == -1)
                    { continue; }

                    Console.WriteLine($"{players[i]}, enter a bet (between 50-100, intervals of 5) or 'pass': ");
                    string betInput = Console.ReadLine().ToLower();

                    if (betInput == "pass")
                    {
                        Console.WriteLine($"{players[i]} passed\n");
                        if (bets.Count <= i)
                            bets.Add(-1);
                        else
                            bets[i] = -1;
                        passCount++;
                    }
                    else if (int.TryParse(betInput, out int bet))
                    {
                        if (bet >= 50 && bet <= 100 && bet % 5 == 0 && !bets.Contains(bet))
                        {
                            if (bets.Count <= i)
                            {
                                bets.Add(bet);
                                hasBet[i] = true;
                            }
                            else
                            {
                                bets[i] = bet;
                                hasBet[i] = true;
                            }
                            Console.WriteLine();
                            
                            // Check if the bet is 100
                            if (bet == 100)
                            {
                                bettingRoundEnded = true;

                                for (int j = i + 1; j < players.Count; j++)
                                {
                                    if (!hasBet[j])
                                    {
                                        if (bets.Count <= j)
                                        {
                                            bets.Add(-1);
                                        }
                                        else
                                            bets[j] = -1;
                                    }
                                }
                                break;
                            }

                        }
                        else
                        {
                            Console.WriteLine("Invalid bet");
                            i--;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                        i--;
                    }

                    if (passCount >= 3)
                    {
                        Console.WriteLine("Betting round ends");
                        // inserting default bet of 50 to player 4 since all prior players passed.
                        if (bets.Count == 4)
                        {
                            bettingRoundEnded = true;
                            break;
                        }
                        else
                        {
                            bets.Add(50);
                            hasBet[i + 1] = true;
                            bettingRoundEnded = true;
                            break;
                        }
                    }
                }
            }

            // showing betting results
            Console.WriteLine("\nBetting round complete, here are the results:");
            for (int i = 0; i < players.Count; i++)
            {
                string result;
                if (bets[i] == -1)
                {
                    result = hasBet[i] ? "Passed after betting" : "Passed";
                }
                else 
                {
                    result = $"Bet {bets[i]}";
                }
                Console.WriteLine($"{players[i]} : {result}");
            }

            Console.ReadKey();
        }
    }
}
