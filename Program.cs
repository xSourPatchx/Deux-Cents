using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Will need below in unity, to check if card slot has a card or is empty once card are played
            //bool[] playerOneAvailableCardSlot = { true, true, true, true, true, true, true, true, true, true };
            //bool[] playerTwoAvailableCardSlot = { true, true, true, true, true, true, true, true, true, true };
            //bool[] playerThreeAvailableCardSlot = { true, true, true, true, true, true, true, true, true, true };
            //bool[] playerFourAvailableCardSlot = { true, true, true, true, true, true, true, true, true, true };


            /* SECTION 1: INITIAL CARDS, SHUFFLE AND DEAL */

            Console.WriteLine("Shuffling and dealing cards...");
            Thread.Sleep(2000);

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
            List<Card> playerOne = new List<Card>();
            List<Card> playerTwo = new List<Card>();
            List<Card> playerThree = new List<Card>();
            List<Card> playerFour = new List<Card>();

            for (int i = 0; i < (deck.Count); i += 4)
            {
                playerOne.Add(deck[i]);
                playerTwo.Add(deck[i + 1]);
                playerThree.Add(deck[i + 2]);
                playerFour.Add(deck[i + 3]);
            }

            Console.WriteLine("\n#########################\n");

            Console.WriteLine("Player one's hand:\n");
            foreach (Card player in playerOne) { Console.WriteLine(player); } // disp playerOne hand

            Thread.Sleep(1000);

            Console.WriteLine("\nPlayer two's hand:\n");
            foreach (Card player in playerTwo) { Console.WriteLine(player); } // disp playerTwo hand

            Thread.Sleep(1000);

            Console.WriteLine("\nPlayer three's hand:\n");
            foreach (Card player in playerThree) { Console.WriteLine(player); } // disp playerThree hand

            Thread.Sleep(1000);

            Console.WriteLine("\nPlayer four's hand:\n");
            foreach (Card player in playerFour) { Console.WriteLine(player); } // disp playerFour hand

            Console.WriteLine("\n#########################\n");


            /* SECTION 2: BETTING ROUND */ // should make this a method

            Console.WriteLine("Betting round\n");
            Thread.Sleep(1000);

            List<string> players = new List<string> { "Player One", "Player Two", "Player Three", "Player Four" };
            List<int> bets = new List<int>(); // store bets
            int passCount = 0;
            bool bettingRoundEnded = false;
    
            while (!bettingRoundEnded)
            {
                for (int i = 0; i < (players.Count); i++)
                {
                    if (bets.Count > i && bets[i] == -1)
                    { continue; }
    
                    Console.WriteLine($"{players[i]}, enter a bet (between 50-100, intervals of 5) or 'pass': ");
                    string input = Console.ReadLine().ToLower();
    
                    if (input == "pass")
                    {
                        Console.WriteLine($"{players[i]} passed\n");
                        if (bets.Count <= i)
                            bets.Add(-1);
                        else
                            bets[i] = -1;
                        passCount++;
                    }
                    else if (int.TryParse(input, out int bet))
                    {
                        if (bet >= 50 && bet < 100 && bet % 5 == 0 && !bets.Contains(bet))
                        {
                            if (bets.Count <= i)
                                bets.Add(bet);
                            else
                                bets[i] = bet;
                            Console.WriteLine();
                        }
                        else if (bet == 100)
                        {
                            if (passCount == 0)
                            {
                                bets.Add(bet);
                                bets.Add(-1);
                                bets.Add(-1);
                                bets.Add(-1);
                                bettingRoundEnded = true;
                                break;
                            }
                            else if (passCount == 1)
                            {
                                bets.Add(bet);
                                bets.Add(-1);
                                bets.Add(-1);
                                bettingRoundEnded = true;
                                break;
                            }
                            else if (passCount == 2)
                            {  
                                bets.Add(bet);
                                bets.Add(-1);
                                bettingRoundEnded = true;
                                break;
                            }
                            else
                                continue;
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
                            bettingRoundEnded = true;
                            break;
                        }
                    }
                }
            }
    
            // show betting results
            Console.WriteLine("\nBetting round complete, here are the results:");
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{players[i]} : {bets[i]}");
            }



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
            Card TrickWinner(Card playerOneCard, Card playerTwoCard, Card playerThreeCard, Card playerFourCard)
            {
                List<Card> trick = new List<Card>();
                trick.Add(playerOneCard);
                trick.Add(playerTwoCard);
                trick.Add(playerThreeCard);
                trick.Add(playerFourCard);
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
