using System;
using System.Linq;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Will need below in unity, to check if card slot has a card or is empty once card are played
            //bool[] playerOneDeckAvailableCardSlot = { true, true, true, true, true, true, true, true, true, true };
            //bool[] playerTwoDeckAvailableCardSlot = { true, true, true, true, true, true, true, true, true, true };
            //bool[] playerThreeDeckAvailableCardSlot = { true, true, true, true, true, true, true, true, true, true };
            //bool[] playerFourDeckAvailableCardSlot = { true, true, true, true, true, true, true, true, true, true };


            /* SECTION 1: INITIAL CARDS, SHUFFLE AND DEAL */

            Console.WriteLine("Shuffling and dealing cards...");
            //Thread.Sleep(2000);

            // init list of objects
            List<Card> deck = new List<Card>();

            // append each card to deck
            deck.Add(new Card("5", "clubs", 1, 5)); // clubs
            deck.Add(new Card("6", "clubs", 2, 0));
            deck.Add(new Card("7", "clubs", 3, 0));
            deck.Add(new Card("8", "clubs", 4, 0));
            deck.Add(new Card("9", "clubs", 5, 0));
            deck.Add(new Card("10", "clubs", 6, 10));
            deck.Add(new Card("J", "clubs", 7, 0));
            deck.Add(new Card("Q", "clubs", 8, 0));
            deck.Add(new Card("K", "clubs", 9, 0));
            deck.Add(new Card("A", "clubs", 10, 10));
            deck.Add(new Card("5", "diamonds", 1, 5)); // diamonds
            deck.Add(new Card("6", "diamonds", 2, 0));
            deck.Add(new Card("7", "diamonds", 3, 0));
            deck.Add(new Card("8", "diamonds", 4, 0));
            deck.Add(new Card("9", "diamonds", 5, 0));
            deck.Add(new Card("10", "diamonds", 6, 10));
            deck.Add(new Card("J", "diamonds", 7, 0));
            deck.Add(new Card("Q", "diamonds", 8, 0));
            deck.Add(new Card("K", "diamonds", 9, 0));
            deck.Add(new Card("A", "diamonds", 10, 10));
            deck.Add(new Card("5", "hearts", 1, 5)); // hearts
            deck.Add(new Card("6", "hearts", 2, 0));
            deck.Add(new Card("7", "hearts", 3, 0));
            deck.Add(new Card("8", "hearts", 4, 0));
            deck.Add(new Card("9", "hearts", 5, 0));
            deck.Add(new Card("10", "hearts", 6, 10));
            deck.Add(new Card("J", "hearts", 7, 0));
            deck.Add(new Card("Q", "hearts", 8, 0));
            deck.Add(new Card("K", "hearts", 9, 0));
            deck.Add(new Card("A", "hearts", 10, 10));
            deck.Add(new Card("5", "spades", 1, 5)); // spades
            deck.Add(new Card("6", "spades", 2, 0));
            deck.Add(new Card("7", "spades", 3, 0));
            deck.Add(new Card("8", "spades", 4, 0));
            deck.Add(new Card("9", "spades", 5, 0));
            deck.Add(new Card("10", "spades", 6, 10));
            deck.Add(new Card("J", "spades", 7, 0));
            deck.Add(new Card("Q", "spades", 8, 0));
            deck.Add(new Card("K", "spades", 9, 0));
            deck.Add(new Card("A", "spades", 10, 10));

            // shuffle deck 3 times
            ShuffleDeck();
            ShuffleDeck();
            ShuffleDeck();

            // create 4 list for all 4 players 
            List<Card> playerOneDeck = new List<Card>();
            List<Card> playerTwoDeck = new List<Card>();
            List<Card> playerThreeDeck = new List<Card>();
            List<Card> playerFourDeck = new List<Card>();

            for (int i = 0; i < (deck.Count); i += 4)
            {
                playerOneDeck.Add(deck[i]);
                playerTwoDeck.Add(deck[i + 1]);
                playerThreeDeck.Add(deck[i + 2]);
                playerFourDeck.Add(deck[i + 3]);
            }

            Console.WriteLine("\n#########################\n");

            Console.WriteLine("Player one's hand:\n");
            foreach (Card player in playerOneDeck) { Console.WriteLine(player); } // disp playerOneDeck hand

            //Thread.Sleep(1000);

            Console.WriteLine("\nPlayer two's hand:\n");
            foreach (Card player in playerTwoDeck) { Console.WriteLine(player); } // disp playerTwoDeck hand

            //Thread.Sleep(1000);

            Console.WriteLine("\nPlayer three's hand:\n");
            foreach (Card player in playerThreeDeck) { Console.WriteLine(player); } // disp playerThreeDeck hand

            //Thread.Sleep(1000);

            Console.WriteLine("\nPlayer four's hand:\n");
            foreach (Card player in playerFourDeck) { Console.WriteLine(player); } // disp playerFourDeck hand

            Console.WriteLine("\n#########################\n");


            /* SECTION 2: BETTING ROUND */ // should make this a method

            Console.WriteLine("Betting round\n");

            List<string> players = new List<string> { "Player One", "Player Two", "Player Three", "Player Four" };
            List<int> bets = new List<int>(); // store bets
            bool[] hasBet = new bool[players.Count]; // track if a player has ever placed a bet
            bool[] hasPassed = new bool[players.Count]; // track if a player has passed
            //int passCount = 0;
            bool bettingRoundEnded = false;

            while (!bettingRoundEnded)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (hasPassed[i])
                    {
                        continue; // Skip players who have already passed
                    }

                    Console.WriteLine($"{players[i]}, enter a bet (between 50-100, intervals of 5) or 'pass': ");
                    string betInput = Console.ReadLine().ToLower();

                    if (betInput == "pass")
                    {
                        Console.WriteLine($"{players[i]} passed\n");
                        hasPassed[i] = true;
                        if (bets.Count <= i)
                            bets.Add(-1);
                        else
                            bets[i] = -1;
                    }
                    else if (int.TryParse(betInput, out int bet))
                    {
                        if (bet >= 50 && bet <= 100 && bet % 5 == 0 && !bets.Contains(bet))
                        {
                            if (bets.Count <= i)
                            {
                                bets.Add(bet);
                            }
                            else
                            {
                                bets[i] = bet;  
                            }
                            hasBet[i] = true;
                            Console.WriteLine();

                            // Check if the bet is 100
                            if (bet == 100)
                            {
                                bettingRoundEnded = true; // End the betting round immediately
                                Console.WriteLine($"{players[i]} bet 100. Betting round ends.\n");

                                // Set all subsequent players who haven't placed a bet to -1
                                for (int j = i + 1; j < players.Count; j++)
                                {
                                    if (j != i && !hasPassed[j])
                                    {
                                        hasPassed[j] = true;
                                        if (bets.Count <= j)
                                            bets.Add(-1);
                                        else
                                            bets[j] = -1;
                                        Console.WriteLine($"{players[j]} automatically passes.\n");
                                    }
                                }
                                break; // Exit the for loop
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

                    // End the betting round if 3 players have passed
                    if (hasPassed.Count(p => p) >= 3)
                    {
                        Console.WriteLine("Betting round ends");
                        // Inserting default bet of 50 to player 4 if all prior players passed
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

            // Showing betting results
            Console.WriteLine("\nBetting round complete, here are the results:");
            for (int i = 0; i < players.Count; i++)
            {
                string result;
                if (hasPassed[i])
                {
                    result = hasBet[i] ? "Passed after betting" : "Passed";
                }
                else
                {
                    result = $"Bet {bets[i]}";
                }
                Console.WriteLine($"{players[i]} : {result}");
            }


            /* SECTION 3: TRUMP SUIT SELECTION */


            // index of highest bidder
            Console.WriteLine();
            var highestBidder = bets.Max();
            int indexOfHighestBidder = bets.IndexOf(highestBidder);
            Console.WriteLine($"{players[indexOfHighestBidder]} won the bid.");
            Console.WriteLine();

            Console.WriteLine($"{players[indexOfHighestBidder]}, please choose a trump suit. (enter \"clubs\", \"diamonds\", \"hearts\", \"spades\")");
            string trumpSuit = Console.ReadLine().ToLower();

            while (trumpSuit != "clubs" && trumpSuit != "diamonds" && trumpSuit != "hearts" && trumpSuit != "spades")
            {
                Console.WriteLine();
                Console.WriteLine($"{trumpSuit} is an invalid input, please try again.");
                trumpSuit = Console.ReadLine().ToLower();
            }

            Console.WriteLine($"Trump suit is {trumpSuit}.");
            Console.WriteLine();

            
            /* SECTION 4: ROUND PLAYING */

            int teamOnePoints = 0;
            int teamTwoPoints = 0;
            List<Card>[] playerDecks = { playerOneDeck, playerTwoDeck, playerThreeDeck, playerFourDeck }; // array of lists

            int currentPlayerIndex = indexOfHighestBidder; //set current index to index of player who won the bet
            

            // for the first trick
            for (int trick = 0; trick < 10; trick ++)
            {
                Console.WriteLine();
                Console.WriteLine($"Trick #{trick + 1}:");

                List<Card> currentTrick = new List<Card> (); // empty list to hold tricks
                string leadingSuit = null;

                for (int i = 0; i < 4; i++)
                {
                    int playerIndex = (currentPlayerIndex + i) % 4; // ensuring player who won the bet goes first
                    List<Card> playerDeck = playerDecks[playerIndex];

                    // adding loop to validate user input
                    int cardIndex = -1; // initializing invalid input
                    bool validInput = false;
                    while (!validInput)
                    {
                        Console.WriteLine($"{players[playerIndex]}, choose a card to play (enter index 0-{playerDeck.Count - 1}):");
                        for (int j = 0; j < playerDeck.Count; j++)
                        {
                            Console.WriteLine($"{j} : {playerDeck[j]}");
                        }

                        string Input = Console.ReadLine();
                        if (int.TryParse(Input, out cardIndex) && cardIndex < playerDeck.Count && cardIndex >= 0)
                        {                        
                            if (i == 0)
                            {
                                validInput = true;
                            }
                            else
                            {
                                if (playerDeck[cardIndex].cardSuit != leadingSuit && playerDeck.Any(card => card.cardSuit == leadingSuit))
                                {
                                    Console.WriteLine();
                                    Console.WriteLine($"You must play a card of ({leadingSuit}) since its in your deck, try again.");
                                    Console.WriteLine();
                                }
                                else
                                {
                                    validInput = true;
                                }

                            }
                        }
                        else
                        {
                            Console.WriteLine($"{cardIndex} is an invalid input, please try again.");
                        }
                    }

                    Card playedCard = playerDeck[cardIndex];
                    playerDeck.RemoveAt(cardIndex);

                    if (i == 0)
                    {
                        leadingSuit =  playedCard.cardSuit;
                        Console.WriteLine($"leading suit: {leadingSuit}");
                    }

                    currentTrick.Add(playedCard);
                    Console.WriteLine($"{players[playerIndex]} played {playedCard}");
                    Console.WriteLine();

                    // left off here
                    // need to find out who won the trick, so that the winner of the trick plays next


                }

            }
            /*
            // for trick 1 to 9
            for (int trick = 1; trick < 10; trick ++)
            {
                Console.WriteLine();
                Console.WriteLine($"Trick #{trick + 1}:");

                List<Card> currentTrick = new List<Card> (); // empty list to hold tricks
                string leadingSuit = null;

                for (int i = 0; i < 4; i++)
                {
                    int playerIndex = (currentPlayerIndex + i) % 4; // ensuring player who won the bet goes first
                    List<Card> playerDeck = playerDecks[playerIndex];

                    // adding loop to validate user input
                    int cardIndex = -1; // initializing invalid input
                    bool validInput = false;
                    while (!validInput)
                    {
                        Console.WriteLine($"{players[playerIndex]}, choose a card to play (enter index 0-{playerDeck.Count - 1}):");
                        for (int j = 0; j < playerDeck.Count; j++)
                        {
                            Console.WriteLine($"{j} : {playerDeck[j]}");
                        }
                        string Input = Console.ReadLine();
                        if (int.TryParse(Input, out cardIndex) && cardIndex < playerDeck.Count && cardIndex >= 0)
                        {
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine($"{cardIndex} is an invalid input, please try again.");
                        }
                    }

                    Card playedCard = playerDeck[cardIndex];
                    playerDeck.RemoveAt(cardIndex);

                    if (i == 0)
                    {
                        leadingSuit =  playedCard.cardSuit;
                    }

                    currentTrick.Add(playedCard);
                    Console.WriteLine($"{players[playerIndex]} played {playedCard}");
                    Console.WriteLine();
                    // left off here
                    // need to find out who won the trick

                }
             */




            // ******* seperate into seperate .cs file per object!!!!!
            // Next steps
            // -initiate team 1 and team 2 discard piles/points
            // -should integrate pop method when card is played
            // -push method when card are discarded
            // -coint points at the end



            //// test below
            //List<Card> testTrick = new List<Card>();
            //for (int i = 0; i < 4; i++)
            //{
            //    testTrick.Add(deck[i]);
            //}

            //foreach (Card card in testTrick) { Console.WriteLine(card); }

            //Console.WriteLine("\n#########################\n");

            //// call method to find winner of trick
            //Card cardwinner = TrickWinner(testTrick[0], testTrick[1], testTrick[2], testTrick[3]);
            //Console.WriteLine(cardwinner);

            //Console.WriteLine("\n#########################\n");


            // method to shuffle deck
            void ShuffleDeck()
            {
                int n = 0;
                while (n < deck.Count)
                {
                    Random random = new Random();
                    int randomCardIndex = random.Next(n, deck.Count);
                    Card temp = deck[randomCardIndex];
                    deck[randomCardIndex] = deck[n];
                    deck[n] = temp;
                    n++;
                }
            }

            // method to select winner of trick (not yet used)
            Card TrickWinner(Card playerOneDeckCard, Card playerTwoDeckCard, Card playerThreeDeckCard, Card playerFourDeckCard)
            {
                List<Card> trick = new List<Card>();
                trick.Add(playerOneDeckCard);
                trick.Add(playerTwoDeckCard);
                trick.Add(playerThreeDeckCard);
                trick.Add(playerFourDeckCard);
                int maxVal = trick.Max(x => x.cardFaceValue);
                Card maxCard = trick.First(x => x.cardFaceValue == maxVal);
                return maxCard;
            }

            Console.ReadKey();
        }
    }

    class Card
    {
        public string cardFace; // 5, 6, 7, 8, 9, 10, J, Q, K, A
        public string cardSuit; // clubs, diamonds, hearts, spades
        public int cardFaceValue; // 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
        public int cardPointValue; // 0, 5, 10
        public Card(string cardFace, string cardSuit, int cardFaceValue, int cardPointValue)
        {
            this.cardFace = cardFace;
            this.cardSuit = cardSuit;
            this.cardFaceValue = cardFaceValue;
            this.cardPointValue = cardPointValue;
        }

        public override string ToString()
        {
            return cardFace + "(" + cardFaceValue + ")" + " of " + cardSuit + " = " + cardPointValue + " points";
        }
    }
}
